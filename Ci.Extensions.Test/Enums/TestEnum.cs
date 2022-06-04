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
        [Display(Name = "One1")]
        [System.ComponentModel.Description("One")]
        First = 1,

        [Display(Name = "Two2")]
        [System.ComponentModel.Description("Two")]
        Second = 2,

        [Display(Name = "Three3")]
        [System.ComponentModel.Description("Three")]
        Third = 3,
    }
}
