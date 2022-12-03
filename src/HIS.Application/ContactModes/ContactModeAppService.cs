using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.ContactModes
{
    public class ContactModeAppService : HISAppService, IContactModeAppService
    {
        private readonly IContactModeRepository _contactModeRepository;
        public ContactModeAppService(IContactModeRepository contactModeRepository)
        {
            _contactModeRepository = contactModeRepository;
        }
        public async Task<ContactModeDto> CreateAsync(ContactModeDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var contactModeDefaultIndicatorTrue = await _contactModeRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (contactModeDefaultIndicatorTrue != null)
                {
                    contactModeDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _contactModeRepository.UpdateAsync(contactModeDefaultIndicatorTrue);
                }

            }

            var ContactMode = ObjectMapper.Map<ContactModeDto, ContactMode>(input);

            var createdContactMode = await _contactModeRepository.InsertAsync(ContactMode);

            return ObjectMapper.Map<ContactMode, ContactModeDto>(createdContactMode);
        }

        public async Task<PagedResultDto<ContactModeDto>> GetListAsync(GetContactModeListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ContactMode.Sequence);
            }

            var contactModeQueryable = await _contactModeRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                contactModeQueryable = contactModeQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.ContactModeSearch != null)
            {
                if (!string.IsNullOrEmpty(input.ContactModeSearch.Name))
                {
                    contactModeQueryable = contactModeQueryable.Where(i => i.Name.Contains(input.ContactModeSearch.Name));
                }

                if (input.ContactModeSearch.Sequence != 0)
                {
                    contactModeQueryable = contactModeQueryable.Where(i => i.Sequence == input.ContactModeSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.ContactModeSearch.Description))
                {
                    contactModeQueryable = contactModeQueryable.Where(i => i.Description.Contains(input.ContactModeSearch.Description));
                }
            }


            var ContactModes = contactModeQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = contactModeQueryable.Count();

            return new PagedResultDto<ContactModeDto>(
                totalCount,
                ObjectMapper.Map<List<ContactMode>, List<ContactModeDto>>(ContactModes)
            );
        }

        public async Task<ContactModeDto> GetAsync(Guid id)
        {
            var ContactMode = await _contactModeRepository.GetAsync(id);

            return ObjectMapper.Map<ContactMode, ContactModeDto>(ContactMode);
        }

        public async Task<ContactModeDto> UpdateAsync(Guid id, ContactModeDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var contactModeDefaultIndicatorTrue = await _contactModeRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (contactModeDefaultIndicatorTrue != null)
                {
                    contactModeDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _contactModeRepository.UpdateAsync(contactModeDefaultIndicatorTrue);
                }

            }

            var ContactMode = await _contactModeRepository.GetAsync(id);

            ContactMode.Name = model.Name;
            ContactMode.Description = model.Description;
            ContactMode.Sequence = model.Sequence;
            ContactMode.Comments = model.Comments;
            ContactMode.IsDefaultIndicator = model.IsDefaultIndicator;
            ContactMode.IsSystemIndicator = model.IsSystemIndicator;

            var updatedContactMode = await _contactModeRepository.UpdateAsync(ContactMode);

            return ObjectMapper.Map<ContactMode, ContactModeDto>(updatedContactMode);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _contactModeRepository.DeleteAsync(id);
        }

    }
}
