using HIS.Educations;
using HIS.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Educations
{
    public class EfCoreEducationRepository : EfCoreRepository<HISDbContext, Education, Guid>,
            IEducationRepository
    {
        public EfCoreEducationRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
