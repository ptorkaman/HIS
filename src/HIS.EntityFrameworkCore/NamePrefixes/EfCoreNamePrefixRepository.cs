using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.NamePrefixes
{
    public class EfCoreNamePrefixRepository : EfCoreRepository<HISDbContext, NamePrefix, Guid>,
            INamePrefixRepository
    {
        public EfCoreNamePrefixRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
