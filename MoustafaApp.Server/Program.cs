using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Text;

using MoustafaApp.Server.DomainBusiness.CartBusiness;
using MoustafaApp.Server.Service.CartService.CartService;
using MoustafaApp.Server.Service.OrderService;
using MoustafaApp.Server.Service.ProductService;
using MoustafaApp.Server.Service.UserService;
using MoustafaApp.Server.Services.Common;
using MoustafaApp.Server.Validators;

namespace moustafaapp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                    });
            });

            // Database
            builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(
         builder.Configuration.GetConnectionString("Default"),
         sqlOptions =>
         {
             sqlOptions.EnableRetryOnFailure(
                 maxRetryCount: 10,
                 maxRetryDelay: TimeSpan.FromSeconds(10),
                 errorNumbersToAdd: null);
         }));

            //redis
            var redisConnection = builder.Configuration.GetConnectionString("Redis");

            try
            {
                if (!string.IsNullOrWhiteSpace(redisConnection))
                {
                    builder.Services.AddSingleton(new RedisConnection(redisConnection));
                    builder.Services.AddSingleton<ICacheService, RedisCacheService>();

                    Console.WriteLine("Redis Connected");
                }
                else
                {
                    throw new Exception("Redis connection empty");
                }
            }
            catch
            {
                builder.Services.AddMemoryCache();
                builder.Services.AddSingleton<ICacheService, MemoryCacheService>();

                Console.WriteLine("Using MemoryCache");
            }
            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

             .AddJwtBearer(options =>
                         {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                     ValidateIssuer = true,

                     ValidAudience = builder.Configuration["JWT:ValidAudience"],
                     ValidateAudience = true,

                     ValidateLifetime = true,

                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),

                     ValidateIssuerSigningKey = true
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine(" JWT Authentication Failed");
                         Console.WriteLine(context.Exception.ToString());
                         return Task.CompletedTask;
                     },

                     OnTokenValidated = context =>
                     {
                         Console.WriteLine(" JWT Token Validated");
                         return Task.CompletedTask;
                     },

                     OnChallenge = context =>
                     {
                         Console.WriteLine("⚠️ JWT Challenge Triggered");
                         return Task.CompletedTask;
                     }
                 };
             });
            // Compression
            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(typeof(MappingModel));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddTransient<ProductIRepo, ProductRepo>();
            builder.Services.AddTransient<CategoryIRepo, CategoryRepo>();
            builder.Services.AddTransient<CartIRepo, CartRepo>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<DepartmentIRepo, DepartmentRepo>();
            builder.Services.AddTransient<CouponIRepo, CouponRepo>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<ReviewIRepo, ReviewRepo>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<CartCalculator>();
            builder.Services.AddScoped<CartValidator>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            var app = builder.Build();

            // Create roles automatically
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();   // 👈 هذا السطر المهم

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = { "User", "Admin", "Manager" };

                foreach (var role in roles)
                {
                    var exists = roleManager.RoleExistsAsync(role).Result;

                    if (!exists)
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            app.UseResponseCompression();

            app.UseCors("AllowAngular");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("index.html");

            app.Run();

        }
    }
}