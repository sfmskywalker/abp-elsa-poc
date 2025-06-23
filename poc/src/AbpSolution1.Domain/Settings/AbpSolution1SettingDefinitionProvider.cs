using Volo.Abp.Settings;

namespace AbpSolution1.Settings;

public class AbpSolution1SettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AbpSolution1Settings.MySetting1));
    }
}
