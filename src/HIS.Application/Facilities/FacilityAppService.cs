using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Facilities
{
    public class FacilityAppService : HISAppService, IFacilityAppService
    {
        private readonly IFacilityRepository _facilityRepository;
        public FacilityAppService(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }
        public async Task<FacilityDto> CreateAsync(FacilityDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var FacilityDefaultIndicatorTrue = await _facilityRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (FacilityDefaultIndicatorTrue != null)
                {
                    FacilityDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _facilityRepository.UpdateAsync(FacilityDefaultIndicatorTrue);
                }

            }

            var Facility = ObjectMapper.Map<FacilityDto, Facility>(input);

            var createdFacility = await _facilityRepository.InsertAsync(Facility);

            return ObjectMapper.Map<Facility, FacilityDto>(createdFacility);
        }

        public async Task<PagedResultDto<FacilityDto>> GetListAsync(GetFacilityListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Facility.Sequence);
            }

            var FacilityQueryable = await _facilityRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                FacilityQueryable = FacilityQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.FacilitySearch != null)
            {
                if (!string.IsNullOrEmpty(input.FacilitySearch.Name))
                {
                    FacilityQueryable = FacilityQueryable.Where(i => i.Name.Contains(input.FacilitySearch.Name));
                }

                if (input.FacilitySearch.Sequence != 0)
                {
                    FacilityQueryable = FacilityQueryable.Where(i => i.Sequence == input.FacilitySearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.FacilitySearch.Description))
                {
                    FacilityQueryable = FacilityQueryable.Where(i => i.Description.Contains(input.FacilitySearch.Description));
                }
            }


            var Facilitys = FacilityQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = FacilityQueryable.Count();

            return new PagedResultDto<FacilityDto>(
                totalCount,
                ObjectMapper.Map<List<Facility>, List<FacilityDto>>(Facilitys)
            );
        }

        public async Task<FacilityDto> GetAsync(Guid id)
        {
            var Facility = await _facilityRepository.GetAsync(id);

            return ObjectMapper.Map<Facility, FacilityDto>(Facility);
        }

        public async Task<FacilityDto> UpdateAsync(Guid id, FacilityDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var FacilityDefaultIndicatorTrue = await _facilityRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (FacilityDefaultIndicatorTrue != null)
                {
                    FacilityDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _facilityRepository.UpdateAsync(FacilityDefaultIndicatorTrue);
                }

            }

            var Facility = await _facilityRepository.GetAsync(id);

            Facility.Name = model.Name;
            Facility.Description = model.Description;
            Facility.Sequence = model.Sequence;
            Facility.Comments = model.Comments;
            Facility.IsDefaultIndicator = model.IsDefaultIndicator;
            Facility.IsSystemIndicator = model.IsSystemIndicator;

            var updatedFacility = await _facilityRepository.UpdateAsync(Facility);

            return ObjectMapper.Map<Facility, FacilityDto>(updatedFacility);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _facilityRepository.DeleteAsync(id);
        }

    }
}
