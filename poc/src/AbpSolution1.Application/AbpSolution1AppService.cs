using AbpSolution1.Localization;
using Volo.Abp.Application.Services;

namespace AbpSolution1;

/* Inherit your application services from this class.
 */
public abstract class AbpSolution1AppService : ApplicationService
{
    protected AbpSolution1AppService()
    {
        LocalizationResource = typeof(AbpSolution1Resource);
    }
}
