using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.CheckoutDetails;

public class CheckoutDetailDto : FullAuditedEntityDto<Guid>
{
    public Guid CheckoutId { get; set; }
    public DateTime ReturnDate { get; set; }
    public Guid BookId { get; set; }
    public string BookName { get; set; }
}