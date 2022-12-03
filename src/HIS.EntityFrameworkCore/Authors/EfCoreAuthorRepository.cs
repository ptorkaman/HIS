using HIS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HIS.Authors
{
    public class EfCoreAuthorRepository : EfCoreRepository<HISDbContext, Author, Guid>,
            IAuthorRepository
    {
        public EfCoreAuthorRepository(
           IDbContextProvider<HISDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
