using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ZivotopisCore.Models.Home
{
    public class CustomPhoneAttribute : RegularExpressionAttribute
    {
        public CustomPhoneAttribute()
            : base(@"^\+?[0-9\s]{9,15}$")
        {
        }
    }

    public class CustomEmailAttribute : RegularExpressionAttribute
    {
        public CustomEmailAttribute()
            : base("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$")
        {
        }
    }

}