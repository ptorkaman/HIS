using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Nationalities
{
    public class GetNationalityListDto :  PagedAndSortedResultRequestDto
    {
        public NationalitySearchDto NationalitySearch { get; set; }
        public string Filter { get; set; }
    }
}
