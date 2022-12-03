using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Countries
{
    public class CountryDto : EntityDto<Guid>
    {

        [Required(ErrorMessage = "the {0} field is required")]
        [StringLength(50, ErrorMessage = "the {0} field must be maxiumum of {1} characters")]
        public string Name { get; set; }

        [StringLength(2, ErrorMessage = "the {0} field must be maxiumum of {1} characters")]
        public string TwoLettersIsoCode { get; set; }

        [StringLength(3, ErrorMessage = "the {0} field must be maxiumum of {1} characters")]
        public string ThreeLettersIsoCode { get; set; }

        [StringLength(8, ErrorMessage = "the {0} field must be maxiumum of {1} characters")]
        public string NumericIsoCode { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "the {0} field must be between {1} and {2}")]
        public int Sequence { get; set; }

        public bool IsDefaultIndicator { get; set; }

        public bool IsSystemIndicator { get; set; }
    }
}
