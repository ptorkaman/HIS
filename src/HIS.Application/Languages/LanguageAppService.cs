using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Languages
{
    public class LanguageAppService : HISAppService, ILanguageAppService
    {
        private readonly ILanguageRepository _languageRepository;
        public LanguageAppService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }
        public async Task<LanguageDto> CreateAsync(LanguageDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var LanguageDefaultIndicatorTrue = await _languageRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (LanguageDefaultIndicatorTrue != null)
                {
                    LanguageDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _languageRepository.UpdateAsync(LanguageDefaultIndicatorTrue);
                }

            }

            var Language = ObjectMapper.Map<LanguageDto, Language>(input);

            var createdLanguage = await _languageRepository.InsertAsync(Language);

            return ObjectMapper.Map<Language, LanguageDto>(createdLanguage);
        }

        public async Task<PagedResultDto<LanguageDto>> GetListAsync(GetLanguageListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Language.Sequence);
            }

            var LanguageQueryable = await _languageRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                LanguageQueryable = LanguageQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.LanguageSearch != null)
            {
                if (!string.IsNullOrEmpty(input.LanguageSearch.Name))
                {
                    LanguageQueryable = LanguageQueryable.Where(i => i.Name.Contains(input.LanguageSearch.Name));
                }

                if (input.LanguageSearch.Sequence != 0)
                {
                    LanguageQueryable = LanguageQueryable.Where(i => i.Sequence == input.LanguageSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.LanguageSearch.Description))
                {
                    LanguageQueryable = LanguageQueryable.Where(i => i.Description.Contains(input.LanguageSearch.Description));
                }
            }


            var Languages = LanguageQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = LanguageQueryable.Count();

            return new PagedResultDto<LanguageDto>(
                totalCount,
                ObjectMapper.Map<List<Language>, List<LanguageDto>>(Languages)
            );
        }

        public async Task<LanguageDto> GetAsync(Guid id)
        {
            var Language = await _languageRepository.GetAsync(id);

            return ObjectMapper.Map<Language, LanguageDto>(Language);
        }

        public async Task<LanguageDto> UpdateAsync(Guid id, LanguageDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var LanguageDefaultIndicatorTrue = await _languageRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (LanguageDefaultIndicatorTrue != null)
                {
                    LanguageDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _languageRepository.UpdateAsync(LanguageDefaultIndicatorTrue);
                }

            }

            var Language = await _languageRepository.GetAsync(id);

            Language.Name = model.Name;
            Language.Description = model.Description;
            Language.Sequence = model.Sequence;
            Language.Comments = model.Comments;
            Language.IsDefaultIndicator = model.IsDefaultIndicator;
            Language.IsSystemIndicator = model.IsSystemIndicator;

            var updatedLanguage = await _languageRepository.UpdateAsync(Language);

            return ObjectMapper.Map<Language, LanguageDto>(updatedLanguage);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _languageRepository.DeleteAsync(id);
        }

    }
}
