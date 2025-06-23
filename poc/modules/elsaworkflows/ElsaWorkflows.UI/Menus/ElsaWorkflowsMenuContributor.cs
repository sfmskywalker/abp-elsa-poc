using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace ElsaWorkflows.UI.Menus;

public class ElsaWorkflowsMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(ElsaWorkflowsMenus.Prefix, displayName: "Elsa", "~/ElsaWorkflows", icon: "fa fa-diagram-project")
            .AddItem(new ApplicationMenuItem(ElsaWorkflowsMenus.Prefix + ".WorkflowDefinitionEditor", displayName: "Editor", url: "~/elsa/workflows/editor/b3ee5c3c5fe726c8", icon: "fa fa-edit"))
            .AddItem(new ApplicationMenuItem(ElsaWorkflowsMenus.Prefix + ".WorkflowDefinitions", displayName: "Workflow Definitions", url: "~/elsa/workflows/definitions", icon: "fa fa-list"))
            .AddItem(new ApplicationMenuItem(ElsaWorkflowsMenus.Prefix + ".WorkflowInstances", displayName: "Workflow Instances", url: "~/elsa/workflows/instances", icon: "fa fa-cogs")));

        return Task.CompletedTask;
    }
}