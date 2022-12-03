using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.IdentifierTypes
{
    public class GetIdentifierTypeListDto :  PagedAndSortedResultRequestDto
    {
        public IdentifierTypeSearchDto IdentifierTypeSearch { get; set; }
        public string Filter { get; set; }
    }
}
