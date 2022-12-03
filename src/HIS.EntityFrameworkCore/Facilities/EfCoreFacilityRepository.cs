using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Facilities
{
    public class EfCoreFacilityRepository : EfCoreRepository<HISDbContext, Facility, Guid>,
            IFacilityRepository
    {
        public EfCoreFacilityRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
