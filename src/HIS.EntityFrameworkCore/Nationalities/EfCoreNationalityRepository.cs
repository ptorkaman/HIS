using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Nationalities
{
    public class EfCoreNationalityRepository : EfCoreRepository<HISDbContext, Nationality, Guid>,
            INationalityRepository
    {
        public EfCoreNationalityRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
