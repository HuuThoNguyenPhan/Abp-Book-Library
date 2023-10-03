using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookLibrary.Checkouts;

public interface ICheckoutRepository : IRepository<Checkout, Guid>
{
    Task<List<Checkout>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}