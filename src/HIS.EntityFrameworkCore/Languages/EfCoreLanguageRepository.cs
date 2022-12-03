using HIS.Facilities;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Languages
{
    public class EfCoreLanguageRepository : EfCoreRepository<HISDbContext, Language, Guid>,
            ILanguageRepository
    {
        public EfCoreLanguageRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
