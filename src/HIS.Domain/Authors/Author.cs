using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace HIS.Authors
{
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }
    }
}
