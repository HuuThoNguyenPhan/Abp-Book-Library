using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookLibrary.Checkouts;

public class Checkout : FullAuditedAggregateRoot<Guid>
{
    public Guid CardId { get; set; }
    public long Deposit { get; set; }
    public bool IsFinished { get; set; }

    public Checkout()
    {
    }

    public Checkout(Guid id, Guid cardId, long deposit, bool isFinished) : base(id)
    {
        CardId = cardId;
        Deposit = deposit;
        IsFinished = isFinished;
    }
}