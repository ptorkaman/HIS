using HIS.Educations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HIS.Educations
{
    public interface IEducationRepository: IRepository<Education, Guid>
    {
    }
}
