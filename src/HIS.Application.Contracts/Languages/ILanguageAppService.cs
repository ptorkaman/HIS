using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Languages
{
    public interface ILanguageAppService : IApplicationService
    {
        Task<LanguageDto> CreateAsync(LanguageDto input);

        Task<PagedResultDto<LanguageDto>> GetListAsync(GetLanguageListDto input);
        Task<LanguageDto> GetAsync(Guid id);

        Task<LanguageDto> UpdateAsync(Guid id, LanguageDto model);

        Task DeleteAsync(Guid id);
    }
}
