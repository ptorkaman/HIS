using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HIS.Enums
{
    public enum InActiveStatusEnum
    {
        [Display(Name = "Select An Option")]
        SelectAnOption = 0,

        [Display(Name = "InActive")]
        Inactive = 1,

        [Display(Name = "Inactive-Deceased")]
        InactiveDeceased = 2
    }
}
