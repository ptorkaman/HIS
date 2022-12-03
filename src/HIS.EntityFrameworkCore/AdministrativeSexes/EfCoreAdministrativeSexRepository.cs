using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.AdministrativeSexes
{
    public class EfCoreAdministrativeSexRepository : EfCoreRepository<HISDbContext, AdministrativeSex, Guid>,
            IAdministrativeSexRepository
    {
        public EfCoreAdministrativeSexRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
