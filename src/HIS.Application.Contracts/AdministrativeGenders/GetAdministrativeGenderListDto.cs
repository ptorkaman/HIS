using HIS.AdministrativeGenders;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.AdministrativeGenders
{
    public class GetAdministrativeGenderListDto :  PagedAndSortedResultRequestDto
    {
        public AdministrativeGenderSearchDto AdministrativeSearch { get; set; }
        public string Filter { get; set; }
    }
}
