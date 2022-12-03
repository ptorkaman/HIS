using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Authors
{
    public interface IAuthorAppService : IApplicationService
    {
        Task<AuthorDto> CreateAsync(AuthorDto input);

        Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input);
        Task<AuthorDto> GetAsync(Guid id);

        Task<AuthorDto> UpdateAsync(Guid id, AuthorDto model);

        Task DeleteAsync(Guid id);
    }
}
