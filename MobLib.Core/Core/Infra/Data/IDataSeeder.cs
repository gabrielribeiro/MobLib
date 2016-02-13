namespace MobLib.Core.Infra.Data
{
    public interface IDataSeeder
    {
        void SeedData(IMobContext context);
    }
}
