using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookLibrary.CheckoutDetails;

public class CreateUpdateCheckoutDetailDto
{
    public Guid CheckoutId { get; set; }

    [Required]
    public DateTime ReturnDate { get; set; }

    [Required]
    public Guid BookId { get; set; }
}