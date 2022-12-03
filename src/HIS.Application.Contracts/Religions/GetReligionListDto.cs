using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Religions
{
    public class GetReligionListDto :  PagedAndSortedResultRequestDto
    {
        public ReligionSearchDto ReligionSearch { get; set; }
        public string Filter { get; set; }
    }
}
