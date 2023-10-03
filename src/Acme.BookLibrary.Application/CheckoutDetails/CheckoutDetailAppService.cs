using Acme.BookLibrary.Books;
using Acme.BookLibrary.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookLibrary.CheckoutDetails;

public class CheckoutDetailAppService : BookLibraryAppService, ICheckoutDetailService
{
    private readonly ICheckoutDetailRepository _checkoutDetailRepository;
    private readonly BookAppService _bookAppService;

    public CheckoutDetailAppService(ICheckoutDetailRepository checkoutDetailRepository, BookAppService bookAppService)
    {
        _checkoutDetailRepository = checkoutDetailRepository;
        _bookAppService = bookAppService;
    }

    public async Task<CheckoutDetailDto> CreateAsync(CreateUpdateCheckoutDetailDto input)
    {
        var checkoutDetail = new CheckoutDetail()
        {
            BookId = input.BookId,
            CheckoutId = input.CheckoutId,
        };

        var book = await _bookAppService.GetAsync(input.BookId);

        await _bookAppService.ChangeStatusAsync(book.Id, input.CheckoutId);
        await _checkoutDetailRepository.InsertAsync(checkoutDetail);
        return ObjectMapper.Map<CheckoutDetail, CheckoutDetailDto>(checkoutDetail);
    }

    // Kiểm tra cần trả mấy cuốn nữa

    public async Task DeleteAsync(Guid id)
    {
        await _bookAppService.DeleteAsync(id);
    }

    public async Task<CheckoutDetailDto> GetAsync(Guid id)
    {
        var checkoutDetail = await _checkoutDetailRepository.GetAsync(id);
        var book = await _bookAppService.GetAsync(checkoutDetail.BookId);
        var checkoutDetailDto = ObjectMapper.Map<CheckoutDetail, CheckoutDetailDto>(checkoutDetail);
        checkoutDetailDto.BookName = book.Name;
        return checkoutDetailDto;
    }

    public Task<PagedResultDto<CheckoutDetailDto>> GetListAsync(GetCheckoutListDto input)
    {
        /*if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Author.Name);
        }*/

        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, CreateUpdateCheckoutDetailDto input)
    {
        throw new NotImplementedException();
    }
}