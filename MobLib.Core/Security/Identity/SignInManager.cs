using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace MobLib.Security.Identity
{
    public class SignInManager<T> : SignInManager<T, int>
        where T : IdentityUser
    {
        public SignInManager(UserManager<T> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(T user)
        {
            var result = this.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return result;
        }

        public async override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            SignInStatus result = SignInStatus.Failure;
            var user = await this.UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                var isCheckedPassword = await this.UserManager.CheckPasswordAsync(user, password);
                if (isCheckedPassword)
                {

                    await base.SignInAsync(user, isPersistent, shouldLockout);
                    result = SignInStatus.Success;
                }
            }

            return result;
        }
    }
}
