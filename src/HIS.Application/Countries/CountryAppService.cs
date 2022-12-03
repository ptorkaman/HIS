using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Countries
{
    public class CountryAppService : HISAppService, ICountryAppService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryAppService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<CountryDto> CreateAsync(CountryDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var countryDefaultIndicatorTrue = await _countryRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (countryDefaultIndicatorTrue != null)
                {
                    countryDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _countryRepository.UpdateAsync(countryDefaultIndicatorTrue);
                }

            }

            var Country = ObjectMapper.Map<CountryDto, Country>(input);

            var createdCountry = await _countryRepository.InsertAsync(Country);

            return ObjectMapper.Map<Country, CountryDto>(createdCountry);
        }

        public async Task<PagedResultDto<CountryDto>> GetListAsync(GetCountryListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Country.Sequence);
            }

            var CountryQueryable = await _countryRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                CountryQueryable = CountryQueryable.Where(i => i.Name.Contains(input.Filter) || i.TwoLettersIsoCode.Contains(input.Filter)  || i.ThreeLettersIsoCode.Contains(input.Filter) || i.NumericIsoCode.Contains(input.Filter));

            if (input.CountrySearch != null)
            {
                if (!string.IsNullOrEmpty(input.CountrySearch.Name))
                {
                    CountryQueryable = CountryQueryable.Where(i => i.Name.Contains(input.CountrySearch.Name));
                }

                if (input.CountrySearch.Sequence != 0)
                {
                    CountryQueryable = CountryQueryable.Where(i => i.Sequence == input.CountrySearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.CountrySearch.TwoLettersIsoCode))
                {
                    CountryQueryable = CountryQueryable.Where(i => i.TwoLettersIsoCode.Contains(input.CountrySearch.TwoLettersIsoCode));
                }

                if (!string.IsNullOrEmpty(input.CountrySearch.ThreeLettersIsoCode))
                {
                    CountryQueryable = CountryQueryable.Where(i => i.ThreeLettersIsoCode.Contains(input.CountrySearch.ThreeLettersIsoCode));
                }

                if (!string.IsNullOrEmpty(input.CountrySearch.NumericIsoCode))
                {
                    CountryQueryable = CountryQueryable.Where(i => i.NumericIsoCode.Contains(input.CountrySearch.NumericIsoCode));
                }
            }


            var Countrys = CountryQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = CountryQueryable.Count();

            return new PagedResultDto<CountryDto>(
                totalCount,
                ObjectMapper.Map<List<Country>, List<CountryDto>>(Countrys)
            );
        }

        public async Task<CountryDto> GetAsync(Guid id)
        {
            var Country = await _countryRepository.GetAsync(id);

            return ObjectMapper.Map<Country, CountryDto>(Country);
        }

        public async Task<CountryDto> UpdateAsync(Guid id, CountryDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var countryDefaultIndicatorTrue = await _countryRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (countryDefaultIndicatorTrue != null)
                {
                    countryDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _countryRepository.UpdateAsync(countryDefaultIndicatorTrue);
                }

            }

            var Country = await _countryRepository.GetAsync(id);

            Country.Name = model.Name;
            Country.TwoLettersIsoCode = model.TwoLettersIsoCode;
            Country.ThreeLettersIsoCode = model.ThreeLettersIsoCode;
            Country.NumericIsoCode = model.NumericIsoCode;
            Country.Sequence = model.Sequence;
            Country.IsDefaultIndicator = model.IsDefaultIndicator;
            Country.IsSystemIndicator = model.IsSystemIndicator;

            var updatedCountry = await _countryRepository.UpdateAsync(Country);

            return ObjectMapper.Map<Country, CountryDto>(updatedCountry);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _countryRepository.DeleteAsync(id);
        }

    }
}
