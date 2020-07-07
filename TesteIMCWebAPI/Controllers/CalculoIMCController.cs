using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteIMCApplication.Commands.CalculoIMC;

namespace TesteIMCWebAPI.Controllers
{
    [Route("api/calculo")]
    [ApiController]
    public class CalculoIMCController : ControllerBase
    {
        private IMediator _mediator;

        public CalculoIMCController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("imc")]
        public async Task<CalculoIMCViewModel> Get(string altura, string peso)
        {
            var request = new CalculoIMCRequest
            {
                Altura = altura,
                Peso = peso
            };

            var response = await _mediator.Send(request);

            var result = new CalculoIMCViewModel
            {
                Altura = Convert.ToDecimal(altura, CultureInfo.InvariantCulture),
                Peso = Convert.ToDecimal(peso, CultureInfo.InvariantCulture),
                IMC = response.IMC,
                Analise = response.Analise
            };

            return result;
        }
    }

    // classe junta com outra no mesmo arquivo
    public class CalculoIMCViewModel
    {
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public decimal IMC { get; set; }
        public string Analise { get; set; }
    }
}
