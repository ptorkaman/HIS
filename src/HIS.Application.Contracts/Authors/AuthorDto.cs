using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HIS.Authors
{
    public class AuthorDto : EntityDto<Guid>
    {

        [Required(ErrorMessage ="the {0} field is required")]
        [StringLength(64)]
        public string Name { get; set; }

        [Required(ErrorMessage = "the {0} field is required")]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        public string ShortBio { get; set; }
    }
}
