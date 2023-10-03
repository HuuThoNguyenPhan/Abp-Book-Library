using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.BookLibrary.Data;

/* This is used if database provider does't define
 * IBookLibraryDbSchemaMigrator implementation.
 */
public class NullBookLibraryDbSchemaMigrator : IBookLibraryDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
