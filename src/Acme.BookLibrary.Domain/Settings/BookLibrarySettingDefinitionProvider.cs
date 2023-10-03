using Volo.Abp.Settings;

namespace Acme.BookLibrary.Settings;

public class BookLibrarySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BookLibrarySettings.MySetting1));
    }
}
