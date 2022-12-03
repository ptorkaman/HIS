using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.AdministrativeSexes
{
    public class AdministrativeSexDto : EntityDto<Guid>
    {

        [Required(ErrorMessage = "the {0} field is required")]

        [StringLength(200, ErrorMessage = "the {0} field must be maxiumum of {1} characters")]
        public string ShortDescription { get; set; }

        [StringLength(1000,ErrorMessage = "the {0} field must be maxiumum of {1} characters")]
        [Required(ErrorMessage = "the {0} field is required")]
        public string LongDescription { get; set; }

        public string Comments { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "the {0} field must be between {1} and {2}")]
        public int Sequence { get; set; }

        public bool IsDefaultIndicator { get; set; }

        public bool IsSystemIndicator { get; set; }
    }
}
