using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using ElsaWorkflows.Localization;
using ElsaWorkflows.UI.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using ElsaWorkflows.Permissions;
using ElsaWorkflows.UI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace ElsaWorkflows.UI;

[DependsOn(
    typeof(ElsaWorkflowsContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
)]
public class ElsaWorkflowsWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(ElsaWorkflowsResource), typeof(ElsaWorkflowsWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ElsaWorkflowsWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ElsaWorkflowsMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ElsaWorkflowsWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<ElsaWorkflowsWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ElsaWorkflowsWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
                //Configure authorization.
            });
        
        Configure<StaticFileOptions>(ConfigureStaticFileOptions);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilderOrNull();
        
        if (app == null)
            return;

        app.UseElsaStudioBlazorAssetsUrlRewriter();
    }
    
    private void ConfigureStaticFileOptions(StaticFileOptions options)
    {
        var provider = new FileExtensionContentTypeProvider
        {
            Mappings =
            {
                // Add custom MIME type mappings for WASM
                [".dat"] = "application/octet-stream" // Adjust the MIME type as needed
            }
        };
        options.ContentTypeProvider = provider;
    }
}
