using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.MemberCards;

public class MemberCardDto : FullAuditedEntityDto<Guid>
{
    public DateTime ExpDate { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}