using System.Runtime.CompilerServices;

namespace MobLib.Core.Infra.Data
{
    public static class MobContextResolver<TContext> where TContext : MobDbContext, new()
    {
        private static TContext context;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static TContext Current()
        {
            if (context == null)
            {
                context = new TContext();
            }

            return context;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Renew()
        {
            context = new TContext();
        }
    }
}
