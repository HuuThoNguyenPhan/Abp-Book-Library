using System.Threading.Tasks;

namespace Acme.BookLibrary.Data;

public interface IBookLibraryDbSchemaMigrator
{
    Task MigrateAsync();
}
