using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.NamePrefixes
{
    public class NamePrefixAppService : HISAppService, INamePrefixAppService
    {
        private readonly INamePrefixRepository _namePrefixRepository;
        public NamePrefixAppService(INamePrefixRepository namePrefixRepository)
        {
            _namePrefixRepository = namePrefixRepository;
        }
        public async Task<NamePrefixDto> CreateAsync(NamePrefixDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var NamePrefixDefaultIndicatorTrue = await _namePrefixRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (NamePrefixDefaultIndicatorTrue != null)
                {
                    NamePrefixDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _namePrefixRepository.UpdateAsync(NamePrefixDefaultIndicatorTrue);
                }

            }

            var NamePrefix = ObjectMapper.Map<NamePrefixDto, NamePrefix>(input);

            var createdNamePrefix = await _namePrefixRepository.InsertAsync(NamePrefix);

            return ObjectMapper.Map<NamePrefix, NamePrefixDto>(createdNamePrefix);
        }

        public async Task<PagedResultDto<NamePrefixDto>> GetListAsync(GetNamePrefixListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(NamePrefix.Sequence);
            }

            var NamePrefixQueryable = await _namePrefixRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                NamePrefixQueryable = NamePrefixQueryable
                    .Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.NamePrefixSearch != null)
            {
                if (!string.IsNullOrEmpty(input.NamePrefixSearch.Name))
                {
                    NamePrefixQueryable = NamePrefixQueryable.Where(i => i.Name.Contains(input.NamePrefixSearch.Name));
                }

                if (input.NamePrefixSearch.Sequence != 0)
                {
                    NamePrefixQueryable = NamePrefixQueryable.Where(i => i.Sequence == input.NamePrefixSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.NamePrefixSearch.Description))
                {
                    NamePrefixQueryable = NamePrefixQueryable.Where(i => i.Description.Contains(input.NamePrefixSearch.Description));
                }
            }


            var NamePrefixs = NamePrefixQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = NamePrefixQueryable.Count();

            return new PagedResultDto<NamePrefixDto>(
                totalCount,
                ObjectMapper.Map<List<NamePrefix>, List<NamePrefixDto>>(NamePrefixs)
            );
        }

        public async Task<NamePrefixDto> GetAsync(Guid id)
        {
            var NamePrefix = await _namePrefixRepository.GetAsync(id);

            return ObjectMapper.Map<NamePrefix, NamePrefixDto>(NamePrefix);
        }

        public async Task<NamePrefixDto> UpdateAsync(Guid id, NamePrefixDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var NamePrefixDefaultIndicatorTrue = await _namePrefixRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (NamePrefixDefaultIndicatorTrue != null)
                {
                    NamePrefixDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _namePrefixRepository.UpdateAsync(NamePrefixDefaultIndicatorTrue);
                }

            }

            var NamePrefix = await _namePrefixRepository.GetAsync(id);

            NamePrefix.Name = model.Name;
            NamePrefix.AdministrativeSexId = model.AdministrativeSexId;
            NamePrefix.Description = model.Description;
            NamePrefix.Sequence = model.Sequence;
            NamePrefix.Comments = model.Comments;
            NamePrefix.IsDefaultIndicator = model.IsDefaultIndicator;
            NamePrefix.IsSystemIndicator = model.IsSystemIndicator;

            var updatedNamePrefix = await _namePrefixRepository.UpdateAsync(NamePrefix);

            return ObjectMapper.Map<NamePrefix, NamePrefixDto>(updatedNamePrefix);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _namePrefixRepository.DeleteAsync(id);
        }

    }
}
