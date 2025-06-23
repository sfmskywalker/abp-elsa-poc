namespace ElsaWorkflows;

public static class ElsaWorkflowsDbProperties
{
    public static string DbTablePrefix { get; set; } = "ElsaWorkflows";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "ElsaWorkflows";
}
