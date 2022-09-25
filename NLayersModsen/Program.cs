using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.Services;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Repositories;
using NLayerApp.WEB.Classes;

namespace NLayerApp.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // JWT Bearer configure

            builder.Services.AddAuthorization();

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(o =>
            //    {
            //        o.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidIssuer = AuthOptions.ISSUER,

            //            ValidateAudience = true,
            //            ValidAudience = AuthOptions.AUDIENCE,

            //            ValidateLifetime = true,

            //            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            //            ValidateIssuerSigningKey = true,
            //        };
            //    });

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebAPI",
                    Description = "web api for modsen",
                    Contact = new OpenApiContact
                    {
                        Name = "Mikhail Ivanov",
                        Email = "m1kh.is@mail.ru",
                        Url = new Uri("https://www.linkedin.com/in/m1kh-is/")
                    },
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // EF Core configuration
            builder.Services.AddDbContext<EventContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EventConnectionString")));

            // associations
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            builder.Services.AddScoped<IRepository<EventEntity>, EventRepository>();

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}