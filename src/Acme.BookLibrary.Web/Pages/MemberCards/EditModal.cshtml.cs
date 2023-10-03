using Acme.BookLibrary.MemberCards;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Acme.BookLibrary.Web.Pages.MemberCards;

public class EditModalModel : BookLibraryPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateUpdateMemberCardDto Card { get; set; }

    private readonly IMemberCardAppService _memberCardAppService;

    public EditModalModel(IMemberCardAppService memberCardAppService)
    {
        _memberCardAppService = memberCardAppService;
    }

    public async Task OnGetAsync()
    {
        var bookDto = await _memberCardAppService.GetAsync(Id);
        Card = ObjectMapper.Map<MemberCardDto, CreateUpdateMemberCardDto>(bookDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _memberCardAppService.UpdateAsync(Id, Card);
        return NoContent();
    }
}