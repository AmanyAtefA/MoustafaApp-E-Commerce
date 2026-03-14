using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using MoustafaApp.Server.Extensions;
using System.IO.Compression;
using System.Text;

namespace moustafaapp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ===============================
            // CORS
            // ===============================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            // ===============================
            // Database
            // ===============================
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


            // ===============================
            // Redis / Memory Cache
            // ===============================
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


            // ===============================
            // Identity
            // ===============================
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


            // ===============================
            // JWT Authentication
            // ===============================
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
                        Console.WriteLine("JWT Authentication Failed");
                        Console.WriteLine(context.Exception.ToString());
                        return Task.CompletedTask;
                    },

                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("JWT Token Validated");
                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        Console.WriteLine("JWT Challenge Triggered");
                        return Task.CompletedTask;
                    }
                };
            });


            // ===============================
            // Response Compression
            // ===============================
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


            builder.Services.AddControllers();
            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingModel));

 
            // ===============================
            // IServices & IRepo
            // ===============================
            builder.Services.AddApplicationServices();


           

            builder.Services.AddEndpointsApiExplorer();


            // ===============================
            // Swagger
            // ===============================
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer",
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using Bearer scheme",
                        Name = "Authorization",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT"
                    });

                options.AddSecurityRequirement(
                    new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
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


            // ===============================
            // Database Migration + Roles
            // ===============================
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = { "User", "Admin", "Manager" };

                foreach (var role in roles)
                {
                    var exists = roleManager.RoleExistsAsync(role).Result;

                    if (!exists)
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }


            // ===============================
            // Middleware Pipeline
            // ===============================

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // 👈 يظهر الأخطاء بالتفصيل
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseResponseCompression();

            app.UseCors("AllowAngular");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            // Angular Routing
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}