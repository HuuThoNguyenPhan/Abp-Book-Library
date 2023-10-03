using Acme.BookLibrary.Authors;
using Acme.BookLibrary.Books;
using Acme.BookLibrary.CheckoutDetails;
using Acme.BookLibrary.Checkouts;
using Acme.BookLibrary.MemberCards;
using Acme.BookLibrary.Members;
using Acme.BookStore.Books;
using AutoMapper;

namespace Acme.BookLibrary;

public class BookLibraryApplicationAutoMapperProfile : Profile
{
    public BookLibraryApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<Author, AuthorLookupDto>();
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateUpdateAuthorDto, Author>();
        CreateMap<CheckoutDetail, CheckoutDetailDto>();
        CreateMap<CreateUpdateCheckoutDetailDto, CheckoutDetail>();
        CreateMap<Checkout, CheckoutDto>();
        CreateMap<CreateUpdateCheckoutDto, Checkout>();
        CreateMap<MemberCard, MemberCardDto>();
        CreateMap<CreateUpdateMemberCardDto, MemberCard>();
    }
}