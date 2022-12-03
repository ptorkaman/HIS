using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.MaritalStatuses
{
    public class EfCoreMaritalStatusRepository : EfCoreRepository<HISDbContext, MaritalStatus, Guid>,
            IMaritalStatusRepository
    {
        public EfCoreMaritalStatusRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
