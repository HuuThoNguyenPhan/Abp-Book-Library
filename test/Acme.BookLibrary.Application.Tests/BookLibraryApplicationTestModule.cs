using Volo.Abp.Modularity;

namespace Acme.BookLibrary;

[DependsOn(
    typeof(BookLibraryApplicationModule),
    typeof(BookLibraryDomainTestModule)
    )]
public class BookLibraryApplicationTestModule : AbpModule
{

}
