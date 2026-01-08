

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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


            builder.Services.AddAutoMapper(typeof(MappingModel));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddTransient<ProductIRepo, ProductRepo>();
            builder.Services.AddTransient<CategoryIRepo, CategoryRepo>();
            builder.Services.AddTransient<CartIRepo, CartRepo>();
            builder.Services.AddTransient<DepartmentIRepo, DepartmentRepo>();
            builder.Services.AddScoped<IImageService, ImageService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

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
