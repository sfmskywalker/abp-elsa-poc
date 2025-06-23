using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpSolution1.Data;

/* This is used if database provider does't define
 * IAbpSolution1DbSchemaMigrator implementation.
 */
public class NullAbpSolution1DbSchemaMigrator : IAbpSolution1DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
