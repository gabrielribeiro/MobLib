using Microsoft.AspNet.Identity;
using MobLib.Core.Domain.Entities;

namespace MobLib.Security.Identity
{
    public abstract class IdentityUser : MobEntity, IUser<int>
    {
        public abstract string UserName { get; set; }

        public abstract string Password { get; set; }

        public abstract string Name { get; set; }
    }
}
