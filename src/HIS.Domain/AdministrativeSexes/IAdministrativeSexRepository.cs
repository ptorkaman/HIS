using HIS.AdministrativeSexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HIS.AdministrativeSexes
{
    public interface IAdministrativeSexRepository: IRepository<AdministrativeSex, Guid>
    {
    }
}
