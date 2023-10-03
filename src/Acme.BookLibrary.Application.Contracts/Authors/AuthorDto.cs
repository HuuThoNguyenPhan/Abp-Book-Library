using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookLibrary.Authors;

public class AuthorDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }
    public string ShortBio { get; set; }
}