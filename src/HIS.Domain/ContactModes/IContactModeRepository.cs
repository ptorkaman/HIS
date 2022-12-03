using HIS.ContactModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HIS.ContactModes
{
    public interface IContactModeRepository: IRepository<ContactMode, Guid>
    {
    }
}
