using Acme.BookLibrary.Authors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookLibrary.Web.Pages.Authors;

public class CreateModalModel : BookLibraryPageModel
{
    [BindProperty]
    public CreateAuthorViewModel Author { get; set; }

    private readonly IAuthorAppService _authorAppService;

    public CreateModalModel(IAuthorAppService authorAppService)
    {
        _authorAppService = authorAppService;
    }

    public void OnGet()
    {
        Author = new CreateAuthorViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateAuthorViewModel, CreateUpdateAuthorDto>(Author);
        await _authorAppService.CreateAsync(dto);
        return NoContent();
    }

    public class CreateAuthorViewModel
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [TextArea]
        public string ShortBio { get; set; }
    }
}