using Acme.BookLibrary.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookLibrary.CheckoutDetails;

public interface ICheckoutDetailRepository : IRepository<CheckoutDetail, Guid>
{
    Task<List<CheckoutDetail>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}