using Acme.BookLibrary.CheckoutDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookLibrary.Checkouts;

public class CreateUpdateCheckoutDto
{
    [Required]
    public Guid CardId { get; set; }

    [Required]
    public long Deposit { get; set; }

    [Required]
    public bool IsFinished { get; set; }

    [Required]
    public List<CreateUpdateCheckoutDetailDto> CheckoutDetails { get; set; }
}