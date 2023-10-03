using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.Checkouts;

public class GetCheckoutListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}