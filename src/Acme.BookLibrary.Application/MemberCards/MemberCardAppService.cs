using Acme.BookLibrary.Authors;
using Acme.BookLibrary.Members;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookLibrary.MemberCards;

public class MemberCardAppService : BookLibraryAppService, IMemberCardAppService
{
    private readonly IMemberCardRepository _memberCardRepository;

    public MemberCardAppService(IMemberCardRepository memberCardRepository)
    {
        _memberCardRepository = memberCardRepository;
    }

    public async Task<MemberCardDto> CreateAsync(CreateUpdateMemberCardDto input)
    {
        var memberCard = new MemberCard(GuidGenerator.Create(), input.ExpDate, input.Name, input.Address, input.Email, input.Phone);
        await _memberCardRepository.InsertAsync(memberCard);
        return ObjectMapper.Map<MemberCard, MemberCardDto>(memberCard);
    }

    public async Task<MemberCardDto> GetAsync(Guid id)
    {
        var memberCard = await _memberCardRepository.GetAsync(id);
        return ObjectMapper.Map<MemberCard, MemberCardDto>(memberCard);
    }

    public async Task<PagedResultDto<MemberCardDto>> GetListAsync(GetMemberCardListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(MemberCard.Name);
        }

        var memberCards = await _memberCardRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _memberCardRepository.CountAsync()
            : await _memberCardRepository.CountAsync(
                author => author.Name.Contains(input.Filter));

        return new PagedResultDto<MemberCardDto>(
            totalCount,
            ObjectMapper.Map<List<MemberCard>, List<MemberCardDto>>(memberCards)
        );
    }

    public async Task UpdateAsync(Guid id, CreateUpdateMemberCardDto input)
    {
        var card = await _memberCardRepository.GetAsync(id);

        card.Name = input.Name;
        card.Address = input.Address;
        card.Email = input.Email;
        card.ExpDate = input.ExpDate;
        card.Phone = input.Phone;

        await _memberCardRepository.UpdateAsync(card);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _memberCardRepository.DeleteAsync(id);
    }
}