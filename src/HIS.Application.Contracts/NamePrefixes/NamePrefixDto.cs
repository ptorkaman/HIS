using HIS.AdministrativeSexes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.NamePrefixes
{
    public class NamePrefixDto : EntityDto<Guid>
    {

        [Required(ErrorMessage = "the {0} field is required")]
        [StringLength(200, ErrorMessage = "the {0} field must be maxiumum of {1} characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "the {0} field must be maxiumum of {1} characters")]
        public string Description { get; set; }

        public AdministrativeSexDto AdministrativeSex { get; set; }

        public Guid? AdministrativeSexId { get; set; }

        public string Comments { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "the {0} field must be between {1} and {2}")]
        public int Sequence { get; set; }

        public bool IsDefaultIndicator { get; set; }

        public bool IsSystemIndicator { get; set; }
    }
}
