using Acme.BookLibrary.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookLibrary.Checkouts;

public class EfCoreCheckoutRepository : EfCoreRepository<BookLibraryDbContext, Checkout, Guid>, ICheckoutRepository
{
    public EfCoreCheckoutRepository(IDbContextProvider<BookLibraryDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Checkout>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                c => c.CardId.ToString().Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}