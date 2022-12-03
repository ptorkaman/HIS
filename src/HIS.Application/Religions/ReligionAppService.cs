using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Religions
{
    public class ReligionAppService : HISAppService, IReligionAppService
    {
        private readonly IReligionRepository _religionRepository;
        public ReligionAppService(IReligionRepository religionRepository)
        {
            _religionRepository = religionRepository;
        }
        public async Task<ReligionDto> CreateAsync(ReligionDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var ReligionDefaultIndicatorTrue = await _religionRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (ReligionDefaultIndicatorTrue != null)
                {
                    ReligionDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _religionRepository.UpdateAsync(ReligionDefaultIndicatorTrue);
                }

            }

            var Religion = ObjectMapper.Map<ReligionDto, Religion>(input);

            var createdReligion = await _religionRepository.InsertAsync(Religion);

            return ObjectMapper.Map<Religion, ReligionDto>(createdReligion);
        }

        public async Task<PagedResultDto<ReligionDto>> GetListAsync(GetReligionListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Religion.Sequence);
            }

            var ReligionQueryable = await _religionRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                ReligionQueryable = ReligionQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.ReligionSearch != null)
            {
                if (!string.IsNullOrEmpty(input.ReligionSearch.Name))
                {
                    ReligionQueryable = ReligionQueryable.Where(i => i.Name.Contains(input.ReligionSearch.Name));
                }

                if (input.ReligionSearch.Sequence != 0)
                {
                    ReligionQueryable = ReligionQueryable.Where(i => i.Sequence == input.ReligionSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.ReligionSearch.Description))
                {
                    ReligionQueryable = ReligionQueryable.Where(i => i.Description.Contains(input.ReligionSearch.Description));
                }
            }


            var Religions = ReligionQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = ReligionQueryable.Count();

            return new PagedResultDto<ReligionDto>(
                totalCount,
                ObjectMapper.Map<List<Religion>, List<ReligionDto>>(Religions)
            );
        }

        public async Task<ReligionDto> GetAsync(Guid id)
        {
            var Religion = await _religionRepository.GetAsync(id);

            return ObjectMapper.Map<Religion, ReligionDto>(Religion);
        }

        public async Task<ReligionDto> UpdateAsync(Guid id, ReligionDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var ReligionDefaultIndicatorTrue = await _religionRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (ReligionDefaultIndicatorTrue != null)
                {
                    ReligionDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _religionRepository.UpdateAsync(ReligionDefaultIndicatorTrue);
                }

            }

            var Religion = await _religionRepository.GetAsync(id);

            Religion.Name = model.Name;
            Religion.Description = model.Description;
            Religion.Sequence = model.Sequence;
            Religion.Comments = model.Comments;
            Religion.IsDefaultIndicator = model.IsDefaultIndicator;
            Religion.IsSystemIndicator = model.IsSystemIndicator;

            var updatedReligion = await _religionRepository.UpdateAsync(Religion);

            return ObjectMapper.Map<Religion, ReligionDto>(updatedReligion);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _religionRepository.DeleteAsync(id);
        }

    }
}
