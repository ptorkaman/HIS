using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.Relationships
{
    public interface IRelationshipAppService : IApplicationService
    {
        Task<RelationshipDto> CreateAsync(RelationshipDto input);

        Task<PagedResultDto<RelationshipDto>> GetListAsync(GetRelationshipListDto input);
        Task<RelationshipDto> GetAsync(Guid id);

        Task<RelationshipDto> UpdateAsync(Guid id, RelationshipDto model);

        Task DeleteAsync(Guid id);
    }
}
