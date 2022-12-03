using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.InActiveReasons
{
    public class GetInActiveReasonListDto :  PagedAndSortedResultRequestDto
    {
        public InActiveReasonSearchDto InActiveReasonSearch { get; set; }
        public string Filter { get; set; }
    }
}
