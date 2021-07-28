using Cinema.API.Tools;
using Cinema.API.Tools.Interfaces;
using Cinema.DB.EF;
using Cinema.Services;
using Cinema.Services.Interfaces;
using Cinema.Services.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        public AuthOptions AuthOptions { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AuthOptions>(
                Configuration.GetSection(
                    AuthOptions.Position
                )
            );
            services.Configure<DatabaseOptions>(
                Configuration.GetSection(
                    DatabaseOptions.Position
                )
            );
            services.Configure<HashingOptions>(
                Configuration.GetSection(
                    HashingOptions.Position
                )
            );
            AuthOptions = new AuthOptions();
            Configuration.GetSection(AuthOptions.Position).Bind(AuthOptions);
            services.AddControllers();
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
            services.AddScoped<ApplicationContext, ApplicationContext>();
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
                            ValidIssuer = AuthOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
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