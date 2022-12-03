using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Nationalities
{
    public interface INationalityAppService : IApplicationService
    {
        Task<NationalityDto> CreateAsync(NationalityDto input);

        Task<PagedResultDto<NationalityDto>> GetListAsync(GetNationalityListDto input);
        Task<NationalityDto> GetAsync(Guid id);

        Task<NationalityDto> UpdateAsync(Guid id, NationalityDto model);

        Task DeleteAsync(Guid id);
    }
}
