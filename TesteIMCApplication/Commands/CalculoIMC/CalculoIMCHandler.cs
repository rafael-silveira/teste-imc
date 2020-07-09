using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TesteIMCDominio.Servicos.CalculoIMC;

namespace TesteIMCApplication.Commands.CalculoIMC
{
    public class CalculoIMCHandler : IRequestHandler<CalculoIMCRequest, CalculoIMCResponse>
    {
        private IValidator<CalculoIMCRequest> _validator;
        private IServicoCalculoIMC _servicoCalculoImc;

        public CalculoIMCHandler(IValidator<CalculoIMCRequest> validator, IServicoCalculoIMC servicoCalculoImc)
        {
            _validator = validator;
            _servicoCalculoImc = servicoCalculoImc;
        }

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

                var resultado = _servicoCalculoImc.CalcularIMC(altura, peso);

                response.IMC = resultado.IMC;
                response.Analise = resultado.Analise;
            }

            return response;
        }
    }
}