using Acme.BookLibrary.MemberCards;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Acme.BookLibrary.Web.Pages.MemberCards;

public class CreateModalModel : BookLibraryPageModel
{
    [BindProperty]
    public CreateUpdateMemberCardDto Card { get; set; }

    private readonly IMemberCardAppService _memberCardAppService;

    public CreateModalModel(IMemberCardAppService memberCardAppService)
    {
        _memberCardAppService = memberCardAppService;
    }

    public void OnGet()
    {
        Card = new CreateUpdateMemberCardDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _memberCardAppService.CreateAsync(
          Card
           );
        return NoContent();
    }
}