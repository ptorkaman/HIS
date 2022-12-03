using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Relationships
{
    public class GetRelationshipListDto :  PagedAndSortedResultRequestDto
    {
        public RelationshipSearchDto RelationshipSearch { get; set; }
        public string Filter { get; set; }
    }
}
