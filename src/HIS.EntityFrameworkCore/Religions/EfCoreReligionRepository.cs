using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Religions
{
    public class EfCoreReligionRepository : EfCoreRepository<HISDbContext, Religion, Guid>,
            IReligionRepository
    {
        public EfCoreReligionRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
