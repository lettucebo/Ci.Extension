using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ci.Extensions.Test.Enums
{
    public enum TestEnum
    {
        [Display(Name = "One")]
        [System.ComponentModel.Description("One")]
        First = 1,

        [Display(Name = "Two")]
        [System.ComponentModel.Description("Two")]
        Second = 2,

        [Display(Name = "Three")]
        [System.ComponentModel.Description("Three")]
        Third = 3,
    }
}
