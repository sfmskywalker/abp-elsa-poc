using ElsaWorkflows.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ElsaWorkflows.UI.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ElsaWorkflowsPageModel : AbpPageModel
{
    protected ElsaWorkflowsPageModel()
    {
        LocalizationResourceType = typeof(ElsaWorkflowsResource);
        ObjectMapperContext = typeof(ElsaWorkflowsWebModule);
    }
}
