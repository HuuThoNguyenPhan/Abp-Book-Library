using Acme.BookLibrary.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Threading.Tasks;

namespace Acme.BookLibrary.Web.Pages.Authors;

public class EditModalModel : BookLibraryPageModel
{
    [BindProperty]
    public EditAuthorViewModel Author { get; set; }

    private readonly IAuthorAppService _authorAppService;

    public EditModalModel(IAuthorAppService authorAppService)
    {
        _authorAppService = authorAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var authorDto = await _authorAppService.GetAsync(id);
        Author = ObjectMapper.Map<AuthorDto, EditAuthorViewModel>(authorDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _authorAppService.UpdateAsync(
            Author.Id,
            ObjectMapper.Map<EditAuthorViewModel, CreateUpdateAuthorDto>(Author)
        );

        return NoContent();
    }

    public class EditAuthorViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [TextArea]
        public string ShortBio { get; set; }
    }
}