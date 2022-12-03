using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.AdministrativeSexes;
using Volo.Abp.Domain.Entities.Auditing;

namespace HIS.Countries
{
    [Table("Countries", Schema ="Lookup")]
    public class Country : FullAuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(2)]
        public string TwoLettersIsoCode { get; set; }

        [StringLength(3)]
        public string ThreeLettersIsoCode { get; set; }

        [StringLength(8)]
        public string NumericIsoCode { get; set; }

        public int Sequence { get; set; }

        [Required]
        public bool IsDefaultIndicator { get; set; }

        [Required]
        public bool IsSystemIndicator { get; set; }
    }
}
