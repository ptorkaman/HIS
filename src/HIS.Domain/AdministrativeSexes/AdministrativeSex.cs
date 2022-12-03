using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace HIS.AdministrativeSexes
{
    [Table("AdministrativeSexes", Schema = "Lookup")]
    public class AdministrativeSex : FullAuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(200)]
        public string ShortDescription { get; set; }

        [StringLength(1000)]
        public string LongDescription { get; set; }

        public string Comments { get; set; }

        public int Sequence { get; set; }

        [Required]
        public bool IsDefaultIndicator { get; set; }

        [Required]
        public bool IsSystemIndicator { get; set; }

    }
}
