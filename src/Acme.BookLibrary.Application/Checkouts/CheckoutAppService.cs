using Acme.BookLibrary.Books;
using Acme.BookLibrary.CheckoutDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookLibrary.Checkouts;

public class CheckoutAppService : BookLibraryAppService, ICheckoutAppService
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly CheckoutDetailAppService _checkoutDetailAppService;
    private readonly ICheckoutDetailRepository _checkoutDetailRepository;
    private readonly IBookRepository _bookRepository;

    public CheckoutAppService(ICheckoutRepository checkoutRepository, CheckoutDetailAppService checkoutDetailAppService, ICheckoutDetailRepository checkoutDetailRepository, IBookRepository bookRepository)
    {
        _checkoutRepository = checkoutRepository;
        _checkoutDetailAppService = checkoutDetailAppService;
        _checkoutDetailRepository = checkoutDetailRepository;
        _bookRepository = bookRepository;
    }

    public async Task<CheckoutDto> CreateAsync(CreateUpdateCheckoutDto input)
    {
        var checkout = new Checkout(GuidGenerator.Create(), input.CardId, input.Deposit, input.IsFinished);
        // Kiểm tra đã trả hết sách chưa, nếu rồi thì cho mượn típ

        if (input.CheckoutDetails.Count > 3)
        {
            return null;
        }
        var ck = await _checkoutRepository.InsertAsync(checkout);
        var checkoutDto = ObjectMapper.Map<Checkout, CheckoutDto>(checkout);
        checkoutDto.CheckoutDetails = new List<CheckoutDetailDto>();
        foreach (var item in input.CheckoutDetails)
        {
            item.CheckoutId = ck.Id;
            var d = await _checkoutDetailAppService.CreateAsync(item);
            checkoutDto.CheckoutDetails.Add(d);
        }

        /*CheckoutDto res = await GetAsync(checkout.Id);*/
        return checkoutDto;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _checkoutRepository.DeleteAsync(id);
    }

    public async Task<CheckoutDto> GetAsync(Guid id)
    {
        var queryable = await _checkoutRepository.GetQueryableAsync();
        var checkoutDetails = await _checkoutDetailRepository.GetQueryableAsync();
        var book = await _bookRepository.GetQueryableAsync();

        //lamda
        var query = queryable.Where(q => q.Id == id).Select(c => new CheckoutDto()
        {
            CardId = c.CardId,
            Id = c.Id,
            CreationTime = c.CreationTime,
            Deposit = c.Deposit,
            CheckoutDetails = checkoutDetails
                            .Where(cdt => cdt.CheckoutId == c.Id)
                            .Select(cdt => new CheckoutDetailDto()
                            {
                                CheckoutId = cdt.CheckoutId,
                                Id = cdt.Id,
                                BookId = cdt.BookId,
                                ReturnDate = cdt.ReturnDate,
                                BookName = book.Where(b => b.Id == cdt.BookId).Select(b => b.Name).FirstOrDefault(),
                                IsBorrowed = book.Where(b => b.Id == cdt.BookId).Select(b => b.IsBorrowed).FirstOrDefault(),
                            })
                            .ToList() // ToList()
        });
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        return queryResult;
    }

    public async Task<PagedResultDto<CheckoutDto>> GetListAsync(GetCheckoutListDto input)
    {
        var totalCount = await _checkoutRepository.CountAsync();

        var queryable = await _checkoutRepository.GetQueryableAsync();
        var checkoutDetails = await _checkoutDetailRepository.GetQueryableAsync();
        var book = await _bookRepository.GetQueryableAsync();

        //lamda
        var query = queryable.Select(c => new CheckoutDto()
        {
            CardId = c.CardId,
            Id = c.Id,
            CreationTime = c.CreationTime,
            Deposit = c.Deposit,
            CheckoutDetails = checkoutDetails
                            .Where(cdt => cdt.CheckoutId == c.Id)
                            .Select(cdt => new CheckoutDetailDto()
                            {
                                CheckoutId = cdt.CheckoutId,
                                Id = cdt.Id,
                                BookId = cdt.BookId,
                                ReturnDate = cdt.ReturnDate,
                                BookName = book.Where(b => b.Id == cdt.BookId).Select(b => b.Name).FirstOrDefault(),
                                IsBorrowed = book.Where(b => b.Id == cdt.BookId).Select(b => b.IsBorrowed).FirstOrDefault(),
                            })
                            .ToList() // ToList() để chuyển kết quả thành danh sách
        });
        var queryResult = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<CheckoutDto>(
            totalCount,
            /*ObjectMapper.Map<List<Checkout>, List<CheckoutDto>>(checkouts)*/
            queryResult
        );
    }

    public Task UpdateAsync(Guid id, CreateUpdateCheckoutDto input)
    {
        throw new NotImplementedException();
    }
}