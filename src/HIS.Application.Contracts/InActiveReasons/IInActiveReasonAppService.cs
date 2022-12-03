using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.InActiveReasons
{
    public interface IInActiveReasonAppService : IApplicationService
    {
        Task<InActiveReasonDto> CreateAsync(InActiveReasonDto input);

        Task<PagedResultDto<InActiveReasonDto>> GetListAsync(GetInActiveReasonListDto input);
        Task<InActiveReasonDto> GetAsync(Guid id);

        Task<InActiveReasonDto> UpdateAsync(Guid id, InActiveReasonDto model);

        Task DeleteAsync(Guid id);
    }
}
