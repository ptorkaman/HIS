using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Races
{
    public class GetRaceListDto :  PagedAndSortedResultRequestDto
    {
        public RaceSearchDto RaceSearch { get; set; }
        public string Filter { get; set; }
    }
}
