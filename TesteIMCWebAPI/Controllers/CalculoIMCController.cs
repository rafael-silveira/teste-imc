using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TesteIMCWebAPI.Controllers
{
    [Route("api/calculo")]
    [ApiController]
    public class CalculoIMCController : ControllerBase
    {
        [HttpGet("imc")]
        public CalculoICMViewModel Get(string altura, string peso)
        {
            if (string.IsNullOrEmpty(altura) || string.IsNullOrEmpty(peso))
            {
                throw new ArgumentException("Altura e/ou peso devem ser informados");
            }

            try
            {
                var result = new CalculoICMViewModel
                {
                    Altura = Convert.ToDecimal(altura),
                    Peso = Convert.ToDecimal(peso)
                };

                result.IMC = result.Peso / (result.Altura * result.Altura);

                if (result.IMC < 18.5m)
                    result.Analise = "Magreza";
                else if (result.IMC < 25)
                    result.Analise = "Normal";
                else if (result.IMC < 30)
                    result.Analise = "Sobrepeso";
                else if (result.IMC < 40)
                    result.Analise = "Obesidade";
                else
                    result.Analise = "Obesidade grave";

                return result;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }
    }

    public class CalculoICMViewModel
    {
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public decimal IMC { get; set; }
        public string Analise { get; set; }
    }
}
