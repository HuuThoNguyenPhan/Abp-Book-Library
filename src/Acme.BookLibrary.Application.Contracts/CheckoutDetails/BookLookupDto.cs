using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.CheckoutDetails;

public class BookLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}