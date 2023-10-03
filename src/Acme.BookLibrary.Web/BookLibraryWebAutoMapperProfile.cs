using Acme.BookLibrary.Authors;
using Acme.BookLibrary.Books;
using Acme.BookLibrary.MemberCards;
using AutoMapper;

namespace Acme.BookLibrary.Web;

public class BookLibraryWebAutoMapperProfile : Profile
{
    public BookLibraryWebAutoMapperProfile()
    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel, CreateUpdateBookDto>();
        CreateMap<BookDto, Pages.Books.EditModalModel.EditBookViewModel>();
        CreateMap<Pages.Books.EditModalModel.EditBookViewModel, CreateUpdateBookDto>();
        CreateMap<MemberCardDto, CreateUpdateMemberCardDto>();

        CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModel>();
        CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModel,
                    CreateUpdateAuthorDto>();
    }
}