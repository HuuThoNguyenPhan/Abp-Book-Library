﻿using Acme.BookLibrary.CheckoutDetails;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.Checkouts;

public class CheckoutDto : FullAuditedEntityDto<Guid>
{
    public Guid CardId { get; set; }
    public long Deposit { get; set; }
    public bool IsFinished { get; set; }
    public List<CheckoutDetailDto> CheckoutDetails { get; set; }
}