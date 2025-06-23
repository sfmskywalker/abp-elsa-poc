using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using AbpSolution1.Localization;

namespace AbpSolution1.Web;

[Dependency(ReplaceServices = true)]
public class AbpSolution1BrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<AbpSolution1Resource> _localizer;

    public AbpSolution1BrandingProvider(IStringLocalizer<AbpSolution1Resource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
