using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.NamePrefixes
{
    public class GetNamePrefixListDto :  PagedAndSortedResultRequestDto
    {
        public NamePrefixSearchDto NamePrefixSearch { get; set; }
        public string Filter { get; set; }
    }
}
