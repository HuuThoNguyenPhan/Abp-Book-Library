using Acme.BookLibrary.Booksp;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.Books;

public class BookDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }
    public Guid AuthorId { get; set; }
    public BookType BookType { get; set; }
    public DateTime PublishDate { get; set; }
    public int MaxReturnTime { get; set; }
    public int Quantity { get; set; }
    public long Price { get; set; }
    public string AuthorName { get; set; }
    public bool IsBorrowed { get; set; }
    public string BookId { get; set; }
}