using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.AdministrativeSexes;
using Volo.Abp.Domain.Entities.Auditing;

namespace HIS.ContactModes
{
    [Table("ContactModes", Schema ="Lookup")]
    public class ContactMode : FullAuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Comments { get; set; }

        public int Sequence { get; set; }

        [Required]
        public bool IsDefaultIndicator { get; set; }

        [Required]
        public bool IsSystemIndicator { get; set; }
    }
}
