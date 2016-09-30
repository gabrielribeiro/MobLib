using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MobLib.Security.Identity
{
    public class UserManager<T> : UserManager<T, int>
        where T : IdentityUser
    {
        public UserManager(IUserStore<T, int> store) : base(store)
        {
        }

        public override Task<bool> CheckPasswordAsync(T user, string password)
        {

            return Task.Run(() =>
            {
                bool result = false;
                if (user != null && !string.IsNullOrEmpty(password))
                {
                    var hashedPassword = Criptography.SHA3Hasher.Hash(password);
                    result = user.Password == hashedPassword;
                }

                return result;
            });
        }

        public async override Task<ClaimsIdentity> CreateIdentityAsync(T user, string authenticationType)
        {
            var identity = await base.CreateIdentityAsync(user, authenticationType);

            identity.AddOrUpdateClaimValue(ClaimTypes.Sid, user.Id.ToString());
            identity.AddOrUpdateClaimValue(ClaimTypes.NameIdentifier, user.UserName.ToString());
            identity.AddOrUpdateClaimValue(ClaimTypes.Name, user.Name.ToString());
                
            return identity;
        }
    }
}
