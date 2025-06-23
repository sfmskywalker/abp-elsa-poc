using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ElsaWorkflows.Data;

[ConnectionStringName(ElsaWorkflowsDbProperties.ConnectionStringName)]
public interface IElsaWorkflowsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
