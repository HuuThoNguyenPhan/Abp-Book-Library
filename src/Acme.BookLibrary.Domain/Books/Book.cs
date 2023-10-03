using Acme.BookLibrary.Booksp;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookLibrary.Books;

public class Book : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public DateTime PublishDate { get; set; }
    public Guid AuthorId { get; set; }
    public BookType BookType { get; set; }
    public int Quantity { get; set; }
    public int MaxReturnTime { get; set; }
    public long Price { get; set; }
    public string BookId { get; set; }

    public bool IsBorrowed { get; set; }

    public Book()
    {
    }

    public Book(
        Guid id,
        [NotNull] string name,
        [NotNull] DateTime publishDate,
        [NotNull] Guid authorId,
        [NotNull] BookType bookType,
        [NotNull] int quantity,
        [NotNull] int maxReturn,
        [NotNull] long price,
        [NotNull] bool isBorrowed,
        string bookId)
        : base(id)
    {
        AuthorId = authorId;
        Name = name;
        PublishDate = publishDate;
        BookType = bookType;
        Quantity = quantity;
        MaxReturnTime = maxReturn;
        Price = price;
        BookId = bookId;
        IsBorrowed = isBorrowed;
    }
}