using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.Books;

public class GetBookListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}