using KeyCloak.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace KeyCloak
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddAuthorization(options =>
            //{

            //    options.AddPolicy("CanAccessMobileApp", policy => policy.RequireRole("CanAccessMobileApp"));
            //});
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CanAccessApp", policy =>
            //        policy.Requirements.Add(new CanAccessApp()));
            //});
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;                

            }).AddJwtBearer(o =>
            {
                o.Authority = "http://keycloak.microshit.org:8080/auth/realms/microshit.org";
                o.Audience = "my-app";                
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fbe4cece-c106-40e4-8e71-06b3c8d7fda9")),                    
                    ValidIssuer = "http://keycloak.microshit.org:8080/auth/realms/microshit.org",
                    ValidAudience = "account",                    
                    ValidateIssuer = true,
                    RequireSignedTokens = true,
                    ValidateAudience = true
                };                
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";

                        if (Environment.IsDevelopment())
                        {
                            return c.Response.WriteAsync(c.Exception.ToString());
                        }

                        return c.Response.WriteAsync("An error occured processing your authentication.");
                    }
                };
            });
            // services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();


            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //services.AddAuthentication(options =>
            //{
            //    // Store the session to cookies
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    // OpenId authentication
            //    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //})
            //.AddCookie("Cookies")
            //.AddOpenIdConnect(options =>
            // {
            //     options.Authority = Configuration["Jwt:Authority"];
            //     options.ClientId = Configuration["Jwt:Audience"];
            //     options.RequireHttpsMetadata = false;
            //     options.SaveTokens = true;
            //     // Client secret shared with Keycloak
            //     options.ClientSecret = "59220b63-65d5-4c50-a3ab-fc4373946054";
            //     options.GetClaimsFromUserInfoEndpoint = true;

            //     // OpenID flow to use
            //      options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
            // });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
