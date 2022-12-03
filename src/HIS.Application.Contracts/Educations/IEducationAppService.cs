using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Educations
{
    public interface IEducationAppService : IApplicationService
    {
        Task<EducationDto> CreateAsync(EducationDto input);

        Task<PagedResultDto<EducationDto>> GetListAsync(GetEducationListDto input);
        Task<EducationDto> GetAsync(Guid id);

        Task<EducationDto> UpdateAsync(Guid id, EducationDto model);

        Task DeleteAsync(Guid id);
    }
}
