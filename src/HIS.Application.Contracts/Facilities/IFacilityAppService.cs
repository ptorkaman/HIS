using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Facilities
{
    public interface IFacilityAppService : IApplicationService
    {
        Task<FacilityDto> CreateAsync(FacilityDto input);

        Task<PagedResultDto<FacilityDto>> GetListAsync(GetFacilityListDto input);
        Task<FacilityDto> GetAsync(Guid id);

        Task<FacilityDto> UpdateAsync(Guid id, FacilityDto model);

        Task DeleteAsync(Guid id);
    }
}
