using HIS.ContactModes;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.ContactModes
{
    public class EfCoreContactModeRepository : EfCoreRepository<HISDbContext, ContactMode, Guid>,
            IContactModeRepository
    {
        public EfCoreContactModeRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
