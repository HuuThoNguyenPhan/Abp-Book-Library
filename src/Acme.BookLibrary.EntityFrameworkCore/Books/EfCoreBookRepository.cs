using Acme.BookLibrary.Authors;
using Acme.BookLibrary.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookLibrary.Books;

public class EfCoreBookRepository : EfCoreRepository<BookLibraryDbContext, Book, Guid>, IBookRepository
{
    public EfCoreBookRepository(IDbContextProvider<BookLibraryDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Book>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                b => b.Name.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}