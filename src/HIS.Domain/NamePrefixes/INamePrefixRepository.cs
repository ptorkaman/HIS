using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HIS.NamePrefixes
{
    public interface INamePrefixRepository: IRepository<NamePrefix, Guid>
    {
    }
}
