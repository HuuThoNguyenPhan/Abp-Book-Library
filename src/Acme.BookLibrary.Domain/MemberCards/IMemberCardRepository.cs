using Acme.BookLibrary.Members;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookLibrary.MemberCards;

public interface IMemberCardRepository : IRepository<MemberCard, Guid>
{
    Task<List<MemberCard>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}