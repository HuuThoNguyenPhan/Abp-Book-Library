using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookLibrary.MemberCards;

public interface IMemberCardAppService : IApplicationService
{
    Task<MemberCardDto> CreateAsync(CreateUpdateMemberCardDto input);

    Task<MemberCardDto> GetAsync(Guid id);

    Task<PagedResultDto<MemberCardDto>> GetListAsync(GetMemberCardListDto input);

    Task UpdateAsync(Guid id, CreateUpdateMemberCardDto input);

    Task DeleteAsync(Guid id);
}