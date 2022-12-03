using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Countries
{
    public class GetCountryListDto :  PagedAndSortedResultRequestDto
    {
        public CountrySearchDto CountrySearch { get; set; }
        public string Filter { get; set; }
    }
}
