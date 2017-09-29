using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Company.IdentityServer.Example;
using Company.IdentityServer.Example.Configuration;
using System;
using System.Globalization;
using System.Security.Claims;
using IdentityModel;
using Microsoft.Extensions.Options;
using System.Reflection;
using Company.IdentityServer.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Localization;

namespace Company.IdentityServer
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            _environment = env;
            _environment.ConfigureNLog("nlog.config");
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            string connectionString =  Configuration.GetConnectionString("DefaultConnection");
            //string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.UserAndConfig;trusted_connection=yes;"; ;
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            IdentityServerSettings settings = new IdentityServerSettings();
            Configuration.GetSection("IdentityServerSettings").Bind(settings);

            // ASP.NET Identity DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
  
            // ASP.NET Identity Registrations
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);
                options.Lockout.MaxFailedAccessAttempts = 5;
                
            }).AddEntityFrameworkStores<ApplicationDbContext>();
             
            services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            
            services.AddIdentityServer()
                //.AddOperationalStore(
                //    builder => builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)))
                //.AddConfigurationStore(
                //    builder => builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)))
                .AddInMemoryClients(Clients.Get(settings))
                .AddInMemoryIdentityResources(Company.IdentityServer.Example.Configuration.Resources.GetIdentityResources())
                .AddInMemoryApiResources(Company.IdentityServer.Example.Configuration.Resources.GetApiResources())
                //.AddTestUsers(Users.Get())
                .AddAspNetIdentity<IdentityUser>()
                .AddTemporarySigningCredential()
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

            services.AddMvc().AddViewLocalization();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.UseDeveloperExceptionPage();

            var localizationOption = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOption.Value);

            InitializeDbTestData(app);

            app.UseIdentity();
            app.UseIdentityServer();
            
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.AddNLogWeb();
        }

        private static void InitializeDbTestData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                //scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

                //var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                
                //if (!context.Clients.Any())
                //{
                //    foreach (var client in Clients.Get())
                //    {
                //        context.Clients.Add(client.ToEntity());
                //    }
                //    context.SaveChanges();
                //}

                //if (!context.IdentityResources.Any())
                //{
                //    foreach (var resource in Resources.GetIdentityResources())
                //    {
                //        context.IdentityResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}

                //if (!context.ApiResources.Any())
                //{
                //    foreach (var resource in Resources.GetApiResources())
                //    {
                //        context.ApiResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}
                
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                
                if (!roleManager.Roles.Any())
                {
                    var support = new IdentityRole
                    {
                        Id = "CustomerSupport",
                        ConcurrencyStamp = new Guid().ToString(),
                        Name = "CustomerSupport",
                        NormalizedName = "BAR"
                    };
                    roleManager.CreateAsync(support).Wait();
                    roleManager.AddClaimAsync(support, new Claim(CustomClaimTypes.Permission, "accountApi.Read")).Wait();

                    var builder = new IdentityRole
                    {
                        Id = "Builder",
                        ConcurrencyStamp = new Guid().ToString(),
                        Name = "Builder",
                        NormalizedName = "Builder"
                    };
                    roleManager.CreateAsync(builder).Wait();
                    roleManager.AddClaimAsync(builder, new Claim(CustomClaimTypes.Permission, "accountApi.Read")).Wait();
                    roleManager.AddClaimAsync(builder, new Claim(CustomClaimTypes.Permission, "accountApi.Write")).Wait();

                    var partner = new IdentityRole
                    {
                        Id = "Partner",
                        ConcurrencyStamp = new Guid().ToString(),
                        Name = "Partner",
                        NormalizedName = "Partner"
                    };
                    roleManager.CreateAsync(partner).Wait();
                    roleManager.AddClaimAsync(partner, new Claim(CustomClaimTypes.Permission, "accountApi.Read")).Wait();
                    roleManager.AddClaimAsync(partner, new Claim(CustomClaimTypes.Permission, "accountApi.Write")).Wait();

                }

                if (!userManager.Users.Any())
                {
                    foreach (var testUser in Users.Get())
                    {
                        var identityUser = new IdentityUser(testUser.Username)
                        {
                            Id = testUser.SubjectId,
                            Email = testUser.Email
                        };                        

                        foreach (var claim in testUser.Claims)                        {
                            
                            identityUser.Claims.Add(new IdentityUserClaim<string>
                            {
                                UserId = identityUser.Id,
                                ClaimType = claim.Type,
                                ClaimValue = claim.Value,
                            });
                        }

                        userManager.CreateAsync(identityUser, testUser.Password).Wait();
                        userManager.AddToRolesAsync(identityUser, testUser.Roles).Wait();
                    }
                }
            }
        }
    }
}