using AbpSolution1.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpSolution1.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpSolution1EntityFrameworkCoreModule),
    typeof(AbpSolution1ApplicationContractsModule)
)]
public class AbpSolution1DbMigratorModule : AbpModule
{
}
