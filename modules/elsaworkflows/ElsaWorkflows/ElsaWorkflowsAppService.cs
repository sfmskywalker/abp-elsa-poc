using ElsaWorkflows.Localization;
using Volo.Abp.Application.Services;

namespace ElsaWorkflows;

public abstract class ElsaWorkflowsAppService : ApplicationService
{
    protected ElsaWorkflowsAppService()
    {
        LocalizationResource = typeof(ElsaWorkflowsResource);
        ObjectMapperContext = typeof(ElsaWorkflowsModule);
    }
}
