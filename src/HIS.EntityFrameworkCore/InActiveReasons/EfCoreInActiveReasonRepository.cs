using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.InActiveReasons
{
    public class EfCoreInActiveReasonRepository : EfCoreRepository<HISDbContext, InActiveReason, Guid>,
            IInActiveReasonRepository
    {
        public EfCoreInActiveReasonRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
