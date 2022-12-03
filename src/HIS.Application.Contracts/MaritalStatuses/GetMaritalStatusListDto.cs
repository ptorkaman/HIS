using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.MaritalStatuses
{
    public class GetMaritalStatusListDto :  PagedAndSortedResultRequestDto
    {
        public MaritalStatusSearchDto MaritalStatusSearch { get; set; }
        public string Filter { get; set; }
    }
}
