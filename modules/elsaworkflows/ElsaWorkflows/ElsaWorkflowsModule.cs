using Elsa.Agents;
using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.EntityFrameworkCore;
using ElsaWorkflows.Data;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace ElsaWorkflows;

[DependsOn(
    typeof(ElsaWorkflowsContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class ElsaWorkflowsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<ElsaWorkflowsModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ElsaWorkflowsModule>(validate: true);
        });
        
        context.Services.AddAbpDbContext<ElsaWorkflowsDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
            
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
        });

        context.Services.AddElsa(elsa =>
        {
            elsa.UseWorkflowRuntime(runtime =>
            {
                runtime.UseEntityFrameworkCore(ef => ef.UseSqlite());
                runtime.UseCache();
            });
            elsa.UseWorkflowManagement(management =>
            {
                management.UseEntityFrameworkCore(ef => ef.UseSqlite());
                management.UseCache();
            });
            
            elsa
                .UseAgentActivities()
                .UseAgentPersistence(persistence => persistence.UseEntityFrameworkCore(ef => ef.UseSqlite()))
                .UseAgentsApi();
            
            elsa.UseJavaScript();
            elsa.UseLiquid();
            elsa.UseCSharp();
            elsa.UseWorkflowsApi();
            elsa.UseIdentity(identity => identity.TokenOptions = options => options.SigningKey = "2kj43434gv5h34gv3gfv54g3fc5g34frc5g3fcr3g4frcg3frcrg3fgrc34gfr3gfr4cg3fr");
            elsa.UseDefaultAuthentication();
            elsa.UseHttp();
            elsa.AddActivitiesFrom<ElsaWorkflowsModule>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilderOrNull();

        if (app == null)
        {
            return;
        }

        app.UseRouting();
        app.UseWorkflowsApi();
        app.UseWorkflows();
    }
}
