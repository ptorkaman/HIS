using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Countries
{
    public class EfCoreCountryRepository : EfCoreRepository<HISDbContext, Country, Guid>,
            ICountryRepository
    {
        public EfCoreCountryRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
