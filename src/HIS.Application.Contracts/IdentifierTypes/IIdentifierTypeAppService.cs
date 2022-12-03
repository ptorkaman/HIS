using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.IdentifierTypes
{
    public interface IIdentifierTypeAppService : IApplicationService
    {
        Task<IdentifierTypeDto> CreateAsync(IdentifierTypeDto input);

        Task<PagedResultDto<IdentifierTypeDto>> GetListAsync(GetIdentifierTypeListDto input);
        Task<IdentifierTypeDto> GetAsync(Guid id);

        Task<IdentifierTypeDto> UpdateAsync(Guid id, IdentifierTypeDto model);

        Task DeleteAsync(Guid id);
    }
}
