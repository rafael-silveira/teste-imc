﻿using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteIMCApplication.Commands.CalculoIMC;

namespace TesteIMCWebAPI.Controllers
{
    [Route("api/calculo")]
    [ApiController]
    public class CalculoIMCController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalculoIMCController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("imc")]
        public async Task<IActionResult> GetAsync(string altura, string peso, CancellationToken cancellationToken)
        {
            var request = new CalculoIMCRequest
            {
                Altura = altura,
                Peso = peso
            };

            var response = await _mediator.Send(request, cancellationToken);

            if (!response.IsSuccess)
            {
                return BadRequest("Erros:" + string.Join(", ", response.Errors));
            }

            return Ok(new CalculoIMCViewModel
            {
                Altura = Convert.ToDecimal(altura, CultureInfo.InvariantCulture),
                Peso = Convert.ToDecimal(peso, CultureInfo.InvariantCulture),
                IMC = response.IMC,
                Analise = response.Analise
            });
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
