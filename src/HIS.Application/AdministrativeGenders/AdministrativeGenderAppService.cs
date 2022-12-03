using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using HIS.AdministrativeGenders;

namespace HIS.AdministrativeGenders
{
    public class AdministrativeGenderAppService : HISAppService, IAdministrativeGenderAppService
    {
        private readonly IAdministrativeGenderRepository _administrativeGenderRepository;
        public AdministrativeGenderAppService(IAdministrativeGenderRepository administrativeGenderRepository)
        {
            _administrativeGenderRepository = administrativeGenderRepository;
        }
        public async Task<AdministrativeGenderDto> CreateAsync(AdministrativeGenderDto input)
        {
            try
            {
                if (input.IsDefaultIndicator)
                {
                    var administrativeGenderIsDefaultIndicatorTrue = await _administrativeGenderRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                    if (administrativeGenderIsDefaultIndicatorTrue != null)
                    {
                        administrativeGenderIsDefaultIndicatorTrue.IsDefaultIndicator = false;
                        await _administrativeGenderRepository.UpdateAsync(administrativeGenderIsDefaultIndicatorTrue);
                    }

                }

                var AdministrativeGender = ObjectMapper.Map<AdministrativeGenderDto, AdministrativeGender>(input);

                var createdAdministrativeGender = await _administrativeGenderRepository.InsertAsync(AdministrativeGender);

                return ObjectMapper.Map<AdministrativeGender, AdministrativeGenderDto>(createdAdministrativeGender);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<PagedResultDto<AdministrativeGenderDto>> GetListAsync(GetAdministrativeGenderListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(AdministrativeGender.Sequence);
            }

            var administrativeGenderQueryable = await _administrativeGenderRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                administrativeGenderQueryable = administrativeGenderQueryable.Where(i => i.Description.Contains(input.Filter) || i.Comments.Contains(input.Filter));

            if (input.AdministrativeSearch != null)
            {
                if (!string.IsNullOrEmpty(input.AdministrativeSearch.Description))
                {
                    administrativeGenderQueryable = administrativeGenderQueryable.Where(i => i.Description.Contains(input.AdministrativeSearch.Description));
                }

                if (input.AdministrativeSearch.Sequence != 0)
                {
                    administrativeGenderQueryable = administrativeGenderQueryable.Where(i => i.Sequence == input.AdministrativeSearch.Sequence);
                }
            }


            var AdministrativeGenders = administrativeGenderQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = administrativeGenderQueryable.Count();

            return new PagedResultDto<AdministrativeGenderDto>(
                totalCount,
                ObjectMapper.Map<List<AdministrativeGender>, List<AdministrativeGenderDto>>(AdministrativeGenders)
            );
        }

        public async Task<AdministrativeGenderDto> GetAsync(Guid id)
        {
            var AdministrativeGender = await _administrativeGenderRepository.GetAsync(id);

            return ObjectMapper.Map<AdministrativeGender, AdministrativeGenderDto>(AdministrativeGender);
        }

        public async Task<AdministrativeGenderDto> UpdateAsync(Guid id, AdministrativeGenderDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var administrativeGenderIsDefaultIndicatorTrue = await _administrativeGenderRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (administrativeGenderIsDefaultIndicatorTrue != null)
                {
                    administrativeGenderIsDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _administrativeGenderRepository.UpdateAsync(administrativeGenderIsDefaultIndicatorTrue);
                }

            }

            var AdministrativeGender = await _administrativeGenderRepository.GetAsync(id);

            AdministrativeGender.Description = model.Description;
            AdministrativeGender.Comments = model.Comments;
            AdministrativeGender.Sequence = model.Sequence;
            AdministrativeGender.IsDefaultIndicator = model.IsDefaultIndicator;
            AdministrativeGender.IsSystemIndicator = model.IsSystemIndicator;

            var updatedAdministrativeGender = await _administrativeGenderRepository.UpdateAsync(AdministrativeGender);

            return ObjectMapper.Map<AdministrativeGender, AdministrativeGenderDto>(updatedAdministrativeGender);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _administrativeGenderRepository.DeleteAsync(id);
        }

    }
}
