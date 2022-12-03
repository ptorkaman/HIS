using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Educations
{
    public class GetEducationListDto :  PagedAndSortedResultRequestDto
    {
        public EducationSearchDto EducationSearch { get; set; }
        public string Filter { get; set; }
    }
}
