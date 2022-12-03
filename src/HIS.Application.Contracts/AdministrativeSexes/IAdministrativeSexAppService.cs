using HIS.AdministrativeSexes;
using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.AdministrativeSexes
{
    public interface IAdministrativeSexAppService : IApplicationService
    {
        Task<AdministrativeSexDto> CreateAsync(AdministrativeSexDto input);

        Task<PagedResultDto<AdministrativeSexDto>> GetListAsync(GetAdministrativeSexListDto input);
        Task<AdministrativeSexDto> GetAsync(Guid id);

        Task<AdministrativeSexDto> UpdateAsync(Guid id, AdministrativeSexDto model);

        Task DeleteAsync(Guid id);
    }
}
