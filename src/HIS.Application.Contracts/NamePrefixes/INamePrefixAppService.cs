using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.NamePrefixes
{
    public interface INamePrefixAppService : IApplicationService
    {
        Task<NamePrefixDto> CreateAsync(NamePrefixDto input);

        Task<PagedResultDto<NamePrefixDto>> GetListAsync(GetNamePrefixListDto input);
        Task<NamePrefixDto> GetAsync(Guid id);

        Task<NamePrefixDto> UpdateAsync(Guid id, NamePrefixDto model);

        Task DeleteAsync(Guid id);
    }
}
