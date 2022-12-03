using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Languages
{
    public class GetLanguageListDto :  PagedAndSortedResultRequestDto
    {
        public LanguageSearchDto LanguageSearch { get; set; }
        public string Filter { get; set; }
    }
}
