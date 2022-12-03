using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.InActiveReasons
{
    public class InActiveReasonAppService : HISAppService, IInActiveReasonAppService
    {
        private readonly IInActiveReasonRepository _iInActiveReasonRepository;
        public InActiveReasonAppService(IInActiveReasonRepository iInActiveReasonRepository)
        {
            _iInActiveReasonRepository = iInActiveReasonRepository;
        }
        public async Task<InActiveReasonDto> CreateAsync(InActiveReasonDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var InActiveReasonDefaultIndicatorTrue = await _iInActiveReasonRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (InActiveReasonDefaultIndicatorTrue != null)
                {
                    InActiveReasonDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _iInActiveReasonRepository.UpdateAsync(InActiveReasonDefaultIndicatorTrue);
                }

            }

            var InActiveReason = ObjectMapper.Map<InActiveReasonDto, InActiveReason>(input);

            var createdInActiveReason = await _iInActiveReasonRepository.InsertAsync(InActiveReason);

            return ObjectMapper.Map<InActiveReason, InActiveReasonDto>(createdInActiveReason);
        }

        public async Task<PagedResultDto<InActiveReasonDto>> GetListAsync(GetInActiveReasonListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(InActiveReason.Sequence);
            }

            var InActiveReasonQueryable = await _iInActiveReasonRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                InActiveReasonQueryable = InActiveReasonQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.InActiveReasonSearch != null)
            {
                if (!string.IsNullOrEmpty(input.InActiveReasonSearch.Name))
                {
                    InActiveReasonQueryable = InActiveReasonQueryable.Where(i => i.Name.Contains(input.InActiveReasonSearch.Name));
                }

                if (input.InActiveReasonSearch.Sequence != 0)
                {
                    InActiveReasonQueryable = InActiveReasonQueryable.Where(i => i.Sequence == input.InActiveReasonSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.InActiveReasonSearch.Description))
                {
                    InActiveReasonQueryable = InActiveReasonQueryable.Where(i => i.Description.Contains(input.InActiveReasonSearch.Description));
                }

                if (input.InActiveReasonSearch.InActiveStatus != Enums.InActiveStatusEnum.SelectAnOption)
                {
                    InActiveReasonQueryable = InActiveReasonQueryable.Where(i => i.InActiveStatus == input.InActiveReasonSearch.InActiveStatus);
                }
            }


            var InActiveReasons = InActiveReasonQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = InActiveReasonQueryable.Count();

            return new PagedResultDto<InActiveReasonDto>(
                totalCount,
                ObjectMapper.Map<List<InActiveReason>, List<InActiveReasonDto>>(InActiveReasons)
            );
        }

        public async Task<InActiveReasonDto> GetAsync(Guid id)
        {
            var InActiveReason = await _iInActiveReasonRepository.GetAsync(id);

            return ObjectMapper.Map<InActiveReason, InActiveReasonDto>(InActiveReason);
        }

        public async Task<InActiveReasonDto> UpdateAsync(Guid id, InActiveReasonDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var InActiveReasonDefaultIndicatorTrue = await _iInActiveReasonRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (InActiveReasonDefaultIndicatorTrue != null)
                {
                    InActiveReasonDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _iInActiveReasonRepository.UpdateAsync(InActiveReasonDefaultIndicatorTrue);
                }

            }

            var InActiveReason = await _iInActiveReasonRepository.GetAsync(id);

            InActiveReason.Name = model.Name;
            InActiveReason.Description = model.Description;
            InActiveReason.InActiveStatus = model.InActiveStatus;
            InActiveReason.Sequence = model.Sequence;
            InActiveReason.Comments = model.Comments;
            InActiveReason.IsDefaultIndicator = model.IsDefaultIndicator;
            InActiveReason.IsSystemIndicator = model.IsSystemIndicator;

            var updatedInActiveReason = await _iInActiveReasonRepository.UpdateAsync(InActiveReason);

            return ObjectMapper.Map<InActiveReason, InActiveReasonDto>(updatedInActiveReason);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _iInActiveReasonRepository.DeleteAsync(id);
        }

    }
}
