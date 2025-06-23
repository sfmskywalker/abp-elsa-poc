using ElsaWorkflows.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ElsaWorkflows.Permissions;

public class ElsaWorkflowsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ElsaWorkflowsPermissions.GroupName, L("Permission:ElsaWorkflows"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ElsaWorkflowsResource>(name);
    }
}
