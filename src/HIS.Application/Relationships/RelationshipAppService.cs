using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Relationships
{
    public class RelationshipAppService : HISAppService, IRelationshipAppService
    {
        private readonly IRelationshipRepository _relationshipRepository;
        public RelationshipAppService(IRelationshipRepository relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<RelationshipDto> CreateAsync(RelationshipDto input)
        {
            if (input.IsDefaultIndicator)
            {
                var RelationshipDefaultIndicatorTrue = await _relationshipRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (RelationshipDefaultIndicatorTrue != null)
                {
                    RelationshipDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _relationshipRepository.UpdateAsync(RelationshipDefaultIndicatorTrue);
                }

            }

            var Relationship = ObjectMapper.Map<RelationshipDto, Relationship>(input);

            var createdRelationship = await _relationshipRepository.InsertAsync(Relationship);

            return ObjectMapper.Map<Relationship, RelationshipDto>(createdRelationship);
        }

        public async Task<PagedResultDto<RelationshipDto>> GetListAsync(GetRelationshipListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Relationship.Sequence);
            }

            var RelationshipQueryable = await _relationshipRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                RelationshipQueryable = RelationshipQueryable.Where(i => i.Name.Contains(input.Filter) || i.Description.Contains(input.Filter));

            if (input.RelationshipSearch != null)
            {
                if (!string.IsNullOrEmpty(input.RelationshipSearch.Name))
                {
                    RelationshipQueryable = RelationshipQueryable.Where(i => i.Name.Contains(input.RelationshipSearch.Name));
                }

                if (input.RelationshipSearch.Sequence != 0)
                {
                    RelationshipQueryable = RelationshipQueryable.Where(i => i.Sequence == input.RelationshipSearch.Sequence);
                }

                if (!string.IsNullOrEmpty(input.RelationshipSearch.Description))
                {
                    RelationshipQueryable = RelationshipQueryable.Where(i => i.Description.Contains(input.RelationshipSearch.Description));
                }
            }


            var Relationships = RelationshipQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = RelationshipQueryable.Count();

            return new PagedResultDto<RelationshipDto>(
                totalCount,
                ObjectMapper.Map<List<Relationship>, List<RelationshipDto>>(Relationships)
            );
        }

        public async Task<RelationshipDto> GetAsync(Guid id)
        {
            var Relationship = await _relationshipRepository.GetAsync(id);

            return ObjectMapper.Map<Relationship, RelationshipDto>(Relationship);
        }

        public async Task<RelationshipDto> UpdateAsync(Guid id, RelationshipDto model)
        {
            if (model.IsDefaultIndicator)
            {
                var RelationshipDefaultIndicatorTrue = await _relationshipRepository.FirstOrDefaultAsync(i => i.IsDefaultIndicator);

                if (RelationshipDefaultIndicatorTrue != null)
                {
                    RelationshipDefaultIndicatorTrue.IsDefaultIndicator = false;
                    await _relationshipRepository.UpdateAsync(RelationshipDefaultIndicatorTrue);
                }

            }

            var Relationship = await _relationshipRepository.GetAsync(id);

            Relationship.Name = model.Name;
            Relationship.Description = model.Description;
            Relationship.Sequence = model.Sequence;
            Relationship.Comments = model.Comments;
            Relationship.IsDefaultIndicator = model.IsDefaultIndicator;
            Relationship.IsSystemIndicator = model.IsSystemIndicator;

            var updatedRelationship = await _relationshipRepository.UpdateAsync(Relationship);

            return ObjectMapper.Map<Relationship, RelationshipDto>(updatedRelationship);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _relationshipRepository.DeleteAsync(id);
        }

    }
}
