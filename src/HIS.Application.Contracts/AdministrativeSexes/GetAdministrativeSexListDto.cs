using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.AdministrativeSexes
{
    public class GetAdministrativeSexListDto :  PagedAndSortedResultRequestDto
    {
        public AdministrativeSexSearchDto AdministrativeSearch { get; set; }
        public string Filter { get; set; }
    }
}
