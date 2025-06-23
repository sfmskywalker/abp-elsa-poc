using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ElsaWorkflows.Data;

[ConnectionStringName(ElsaWorkflowsDbProperties.ConnectionStringName)]
public class ElsaWorkflowsDbContext : AbpDbContext<ElsaWorkflowsDbContext>, IElsaWorkflowsDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public ElsaWorkflowsDbContext(DbContextOptions<ElsaWorkflowsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureElsaWorkflows();
    }
}
