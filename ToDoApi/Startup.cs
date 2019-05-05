using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Client.TokenCacheProviders;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ToDoApi.Core.Data;
using ToDoApi.InMemory;
using ToDoApi.InMemory.EntityFramework;

namespace ToDoApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<TodoContext>(options =>
            {
                options.UseInMemoryDatabase("toDoDatabase");
            });

            services.AddTransient<IToDoListRepository, EntityFrameworkToDoListRepository>();
            services.AddTransient<IToDoItemRepository, EntityFrameworkToDoItemRepository>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "ToDo V1", Version = "1.0.0.0" });
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme()
                {
                    AuthorizationUrl = $"https://{Configuration["Auth0:Domain"]}/authorize?audience={Configuration["Auth0:ApiIdentifier"]}",
                    Flow = "implicit",
                    TokenUrl = $"https://{Configuration["Auth0:Domain"]}/token",
                    Scopes = new Dictionary<string, string> {
                            { "read", "User Profile"},
                            { "openid", "User Profile" }
                        }
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", new[] { "read" } }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseCors(config =>
            {
                config.AllowAnyHeader();
                config.AllowAnyMethod();
                config.AllowAnyOrigin();
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
                c.OAuthClientId(Configuration["Auth0:ClientId"]);
                c.OAuthClientSecret(Configuration["Auth0:ClientSecret"]);
                c.OAuthAppName("ToDoSwagger");
                c.OAuthScopeSeparator(" ");
            });
        }
    }
}
