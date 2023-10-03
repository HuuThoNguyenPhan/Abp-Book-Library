using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookLibrary.MemberCards;

public class CreateUpdateMemberCardDto
{

    [Required]
    public DateTime ExpDate { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }
}