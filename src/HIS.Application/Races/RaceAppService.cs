using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Races
{
    public class RaceAppService : HISAppService, IRaceAppService
    {
        private readonly IRaceRepository _raceRepository;
        public RaceAppService(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<RaceDto> CreateAsync(RaceDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var RaceDefaultIndicatorTrue = await _raceRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (RaceDefaultIndicatorTrue != null)
                {
                    RaceDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _raceRepository.UpdateAsync(RaceDefaultIndicatorTrue);
                }

            }

            var Race = ObjectMapper.Map<RaceDto, Race>(input);

            var createdRace = await _raceRepository.InsertAsync(Race);

            return ObjectMapper.Map<Race, RaceDto>(createdRace);
        }

        public async Task<PagedResultDto<RaceDto>> GetListAsync(GetRaceListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Race.Sequence);
            }

            var RaceQueryable = await _raceRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                RaceQueryable = RaceQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.RaceSearch != null)
            {
                if (!string.IsNullOrEmpty(input.RaceSearch.Name))
                {
                    RaceQueryable = RaceQueryable.Where(i => i.Name.Contains(input.RaceSearch.Name));
                }

                if (input.RaceSearch.Sequence != 0)
                {
                    RaceQueryable = RaceQueryable.Where(i => i.Sequence == input.RaceSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.RaceSearch.Description))
                {
                    RaceQueryable = RaceQueryable.Where(i => i.Description.Contains(input.RaceSearch.Description));
                }
            }


            var Races = RaceQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = RaceQueryable.Count();

            return new PagedResultDto<RaceDto>(
                totalCount,
                ObjectMapper.Map<List<Race>, List<RaceDto>>(Races)
            );
        }

        public async Task<RaceDto> GetAsync(Guid id)
        {
            var Race = await _raceRepository.GetAsync(id);

            return ObjectMapper.Map<Race, RaceDto>(Race);
        }

        public async Task<RaceDto> UpdateAsync(Guid id, RaceDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var RaceDefaultIndicatorTrue = await _raceRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (RaceDefaultIndicatorTrue != null)
                {
                    RaceDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _raceRepository.UpdateAsync(RaceDefaultIndicatorTrue);
                }

            }

            var Race = await _raceRepository.GetAsync(id);

            Race.Name = model.Name;
            Race.Description = model.Description;
            Race.Sequence = model.Sequence;
            Race.Comments = model.Comments;
            Race.IsDefaultIndicator = model.IsDefaultIndicator;
            Race.IsSystemIndicator = model.IsSystemIndicator;

            var updatedRace = await _raceRepository.UpdateAsync(Race);

            return ObjectMapper.Map<Race, RaceDto>(updatedRace);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _raceRepository.DeleteAsync(id);
        }

    }
}
