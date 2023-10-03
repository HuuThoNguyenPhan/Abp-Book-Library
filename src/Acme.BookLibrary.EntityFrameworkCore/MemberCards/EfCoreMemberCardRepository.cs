using Acme.BookLibrary.EntityFrameworkCore;
using Acme.BookLibrary.Members;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookLibrary.MemberCards;

public class EfCoreMemberCardRepository : EfCoreRepository<BookLibraryDbContext, MemberCard, Guid>, IMemberCardRepository
{
    public EfCoreMemberCardRepository(IDbContextProvider<BookLibraryDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<MemberCard>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                MemberCard => MemberCard.Name.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}