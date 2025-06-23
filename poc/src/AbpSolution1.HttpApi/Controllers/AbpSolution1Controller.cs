using AbpSolution1.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpSolution1.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AbpSolution1Controller : AbpControllerBase
{
    protected AbpSolution1Controller()
    {
        LocalizationResource = typeof(AbpSolution1Resource);
    }
}
