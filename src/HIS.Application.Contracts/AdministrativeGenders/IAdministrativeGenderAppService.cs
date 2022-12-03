using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.AdministrativeGenders
{
    public interface IAdministrativeGenderAppService : IApplicationService
    {
        Task<AdministrativeGenderDto> CreateAsync(AdministrativeGenderDto input);

        Task<PagedResultDto<AdministrativeGenderDto>> GetListAsync(GetAdministrativeGenderListDto input);
        Task<AdministrativeGenderDto> GetAsync(Guid id);

        Task<AdministrativeGenderDto> UpdateAsync(Guid id, AdministrativeGenderDto model);

        Task DeleteAsync(Guid id);
    }
}
