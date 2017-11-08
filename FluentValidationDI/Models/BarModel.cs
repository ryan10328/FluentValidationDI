using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
using FluentValidationDI.Validators;

namespace FluentValidationDI.Models
{
    [Validator(typeof(BarModelValidator))]
    public class BarModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}