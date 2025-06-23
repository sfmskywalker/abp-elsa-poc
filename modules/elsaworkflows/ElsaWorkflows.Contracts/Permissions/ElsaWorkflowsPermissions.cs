using Volo.Abp.Reflection;

namespace ElsaWorkflows.Permissions;

public class ElsaWorkflowsPermissions
{
    public const string GroupName = "ElsaWorkflows";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ElsaWorkflowsPermissions));
    }
}
