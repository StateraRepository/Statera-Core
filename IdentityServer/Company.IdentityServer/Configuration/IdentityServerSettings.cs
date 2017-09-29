using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Company.IdentityServer
{
    public class IdentityServerSettings
    {
        public string redirectUrls { get; set; }
        public List<string> redirectUrlsList {
            get
            {
                return this.redirectUrls.Split(',').ToList();
            }
        }
        public string postLogoutRedirectUris { get; set; }
        public List<string> postLogoutRedirectUrisList {
            get
            {
                return this.postLogoutRedirectUris.Split(',').ToList();
            }
        }
        public string allowedCorsOrigins { get; set; }
        public List<string> allowedCorsOriginsList {
            get
            {
                return this.allowedCorsOrigins.Split(',').ToList();
            }
        }

    }
    
    //public class ConfigureMultitenancyOptions : IConfigureOptions<MultitenancyOptions>
    //{
    //    private readonly IServiceScopeFactory _serviceScopeFactory;
    //    public ConfigureMultitenancyOptions(IServiceScopeFactory serivceScopeFactory)
    //    {
    //        _serviceScopeFactory = serivceScopeFactory;
    //    }

    //    public void Configure(MultitenancyOptions options)
    //    {
    //        using (var scope = _serviceScopeFactory.CreateScope())
    //        {
    //            var provider = scope.ServiceProvider;
    //            using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
    //            {
    //                options.AppTenants = dbContext.AppTenants.ToList();
    //            }
    //        }
    //    }
    //}
}
