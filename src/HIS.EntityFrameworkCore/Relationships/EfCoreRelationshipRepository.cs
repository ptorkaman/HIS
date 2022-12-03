using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Relationships
{
    public class EfCoreRelationshipRepository : EfCoreRepository<HISDbContext, Relationship, Guid>,
            IRelationshipRepository
    {
        public EfCoreRelationshipRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
