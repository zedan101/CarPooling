using CarPool.Services;
using CarPool.Services.Interfaces;
using CarPool.Services.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Security.Claims;
using System.Text;
using Carpool.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CarPool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IRidesService, RidesService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserContext, UserContext>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.Issuer,
                        ValidAudience = Configuration.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.key))
                     };
                });

            builder.Services.Configure<IdentityOptions>(options => options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
            

            var connectionString = builder.Configuration["ConnectionString:CarpoolDB"];

            builder.Services.AddDbContext<CarPoolContext>(opts => opts.UseSqlServer(connectionString));

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration.swaggerDocVersion, new() { Title = Configuration.swaggerDocTitle, Version = Configuration.swaggerDocVersion });
                //c.CustomSchemaIds(x => x.FullName);
                c.CustomSchemaIds(s => s.FullName?.Replace("+", "."));
                c.AddSecurityDefinition(Configuration.swaggerAuthSchema, new OpenApiSecurityScheme
                {
                    Name = Configuration.swaggerAuthName,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Configuration.swaggerAuthSchema,
                    BearerFormat = Configuration.swaggerBearerFormate,
                    In = ParameterLocation.Header,
                    Description = Configuration.swaggerAuthDescription,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id =Configuration.swaggerAuthName
                },
                Scheme = Configuration.swaggerAuthSchema,
                Name = Configuration.swaggerAuthName,
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
            });
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}