using Acme.BookLibrary.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.BookLibrary;

[DependsOn(
    typeof(BookLibraryEntityFrameworkCoreTestModule)
    )]
public class BookLibraryDomainTestModule : AbpModule
{

}
