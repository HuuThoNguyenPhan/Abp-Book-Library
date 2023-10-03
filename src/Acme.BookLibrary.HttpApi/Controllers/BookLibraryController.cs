using Acme.BookLibrary.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.BookLibrary.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BookLibraryController : AbpControllerBase
{
    protected BookLibraryController()
    {
        LocalizationResource = typeof(BookLibraryResource);
    }
}
