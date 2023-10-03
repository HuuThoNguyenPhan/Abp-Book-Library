using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.BookLibrary.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.BookLibrary.EntityFrameworkCore;

public class EntityFrameworkCoreBookLibraryDbSchemaMigrator
    : IBookLibraryDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBookLibraryDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BookLibraryDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BookLibraryDbContext>()
            .Database
            .MigrateAsync();
    }
}
