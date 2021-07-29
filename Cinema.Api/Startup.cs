using Cinema.API.Tools;
using Cinema.API.Tools.Interfaces;
using Cinema.DB.EF;
using Cinema.Services;
using Cinema.Services.Interfaces;
using Cinema.Services.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Cinema.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AuthOptions>(
                Configuration.GetSection(
                    AuthOptions.Position
                )
            );
            services.Configure<HashingOptions>(
                Configuration.GetSection(
                    HashingOptions.Position
                )
            );
            var authOptions = new AuthOptions();
            Configuration.GetSection(AuthOptions.Position).Bind(authOptions);
            services.AddControllers();
            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer("name=DatabaseOptions:ConnectionString"));
            services.AddCors(
                c =>
                {
                    c.AddPolicy(
                        "AllowOrigin",
                        options =>
                            options
                                .AllowAnyMethod()
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                    );
                }
            );
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthTool, AuthTool>();
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1", 
                        new OpenApiInfo
                        {
                            Title = "Cinema.Api", 
                            Version = "v1"
                        }
                    );
                }
            );
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    options => 
                    { 
                        options.RequireHttpsMetadata = true; 
                        options.TokenValidationParameters = new TokenValidationParameters 
                        {
                            ValidateIssuer = true,
                            ValidIssuer = authOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = authOptions.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true
                        }; 
                    }
                );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema.Api v1"));
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowOrigin");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                }
            );
        }
    }
}