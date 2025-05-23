using ECommerce.Core.Identity;
using ECommerce.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ECommerce.Core.Service;
using ECommerce.Service;
using System.Text;
using System.Security.Claims;



namespace ECommerceApi.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services , IConfiguration configuration) 
        {
            Services.AddIdentity<AppUser, IdentityRole>(Options =>
            {
                Options.User.RequireUniqueEmail = true;
            })
                    .AddEntityFrameworkStores<AppIdentityDbContext>();
            //Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options =>
            //{
            //    Options.MapInboundClaims = true;
            //    Options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = configuration["JWT:ValidIssuer"],
            //        ValidateAudience = true,
            //        ValidAudience = configuration["JWT:ValidAudience"],
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
            //        NameClaimType = ClaimTypes.Name,
            //        RoleClaimType = ClaimTypes.Role
            //    };
            //});

            Services.AddScoped<ITokenService, TokenService>();
            return Services;
        }
    }
}
