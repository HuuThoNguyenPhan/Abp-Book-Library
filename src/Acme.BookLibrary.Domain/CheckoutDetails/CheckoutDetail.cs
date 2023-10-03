using Acme.BookLibrary.Checkouts;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookLibrary.CheckoutDetails;

public class CheckoutDetail : FullAuditedAggregateRoot<Guid>
{
    public Guid CheckoutId { get; set; }
    public DateTime ReturnDate { get; set; }
    public Guid BookId { get; set; }
}