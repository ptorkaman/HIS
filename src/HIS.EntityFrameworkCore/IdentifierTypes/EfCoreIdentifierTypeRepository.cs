using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.IdentifierTypes
{
    public class EfCoreIdentifierTypeRepository : EfCoreRepository<HISDbContext, IdentifierType, Guid>,
            IIdentifierTypeRepository
    {
        public EfCoreIdentifierTypeRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
