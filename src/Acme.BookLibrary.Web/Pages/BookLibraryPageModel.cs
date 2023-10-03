using Acme.BookLibrary.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Acme.BookLibrary.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class BookLibraryPageModel : AbpPageModel
{
    protected BookLibraryPageModel()
    {
        LocalizationResourceType = typeof(BookLibraryResource);
    }
}
