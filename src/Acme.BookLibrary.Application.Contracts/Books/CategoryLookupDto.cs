using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.Books;

public class CategoryLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}