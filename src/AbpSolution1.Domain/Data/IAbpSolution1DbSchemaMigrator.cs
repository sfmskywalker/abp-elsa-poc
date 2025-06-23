using System.Threading.Tasks;

namespace AbpSolution1.Data;

public interface IAbpSolution1DbSchemaMigrator
{
    Task MigrateAsync();
}
