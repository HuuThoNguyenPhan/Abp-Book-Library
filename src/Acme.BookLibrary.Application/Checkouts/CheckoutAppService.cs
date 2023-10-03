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

    public CheckoutAppService(ICheckoutRepository checkoutRepository, CheckoutDetailAppService checkoutDetailAppService)
    {
        _checkoutRepository = checkoutRepository;
        _checkoutDetailAppService = checkoutDetailAppService;
    }

    public async Task<CheckoutDto> CreateAsync(CreateUpdateCheckoutDto input)
    {
        var checkout = new Checkout(GuidGenerator.Create(), input.CardId, input.Deposit, input.IsFinished);
        // Kiểm tra đã trả hết sách chưa, nếu rồi thì cho mượn típ
        await _checkoutRepository.InsertAsync(checkout);
        if (input.CheckoutDetails.Count > 3)
        {
            return null;
        }

        foreach (var item in input.CheckoutDetails)
        {
            item.CheckoutId = checkout.Id;
            await _checkoutDetailAppService.CreateAsync(item);
        }

        return ObjectMapper.Map<Checkout, CheckoutDto>(checkout);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _checkoutRepository.DeleteAsync(id);
    }

    public Task<CheckoutDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResultDto<CheckoutDto>> GetListAsync(GetCheckoutListDto input)
    {
        /*if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Author.Name);
        }*/

        var checkouts = await _checkoutRepository.GetListAsync();

        var totalCount = await _checkoutRepository.CountAsync();

        return new PagedResultDto<CheckoutDto>(
            totalCount,
            ObjectMapper.Map<List<Checkout>, List<CheckoutDto>>(checkouts)
        );
    }

    public Task UpdateAsync(Guid id, CreateUpdateCheckoutDto input)
    {
        throw new NotImplementedException();
    }
}