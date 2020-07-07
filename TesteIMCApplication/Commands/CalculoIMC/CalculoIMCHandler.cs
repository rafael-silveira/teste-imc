using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace TesteIMCApplication.Commands.CalculoIMC
{
    public class CalculoIMCHandler : IRequestHandler<CalculoIMCRequest, CalculoIMCResponse>
    {
        private IValidator<CalculoIMCRequest> _validator;

        public CalculoIMCHandler(IValidator<CalculoIMCRequest> validator)
        {
            _validator = validator;
        }

        public async Task<CalculoIMCResponse> Handle(CalculoIMCRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                throw new ArgumentException("Altura e/ou peso devem ser informados");

            var response = new CalculoIMCResponse();

            var altura = Convert.ToDecimal(request.Altura, CultureInfo.InvariantCulture);
            var peso = Convert.ToDecimal(request.Peso, CultureInfo.InvariantCulture);

            response.IMC = peso / (altura * altura);

            if (response.IMC < 18.5m)
                response.Analise = "Magreza";
            else if (response.IMC < 25)
                response.Analise = "Normal";
            else if (response.IMC < 30)
                response.Analise = "Sobrepeso";
            else if (response.IMC < 40)
                response.Analise = "Obesidade";
            else
                response.Analise = "Obesidade grave";

            return response;
        }
    }
}