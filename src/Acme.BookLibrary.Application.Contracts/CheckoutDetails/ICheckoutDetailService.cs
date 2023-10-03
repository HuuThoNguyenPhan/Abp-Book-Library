using Acme.BookLibrary.Checkouts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookLibrary.CheckoutDetails;

public interface ICheckoutDetailService : IApplicationService
{
    Task<CheckoutDetailDto> GetAsync(Guid id);

    Task<PagedResultDto<CheckoutDetailDto>> GetListAsync(GetCheckoutListDto input);

    Task<CheckoutDetailDto> CreateAsync(CreateUpdateCheckoutDetailDto input);

    Task UpdateAsync(Guid id, CreateUpdateCheckoutDetailDto input);

    Task DeleteAsync(Guid id);
}