using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using HIS.AdministrativeSexes;

namespace HIS.AdministrativeSexes
{
    public class AdministrativeSexAppService : HISAppService, IAdministrativeSexAppService
    {
        private readonly IAdministrativeSexRepository _administrativeSexRepository;
        public AdministrativeSexAppService(IAdministrativeSexRepository administrativeSexRepository)
        {
            _administrativeSexRepository = administrativeSexRepository;
        }
        public async Task<AdministrativeSexDto> CreateAsync(AdministrativeSexDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var administrativeSexIsDefaultIndicatorTrue = await _administrativeSexRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (administrativeSexIsDefaultIndicatorTrue != null)
                {
                    administrativeSexIsDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _administrativeSexRepository.UpdateAsync(administrativeSexIsDefaultIndicatorTrue);
                }

            }

            var AdministrativeSex = ObjectMapper.Map<AdministrativeSexDto, AdministrativeSex>(input);

            var createdAdministrativeSex = await _administrativeSexRepository.InsertAsync(AdministrativeSex);

            return ObjectMapper.Map<AdministrativeSex, AdministrativeSexDto>(createdAdministrativeSex);
        }

        public async Task<PagedResultDto<AdministrativeSexDto>> GetListAsync(GetAdministrativeSexListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(AdministrativeSex.Sequence);
            }

            var administrativeSexQueryable = await _administrativeSexRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                administrativeSexQueryable = administrativeSexQueryable.Where(i => i.ShortDescription.Contains(input.Filter) || i.LongDescription.Contains(input.Filter));

            if (input.AdministrativeSearch != null)
            {
                if (!string.IsNullOrEmpty(input.AdministrativeSearch.ShortDescription))
                {
                    administrativeSexQueryable = administrativeSexQueryable.Where(i => i.ShortDescription.Contains(input.AdministrativeSearch.ShortDescription));
                }

                if (input.AdministrativeSearch.Sequence != 0)
                {
                    administrativeSexQueryable = administrativeSexQueryable.Where(i => i.Sequence == input.AdministrativeSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.AdministrativeSearch.LongDescription))
                {
                    administrativeSexQueryable = administrativeSexQueryable.Where(i => i.LongDescription.Contains(input.AdministrativeSearch.LongDescription));
                }
            }


            var AdministrativeSexs = administrativeSexQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = administrativeSexQueryable.Count();

            return new PagedResultDto<AdministrativeSexDto>(
                totalCount,
                ObjectMapper.Map<List<AdministrativeSex>, List<AdministrativeSexDto>>(AdministrativeSexs)
            );
        }

        public async Task<AdministrativeSexDto> GetAsync(Guid id)
        {
            var AdministrativeSex = await _administrativeSexRepository.GetAsync(id);

            return ObjectMapper.Map<AdministrativeSex, AdministrativeSexDto>(AdministrativeSex);
        }

        public async Task<AdministrativeSexDto> UpdateAsync(Guid id, AdministrativeSexDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var administrativeSexIsDefaultIndicatorTrue = await _administrativeSexRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (administrativeSexIsDefaultIndicatorTrue != null)
                {
                    administrativeSexIsDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _administrativeSexRepository.UpdateAsync(administrativeSexIsDefaultIndicatorTrue);
                }

            }

            var administrativeSex = await _administrativeSexRepository.GetAsync(id);

            administrativeSex.ShortDescription = model.ShortDescription;
            administrativeSex.LongDescription = model.LongDescription;
            administrativeSex.Sequence = model.Sequence;
            administrativeSex.Comments = model.Comments;
            administrativeSex.IsDefaultIndicator = model.IsDefaultIndicator;
            administrativeSex.IsSystemIndicator = model.IsSystemIndicator;

            var updatedAdministrativeSex = await _administrativeSexRepository.UpdateAsync(administrativeSex);

            return ObjectMapper.Map<AdministrativeSex, AdministrativeSexDto>(updatedAdministrativeSex);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _administrativeSexRepository.DeleteAsync(id);
        }

    }
}
