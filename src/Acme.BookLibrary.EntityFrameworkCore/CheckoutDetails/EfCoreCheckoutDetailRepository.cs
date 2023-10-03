using Acme.BookLibrary.Checkouts;
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

namespace Acme.BookLibrary.CheckoutDetails;

public class EfCoreCheckoutDetailRepository : EfCoreRepository<BookLibraryDbContext, CheckoutDetail, Guid>, ICheckoutDetailRepository
{
    public EfCoreCheckoutDetailRepository(IDbContextProvider<BookLibraryDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<CheckoutDetail>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}