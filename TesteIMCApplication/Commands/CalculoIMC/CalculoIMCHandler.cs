using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        // classe faz muitas coisas ainda
        // ele controla a validacao e calcula o imc
        public async Task<CalculoIMCResponse> Handle(CalculoIMCRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            var response = new CalculoIMCResponse();

            if (!validationResult.IsValid)
            {
                response.SetFail(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            else
            {
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
            }

            return response;
        }
    }
}