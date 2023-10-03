using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookLibrary.Authors;

public class CreateUpdateAuthorDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [Required]
    public string ShortBio { get; set; }
}