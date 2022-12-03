using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace HIS.AdministrativeGenders
{
    [Table("AdministrativeGenders", Schema = "Lookup")]
    public class AdministrativeGender : FullAuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(70)]
        public string Description { get; set; }

        public string Comments { get; set; }

        public int Sequence { get; set; }

        [Required]
        public bool IsDefaultIndicator { get; set; }

        [Required]
        public bool IsSystemIndicator { get; set; }
    }
}
