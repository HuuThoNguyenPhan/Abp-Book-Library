using System;
using System.Collections.Generic;
using System.Text;
using Acme.BookLibrary.Localization;
using Volo.Abp.Application.Services;

namespace Acme.BookLibrary;

/* Inherit your application services from this class.
 */
public abstract class BookLibraryAppService : ApplicationService
{
    protected BookLibraryAppService()
    {
        LocalizationResource = typeof(BookLibraryResource);
    }
}
