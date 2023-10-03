using Acme.BookLibrary.Authors;
using Acme.BookLibrary.CheckoutDetails;
using Acme.BookLibrary.Checkouts;
using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookLibrary.Books;

public class BookAppService : BookLibraryAppService, IBookAppService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICheckoutDetailRepository _checkoutDetailRepository;
    private readonly ICheckoutRepository _checkoutRepository;

    public BookAppService(IBookRepository bookRepository, IAuthorRepository authorRepository, ICheckoutDetailRepository checkoutDetailRepository, ICheckoutRepository checkoutRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _checkoutDetailRepository = checkoutDetailRepository;
        _checkoutRepository = checkoutRepository;
    }

    public async Task<PagedResultDto<BookDto>> CreateAsync(CreateUpdateBookDto input)
    {
        var bookId = "Book" + DateTime.Now.ToFileTime();
        for (int i = 0; i < input.Quantity; i++)
        {
            var book = new Book(
            GuidGenerator.Create(),
            input.Name,
            input.PublishDate,
            input.AuthorId,
            input.BookType,
            input.Quantity,
            input.MaxReturnTime,
            input.Price,
            false,
            bookId

             );
            await _bookRepository.InsertAsync(book);
        }
        var books = await _bookRepository.GetListAsync(b => b.BookId == bookId);

        var totalCount = await _bookRepository.CountAsync(
                b => b.BookId == bookId);

        return new PagedResultDto<BookDto>(
            totalCount,
            ObjectMapper.Map<List<Book>, List<BookDto>>(books)
        );
    }

    public async Task<BookDto> GetAsync(Guid id)
    {
        var book = await _bookRepository.GetAsync(id);
        var author = await _authorRepository.GetAsync(book.AuthorId);
        var bookDto = ObjectMapper.Map<Book, BookDto>(book);
        bookDto.AuthorName = author.Name;
        return bookDto;
    }

    public async Task<PagedResultDto<BookDto>> GetListAsync(GetBookListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Book.Name);
        }

        var books = await _bookRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var authors = await _authorRepository.GetListAsync();

        var totalCount = input.Filter == null
            ? await _bookRepository.CountAsync()
            : await _bookRepository.CountAsync(
                author => author.Name.Contains(input.Filter));

        var res = ObjectMapper.Map<List<Book>, List<BookDto>>(books);
        foreach (var item in res)
        {
            var author = await _authorRepository.GetAsync(item.AuthorId);
            item.AuthorName = author.Name;
        }
        return new PagedResultDto<BookDto>(
            totalCount,
            res
        );
    }

    public async Task UpdateAsync(string id, CreateUpdateBookDto input)
    {
        var books = await _bookRepository.GetListAsync(b => b.BookId == id);
        foreach (var book in books)
        {
            book.Name = input.Name;
            book.BookType = input.BookType;
            book.AuthorId = input.AuthorId;
            book.Quantity = input.Quantity;
            book.MaxReturnTime = input.MaxReturnTime;
            book.Price = input.Price;
            book.PublishDate = input.PublishDate;
            await _bookRepository.UpdateAsync(book);
        }
    }

    public async Task ChangeStatusAsync(Guid id, Guid IdCheckout)
    {
        var book = await _bookRepository.GetAsync(id);
        if (book.IsBorrowed == false)
        {
            book.Quantity -= 1;
        }
        else
        {
            book.Quantity += 1;
            var check = await CheckFinished(IdCheckout);
            if (check == true)
            {
                await UpdateStatus(IdCheckout, check);
            }
        }
        book.IsBorrowed = !book.IsBorrowed;

        await _bookRepository.UpdateAsync(book);
    }

    public async Task UpdateStatus(Guid id, bool finish)
    {
        var checkout = await _checkoutRepository.GetAsync(id);
        checkout.IsFinished = finish;
        await _checkoutRepository.UpdateAsync(checkout);
    }

    public async Task<bool> CheckFinished(Guid IdCheckout)
    {
        var checkoutDetails = await _checkoutDetailRepository.GetListAsync(x => x.CheckoutId == IdCheckout);
        int borrowNum = 0;
        foreach (var item in checkoutDetails)
        {
            var book = await GetAsync(item.BookId);
            if (book.IsBorrowed == false)
            {
                borrowNum++;
            }
        }
        if (borrowNum == checkoutDetails.Count)
        {
            return true;
        }
        return false;
    }

    public async Task ChangeQuantity(Guid id, int input)
    {
        var book = await _bookRepository.GetAsync(id);
        book.Quantity = input;
        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await GetAsync(id);
        var books = await _bookRepository.GetListAsync(b => b.BookId == book.BookId);
        int quantity = book.Quantity - 1;
        foreach (var item in books)
        {
            await ChangeQuantity(item.Id, quantity);
        }
        await _bookRepository.DeleteAsync(id);
    }

    public async Task DeleteBookAsync(String id)
    {
        var books = await _bookRepository.GetListAsync(b => b.BookId == id);
        foreach (var book in books)
        {
            await _bookRepository.DeleteAsync(book.Id);
        }
    }

    public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
    {
        var authors = await _authorRepository.GetListAsync();

        return new ListResultDto<AuthorLookupDto>(
            ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
        );
    }
}