using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AbpSolution1.Data;
using Volo.Abp.DependencyInjection;

namespace AbpSolution1.EntityFrameworkCore;

public class EntityFrameworkCoreAbpSolution1DbSchemaMigrator
    : IAbpSolution1DbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAbpSolution1DbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the AbpSolution1DbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AbpSolution1DbContext>()
            .Database
            .MigrateAsync();
    }
}
