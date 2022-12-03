using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.IdentifierTypes
{
    public class IdentifierTypeAppService : HISAppService, IIdentifierTypeAppService
    {
        private readonly IIdentifierTypeRepository _iIdentifierTypeRepository;
        public IdentifierTypeAppService(IIdentifierTypeRepository iIdentifierTypeRepository)
        {
            _iIdentifierTypeRepository = iIdentifierTypeRepository;
        }
        public async Task<IdentifierTypeDto> CreateAsync(IdentifierTypeDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var IdentifierTypeDefaultIndicatorTrue = await _iIdentifierTypeRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (IdentifierTypeDefaultIndicatorTrue != null)
                {
                    IdentifierTypeDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _iIdentifierTypeRepository.UpdateAsync(IdentifierTypeDefaultIndicatorTrue);
                }

            }

            var IdentifierType = ObjectMapper.Map<IdentifierTypeDto, IdentifierType>(input);

            var createdIdentifierType = await _iIdentifierTypeRepository.InsertAsync(IdentifierType);

            return ObjectMapper.Map<IdentifierType, IdentifierTypeDto>(createdIdentifierType);
        }

        public async Task<PagedResultDto<IdentifierTypeDto>> GetListAsync(GetIdentifierTypeListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(IdentifierType.Sequence);
            }

            var IdentifierTypeQueryable = await _iIdentifierTypeRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                IdentifierTypeQueryable = IdentifierTypeQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.IdentifierTypeSearch != null)
            {
                if (!string.IsNullOrEmpty(input.IdentifierTypeSearch.Name))
                {
                    IdentifierTypeQueryable = IdentifierTypeQueryable.Where(i => i.Name.Contains(input.IdentifierTypeSearch.Name));
                }

                if (input.IdentifierTypeSearch.Sequence != 0)
                {
                    IdentifierTypeQueryable = IdentifierTypeQueryable.Where(i => i.Sequence == input.IdentifierTypeSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.IdentifierTypeSearch.Description))
                {
                    IdentifierTypeQueryable = IdentifierTypeQueryable.Where(i => i.Description.Contains(input.IdentifierTypeSearch.Description));
                }

                if (!string.IsNullOrEmpty(input.IdentifierTypeSearch.Regex))
                {
                    IdentifierTypeQueryable = IdentifierTypeQueryable.Where(i => i.Description.Contains(input.IdentifierTypeSearch.Regex));
                }
            }


            var IdentifierTypes = IdentifierTypeQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = IdentifierTypeQueryable.Count();

            return new PagedResultDto<IdentifierTypeDto>(
                totalCount,
                ObjectMapper.Map<List<IdentifierType>, List<IdentifierTypeDto>>(IdentifierTypes)
            );
        }

        public async Task<IdentifierTypeDto> GetAsync(Guid id)
        {
            var IdentifierType = await _iIdentifierTypeRepository.GetAsync(id);

            return ObjectMapper.Map<IdentifierType, IdentifierTypeDto>(IdentifierType);
        }

        public async Task<IdentifierTypeDto> UpdateAsync(Guid id, IdentifierTypeDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var IdentifierTypeDefaultIndicatorTrue = await _iIdentifierTypeRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (IdentifierTypeDefaultIndicatorTrue != null)
                {
                    IdentifierTypeDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _iIdentifierTypeRepository.UpdateAsync(IdentifierTypeDefaultIndicatorTrue);
                }

            }

            var IdentifierType = await _iIdentifierTypeRepository.GetAsync(id);

            IdentifierType.Name = model.Name;
            IdentifierType.Description = model.Description;
            IdentifierType.Regex = model.Regex;
            IdentifierType.Sequence = model.Sequence;
            IdentifierType.Comments = model.Comments;
            IdentifierType.IsDefaultIndicator = model.IsDefaultIndicator;
            IdentifierType.IsSystemIndicator = model.IsSystemIndicator;

            var updatedIdentifierType = await _iIdentifierTypeRepository.UpdateAsync(IdentifierType);

            return ObjectMapper.Map<IdentifierType, IdentifierTypeDto>(updatedIdentifierType);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _iIdentifierTypeRepository.DeleteAsync(id);
        }

    }
}
