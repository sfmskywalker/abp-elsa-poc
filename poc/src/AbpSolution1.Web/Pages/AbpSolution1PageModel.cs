using AbpSolution1.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AbpSolution1.Web.Pages;

public abstract class AbpSolution1PageModel : AbpPageModel
{
    protected AbpSolution1PageModel()
    {
        LocalizationResourceType = typeof(AbpSolution1Resource);
    }
}
