using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.ContactModes
{
    public class GetContactModeListDto :  PagedAndSortedResultRequestDto
    {
        public ContactModeSearchDto ContactModeSearch { get; set; }
        public string Filter { get; set; }
    }
}
