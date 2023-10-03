using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookLibrary.Checkouts;

public interface ICheckoutAppService : IApplicationService
{
    Task<CheckoutDto> GetAsync(Guid id);

    Task<PagedResultDto<CheckoutDto>> GetListAsync(GetCheckoutListDto input);

    Task<CheckoutDto> CreateAsync(CreateUpdateCheckoutDto input);

    Task UpdateAsync(Guid id, CreateUpdateCheckoutDto input);

    Task DeleteAsync(Guid id);
}