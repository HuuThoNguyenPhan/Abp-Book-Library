using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.MemberCards;

public class GetMemberCardListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}