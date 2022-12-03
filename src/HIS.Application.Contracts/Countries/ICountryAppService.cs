using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Countries
{
    public interface ICountryAppService : IApplicationService
    {
        Task<CountryDto> CreateAsync(CountryDto input);

        Task<PagedResultDto<CountryDto>> GetListAsync(GetCountryListDto input);
        Task<CountryDto> GetAsync(Guid id);

        Task<CountryDto> UpdateAsync(Guid id, CountryDto model);

        Task DeleteAsync(Guid id);
    }
}
