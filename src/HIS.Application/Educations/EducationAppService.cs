using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Educations
{
    public class EducationAppService : HISAppService, IEducationAppService
    {
        private readonly IEducationRepository _educationRepository;
        public EducationAppService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }
        public async Task<EducationDto> CreateAsync(EducationDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var EducationDefaultIndicatorTrue = await _educationRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (EducationDefaultIndicatorTrue != null)
                {
                    EducationDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _educationRepository.UpdateAsync(EducationDefaultIndicatorTrue);
                }

            }

            var Education = ObjectMapper.Map<EducationDto, Education>(input);

            var createdEducation = await _educationRepository.InsertAsync(Education);

            return ObjectMapper.Map<Education, EducationDto>(createdEducation);
        }

        public async Task<PagedResultDto<EducationDto>> GetListAsync(GetEducationListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Education.Sequence);
            }

            var EducationQueryable = await _educationRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                EducationQueryable = EducationQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.EducationSearch != null)
            {
                if (!string.IsNullOrEmpty(input.EducationSearch.Name))
                {
                    EducationQueryable = EducationQueryable.Where(i => i.Name.Contains(input.EducationSearch.Name));
                }

                if (input.EducationSearch.Sequence != 0)
                {
                    EducationQueryable = EducationQueryable.Where(i => i.Sequence == input.EducationSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.EducationSearch.Description))
                {
                    EducationQueryable = EducationQueryable.Where(i => i.Description.Contains(input.EducationSearch.Description));
                }
            }


            var Educations = EducationQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = EducationQueryable.Count();

            return new PagedResultDto<EducationDto>(
                totalCount,
                ObjectMapper.Map<List<Education>, List<EducationDto>>(Educations)
            );
        }

        public async Task<EducationDto> GetAsync(Guid id)
        {
            var Education = await _educationRepository.GetAsync(id);

            return ObjectMapper.Map<Education, EducationDto>(Education);
        }

        public async Task<EducationDto> UpdateAsync(Guid id, EducationDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var EducationDefaultIndicatorTrue = await _educationRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (EducationDefaultIndicatorTrue != null)
                {
                    EducationDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _educationRepository.UpdateAsync(EducationDefaultIndicatorTrue);
                }

            }

            var Education = await _educationRepository.GetAsync(id);

            Education.Name = model.Name;
            Education.Description = model.Description;
            Education.Sequence = model.Sequence;
            Education.Comments = model.Comments;
            Education.IsDefaultIndicator = model.IsDefaultIndicator;
            Education.IsSystemIndicator = model.IsSystemIndicator;

            var updatedEducation = await _educationRepository.UpdateAsync(Education);

            return ObjectMapper.Map<Education, EducationDto>(updatedEducation);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _educationRepository.DeleteAsync(id);
        }

    }
}
