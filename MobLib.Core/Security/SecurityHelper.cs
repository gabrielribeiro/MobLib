using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MobLib.Security
{
    public static class SecurityHelper
    {
        public static T GetClaimValue<T>(this ClaimsIdentity identiy, string claimType)
        {
            return identiy.Claims.GetClaimValue<T>(claimType);
        }

        public static T GetClaimValue<T>(this IEnumerable<Claim> claims, string claimType)
        {
            var defaultValue = default(T);

            var claimValue = SecurityHelper.GetClaimValue(claims, claimType);

            if (claimValue != null)
            {
                defaultValue = (T)Convert.ChangeType(claimValue as object, typeof(T));
            }

            return defaultValue;
        }

        public static string GetClaimValue(this IEnumerable<Claim> claims, string claimType)
        {
            string defaultValue = null;

            var claim = claims.FirstOrDefault(e => e.Type == claimType);

            if (claim != null && claim.Value != null)
            {
                defaultValue = claim.Value;
            }

            return defaultValue;
        }

        public static void AddOrUpdateClaimValue(this ClaimsIdentity identiy, string claimType, string claimValue)
        {
            if (identiy.HasClaim(e => e.Type == claimType))
            {
                var claim = identiy.FindFirst(claimType);

                identiy.RemoveClaim(claim);
            }

            var newClaim = new Claim(claimType, claimValue);
            identiy.AddClaim(newClaim);
        }
    }
}
