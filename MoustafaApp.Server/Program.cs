

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using MoustafaApp.Server.DomainBusiness.CartBusiness;
using MoustafaApp.Server.Service.CartService.CartService;
using MoustafaApp.Server.Service.OrderService;
using MoustafaApp.Server.Service.ProductService;
using MoustafaApp.Server.Service.UserService;
using MoustafaApp.Server.Services.Common;
using MoustafaApp.Server.Validators;
using System.Text;

namespace moustafapp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });


            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                   ValidateIssuer = true,
                   ValidAudience = builder.Configuration["JWT:ValidAudience"],
                   ValidateAudience = true,

                   ValidateLifetime = true,

                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
                   ValidateIssuerSigningKey = true
               };
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

           
            builder.Services.AddSingleton<RedisConnection>();
            builder.Services.AddScoped<ICacheService, RedisCacheService>();
            builder.Services.AddScoped<ICheckoutService, CheckoutService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();


            // اضافة رول تلقائي

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = { "User", "Admin", "Manager" };

                foreach (var role in roles)
                {
                    var exists = roleManager.RoleExistsAsync(role).Result;

                    if (!exists)
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            app.UseCors("AllowAngular");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
