using Acme.BookLibrary.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Acme.BookLibrary.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BookLibraryEntityFrameworkCoreModule),
    typeof(BookLibraryApplicationContractsModule)
    )]
public class BookLibraryDbMigratorModule : AbpModule
{
}
