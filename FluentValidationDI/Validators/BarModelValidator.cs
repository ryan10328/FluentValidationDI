using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidationDI.Models;
using FluentValidationDI.Services;

namespace FluentValidationDI.Validators
{
    public class BarModelValidator : AbstractValidator<BarModel>
    {
        public BarModelValidator(IFooService fooService)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull()
                .Must(x => x == fooService.GetBar(2).ToString())
                .WithMessage(x => $"{nameof(x.Name)} 要等於 {fooService.GetBar(2).ToString()}");
            RuleFor(x => x.Email).NotNull().EmailAddress();
        }
       
    }
}