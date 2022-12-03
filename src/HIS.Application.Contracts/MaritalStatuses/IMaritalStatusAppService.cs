using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.MaritalStatuses
{
    public interface IMaritalStatusAppService : IApplicationService
    {
        Task<MaritalStatusDto> CreateAsync(MaritalStatusDto input);

        Task<PagedResultDto<MaritalStatusDto>> GetListAsync(GetMaritalStatusListDto input);
        Task<MaritalStatusDto> GetAsync(Guid id);

        Task<MaritalStatusDto> UpdateAsync(Guid id, MaritalStatusDto model);

        Task DeleteAsync(Guid id);
    }
}
