using Acme.BookLibrary.Books;
using Acme.BookLibrary.CheckoutDetails;
using Acme.BookLibrary.MemberCards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookLibrary.Web.Pages.CheckoutDetails;

public class CreateModalModel : BookLibraryPageModel
{
    private readonly IJSRuntime _jsRuntime;
    private readonly MemberCardAppService _memberCardAppService;

    public List<BookModalView> bookModalViews { get; set; }

    [BindProperty]
    public SearchProductInput Input { get; set; } = new SearchProductInput();

    [BindProperty]
    public CreateModalModelView MemberCard { get; set; }

    private readonly BookAppService _bookAppService;

    public CreateModalModel(BookAppService bookAppService, MemberCardAppService memberCardAppService, IJSRuntime jsRuntime)
    {
        _memberCardAppService = memberCardAppService;
        _jsRuntime = jsRuntime;
        _bookAppService = bookAppService;
    }

    [BindProperty]
    public CreateUpdateCheckoutDetailDto CheckoutDetail { get; set; }

    [BindProperty]
    public BookFind bookFind { get; set; } = new BookFind();

    public class BookFind
    {
        [Required]
        public string Id { get; set; }
    }

    public void OnGet()
    {
        CheckoutDetail = new CreateUpdateCheckoutDetailDto();
        bookModalViews = new List<BookModalView>();
    }

    public async void OnPostGetInfoAsync()
    {
        var memberCard = await _memberCardAppService.GetAsync(Guid.Parse(Input.Keyword));
        MemberCard.Id = memberCard.Id.ToString();
        MemberCard.Name = memberCard.Name;
        MemberCard.Address = memberCard.Address;
        MemberCard.Phone = memberCard.Phone;
        MemberCard.Email = memberCard.Email;
    }

    public async void OnPostAddRowAsync()
    {
        Guid guid;
        bool isGuidValid = Guid.TryParse(bookFind.Id, out guid);

        if (isGuidValid)
        {
            var book = await _bookAppService.GetAsync(Guid.Parse(bookFind.Id));
            if (book == null)
            {
                return;
            }
            else
            {
                bookModalViews = bookModalViews ?? new List<BookModalView>();

                var bookModalView = new BookModalView
                {
                    Id = bookFind.Id,
                    Name = book.Name,
                    IsReturned = "false",
                    ReturnDate = DateTime.Now.AddDays(14)
                };
                bookModalViews.Add(bookModalView);
            }
        }
        else
        {
            return;
        }
        
    }

    public class CreateModalModelView
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

        [Required]
        public string Id { get; set; }
    }

    public class BookModalView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IsReturned { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    public class SearchProductInput
    {
        public string Keyword { get; set; }
    }
}