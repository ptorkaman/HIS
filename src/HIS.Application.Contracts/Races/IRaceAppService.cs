using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Races
{
    public interface IRaceAppService : IApplicationService
    {
        Task<RaceDto> CreateAsync(RaceDto input);

        Task<PagedResultDto<RaceDto>> GetListAsync(GetRaceListDto input);
        Task<RaceDto> GetAsync(Guid id);

        Task<RaceDto> UpdateAsync(Guid id, RaceDto model);

        Task DeleteAsync(Guid id);
    }
}
