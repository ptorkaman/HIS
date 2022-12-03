using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Nationalities
{
    public class NationalityAppService : HISAppService, INationalityAppService
    {
        private readonly INationalityRepository _nationalityRepository;
        public NationalityAppService(INationalityRepository nationalityRepository)
        {
            _nationalityRepository = nationalityRepository;
        }
        public async Task<NationalityDto> CreateAsync(NationalityDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var NationalityDefaultIndicatorTrue = await _nationalityRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (NationalityDefaultIndicatorTrue != null)
                {
                    NationalityDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _nationalityRepository.UpdateAsync(NationalityDefaultIndicatorTrue);
                }

            }

            var Nationality = ObjectMapper.Map<NationalityDto, Nationality>(input);

            var createdNationality = await _nationalityRepository.InsertAsync(Nationality);

            return ObjectMapper.Map<Nationality, NationalityDto>(createdNationality);
        }

        public async Task<PagedResultDto<NationalityDto>> GetListAsync(GetNationalityListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Nationality.Sequence);
            }

            var NationalityQueryable = await _nationalityRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                NationalityQueryable = NationalityQueryable.Where(i => i.Name.Contains(input.Filter));

            if (input.NationalitySearch != null)
            {
                if (!string.IsNullOrEmpty(input.NationalitySearch.Name))
                {
                    NationalityQueryable = NationalityQueryable.Where(i => i.Name.Contains(input.NationalitySearch.Name));
                }

                if (input.NationalitySearch.Sequence != 0)
                {
                    NationalityQueryable = NationalityQueryable.Where(i => i.Sequence == input.NationalitySearch.Sequence);
                }
            }


            var Nationalitys = NationalityQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = NationalityQueryable.Count();

            return new PagedResultDto<NationalityDto>(
                totalCount,
                ObjectMapper.Map<List<Nationality>, List<NationalityDto>>(Nationalitys)
            );
        }

        public async Task<NationalityDto> GetAsync(Guid id)
        {
            var Nationality = await _nationalityRepository.GetAsync(id);

            return ObjectMapper.Map<Nationality, NationalityDto>(Nationality);
        }

        public async Task<NationalityDto> UpdateAsync(Guid id, NationalityDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var NationalityDefaultIndicatorTrue = await _nationalityRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (NationalityDefaultIndicatorTrue != null)
                {
                    NationalityDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _nationalityRepository.UpdateAsync(NationalityDefaultIndicatorTrue);
                }

            }

            var Nationality = await _nationalityRepository.GetAsync(id);

            Nationality.Name = model.Name;
            Nationality.Sequence = model.Sequence;
            Nationality.IsDefaultIndicator = model.IsDefaultIndicator;
            Nationality.IsSystemIndicator = model.IsSystemIndicator;

            var updatedNationality = await _nationalityRepository.UpdateAsync(Nationality);

            return ObjectMapper.Map<Nationality, NationalityDto>(updatedNationality);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _nationalityRepository.DeleteAsync(id);
        }

    }
}
