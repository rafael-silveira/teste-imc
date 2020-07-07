using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;

namespace TesteIMCApplication.Commands.CalculoIMC
{
    public class CalculoIMCValidation : AbstractValidator<CalculoIMCRequest>
    {
        public CalculoIMCValidation()
        {
            RuleFor(x => x.Peso)
                .NotNull().WithMessage("Peso deve ser informado")
                .NotEmpty().WithMessage("Peso deve ser informado")
                .Matches(@"^[0-9]+(\.[0-9]+)?$").WithMessage("Peso inválido");

            RuleFor(x => x.Altura)
                .NotNull().WithMessage("Altura deve ser informada")
                .NotEmpty().WithMessage("Altura deve ser informada")
                .Matches(@"^[0-9]+(\.[0-9]+)?$").WithMessage("Altura inválida");
        }
    }
}