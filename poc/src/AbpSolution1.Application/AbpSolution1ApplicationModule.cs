using AbpSolution1.Books;
using Elsa.Extensions;
using ElsaWorkflows;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;

namespace AbpSolution1;

[DependsOn(
    typeof(AbpSolution1DomainModule),
    typeof(AbpSolution1ApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(ElsaWorkflowsModule)
    )]
public class AbpSolution1ApplicationModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.ConfigureElsa(elsa =>
        {
            elsa.AddVariableTypeAndAlias<Book>("Book", "Books");
            elsa.AddVariableTypeAndAlias<BookDto>("BookDto", "Books");
            elsa.AddActivitiesFrom<AbpSolution1ApplicationModule>();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AbpSolution1ApplicationModule>();
        });
        
        
    }
}
