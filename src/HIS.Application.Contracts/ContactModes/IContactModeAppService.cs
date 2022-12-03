using HIS.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HIS.ContactModes
{
    public interface IContactModeAppService : IApplicationService
    {
        Task<ContactModeDto> CreateAsync(ContactModeDto input);

        Task<PagedResultDto<ContactModeDto>> GetListAsync(GetContactModeListDto input);
        Task<ContactModeDto> GetAsync(Guid id);

        Task<ContactModeDto> UpdateAsync(Guid id, ContactModeDto model);

        Task DeleteAsync(Guid id);
    }
}
