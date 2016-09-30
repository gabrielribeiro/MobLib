using MobLib.Core.Domain.Contracts;
using System;
using System.Threading.Tasks;
using AspNetIdentity = Microsoft.AspNet.Identity;

namespace MobLib.Security.Identity
{
    public class UserStore<T> : AspNetIdentity.IUserStore<T, int>
        where T : IdentityUser
    {
        private IMobService<T> userService;

        public UserStore(IMobService<T> userService)
        {
            this.userService = userService;
        }

        public Task CreateAsync(T user)
        {
            return Task.Run(() => this.userService.Insert(user));
        }

        public Task DeleteAsync(T user)
        {
            return Task.Run(() => this.userService.Delete(user));
        }

        public Task<T> FindByIdAsync(int userId)
        {
            return Task.Run(() => this.userService.SingleOrDefault(x => x.Id == userId));

        }

        public Task<T> FindByNameAsync(string userName)
        {
            return Task.Run(() => this.userService.SingleOrDefault(x => x.UserName == userName));
        }

        public Task UpdateAsync(T user)
        {
            return Task.Run(() => this.userService.Update(user));
        }

        public void Dispose()
        {
            userService.Dispose();
        }
    }
}
