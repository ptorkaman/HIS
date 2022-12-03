using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.MaritalStatuses
{
    public class MaritalStatusAppService : HISAppService, IMaritalStatusAppService
    {
        private readonly IMaritalStatusRepository _maritalStatusRepository;
        public MaritalStatusAppService(IMaritalStatusRepository maritalStatusRepository)
        {
            _maritalStatusRepository = maritalStatusRepository;
        }
        public async Task<MaritalStatusDto> CreateAsync(MaritalStatusDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var MaritalStatusDefaultIndicatorTrue = await _maritalStatusRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (MaritalStatusDefaultIndicatorTrue != null)
                {
                    MaritalStatusDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _maritalStatusRepository.UpdateAsync(MaritalStatusDefaultIndicatorTrue);
                }

            }

            var MaritalStatus = ObjectMapper.Map<MaritalStatusDto, MaritalStatus>(input);

            var createdMaritalStatus = await _maritalStatusRepository.InsertAsync(MaritalStatus);

            return ObjectMapper.Map<MaritalStatus, MaritalStatusDto>(createdMaritalStatus);
        }

        public async Task<PagedResultDto<MaritalStatusDto>> GetListAsync(GetMaritalStatusListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(MaritalStatus.Sequence);
            }

            var MaritalStatusQueryable = await _maritalStatusRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                MaritalStatusQueryable = MaritalStatusQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.MaritalStatusSearch != null)
            {
                if (!string.IsNullOrEmpty(input.MaritalStatusSearch.Name))
                {
                    MaritalStatusQueryable = MaritalStatusQueryable.Where(i => i.Name.Contains(input.MaritalStatusSearch.Name));
                }

                if (input.MaritalStatusSearch.Sequence != 0)
                {
                    MaritalStatusQueryable = MaritalStatusQueryable.Where(i => i.Sequence == input.MaritalStatusSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.MaritalStatusSearch.Description))
                {
                    MaritalStatusQueryable = MaritalStatusQueryable.Where(i => i.Description.Contains(input.MaritalStatusSearch.Description));
                }
            }


            var MaritalStatuss = MaritalStatusQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = MaritalStatusQueryable.Count();

            return new PagedResultDto<MaritalStatusDto>(
                totalCount,
                ObjectMapper.Map<List<MaritalStatus>, List<MaritalStatusDto>>(MaritalStatuss)
            );
        }

        public async Task<MaritalStatusDto> GetAsync(Guid id)
        {
            var MaritalStatus = await _maritalStatusRepository.GetAsync(id);

            return ObjectMapper.Map<MaritalStatus, MaritalStatusDto>(MaritalStatus);
        }

        public async Task<MaritalStatusDto> UpdateAsync(Guid id, MaritalStatusDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var MaritalStatusDefaultIndicatorTrue = await _maritalStatusRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (MaritalStatusDefaultIndicatorTrue != null)
                {
                    MaritalStatusDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _maritalStatusRepository.UpdateAsync(MaritalStatusDefaultIndicatorTrue);
                }

            }

            var MaritalStatus = await _maritalStatusRepository.GetAsync(id);

            MaritalStatus.Name = model.Name;
            MaritalStatus.Description = model.Description;
            MaritalStatus.Sequence = model.Sequence;
            MaritalStatus.Comments = model.Comments;
            MaritalStatus.IsDefaultIndicator = model.IsDefaultIndicator;
            MaritalStatus.IsSystemIndicator = model.IsSystemIndicator;

            var updatedMaritalStatus = await _maritalStatusRepository.UpdateAsync(MaritalStatus);

            return ObjectMapper.Map<MaritalStatus, MaritalStatusDto>(updatedMaritalStatus);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _maritalStatusRepository.DeleteAsync(id);
        }

    }
}
