using System.ComponentModel.DataAnnotations;

namespace ElsaWorkflows.UI.Pages.ElsaWorkflows;

public class EditModel : ElsaWorkflowsPageModel
{
    [Required] public string DefinitionId { get; set; } = null!;
}