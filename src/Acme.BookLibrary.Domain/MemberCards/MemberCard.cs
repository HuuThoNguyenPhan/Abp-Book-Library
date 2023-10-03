using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookLibrary.Members;

public class MemberCard : FullAuditedAggregateRoot<Guid>
{
    public DateTime ExpDate { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public MemberCard()
    {
    }

    public MemberCard(Guid id, DateTime expDate, string name, string address, string email, string phone) : base(id)
    {
        ExpDate = expDate;
        Name = name;
        Address = address;
        Email = email;
        Phone = phone;
    }
}