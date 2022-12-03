using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Religions
{
    public interface IReligionAppService : IApplicationService
    {
        Task<ReligionDto> CreateAsync(ReligionDto input);

        Task<PagedResultDto<ReligionDto>> GetListAsync(GetReligionListDto input);
        Task<ReligionDto> GetAsync(Guid id);

        Task<ReligionDto> UpdateAsync(Guid id, ReligionDto model);

        Task DeleteAsync(Guid id);
    }
}
