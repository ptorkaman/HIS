using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.AdministrativeGenders
{
    public class EfCoreAdministrativeGenderRepository : EfCoreRepository<HISDbContext, AdministrativeGender, Guid>,
            IAdministrativeGenderRepository
    {
        public EfCoreAdministrativeGenderRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
