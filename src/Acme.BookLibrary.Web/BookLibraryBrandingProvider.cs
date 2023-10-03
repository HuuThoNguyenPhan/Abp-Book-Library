using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Acme.BookLibrary.Web;

[Dependency(ReplaceServices = true)]
public class BookLibraryBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BookLibrary";
}
