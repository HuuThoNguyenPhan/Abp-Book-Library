using Acme.BookLibrary.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookLibrary.Books;

public interface IBookRepository : IRepository<Book, Guid>
{
    Task<List<Book>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}