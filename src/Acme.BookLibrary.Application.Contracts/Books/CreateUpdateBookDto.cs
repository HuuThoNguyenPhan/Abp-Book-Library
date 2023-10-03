using Acme.BookLibrary.Booksp;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookLibrary.Books;

public class CreateUpdateBookDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    public Guid AuthorId { get; set; }

    [Required]
    public BookType BookType { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; } = DateTime.Now;

    [Required]
    public int MaxReturnTime { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public long Price { get; set; }

    [Required]
    public bool IsBorrowed { get; set; }
}