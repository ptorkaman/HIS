using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Races
{
    public class EfCoreRaceRepository : EfCoreRepository<HISDbContext, Race, Guid>,
            IRaceRepository
    {
        public EfCoreRaceRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
