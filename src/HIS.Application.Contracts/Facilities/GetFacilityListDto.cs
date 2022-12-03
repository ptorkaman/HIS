using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Facilities
{
    public class GetFacilityListDto :  PagedAndSortedResultRequestDto
    {
        public FacilitySearchDto FacilitySearch { get; set; }
        public string Filter { get; set; }
    }
}
