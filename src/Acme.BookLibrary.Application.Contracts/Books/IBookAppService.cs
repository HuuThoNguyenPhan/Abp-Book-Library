using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookLibrary.Books;

public interface IBookAppService : IApplicationService
{
    Task<PagedResultDto<BookDto>> CreateAsync(CreateUpdateBookDto input);

    Task<BookDto> GetAsync(Guid id);

    Task<PagedResultDto<BookDto>> GetListAsync(GetBookListDto input);

    Task UpdateAsync(string id, CreateUpdateBookDto input);

    Task DeleteAsync(Guid id);

    Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
}