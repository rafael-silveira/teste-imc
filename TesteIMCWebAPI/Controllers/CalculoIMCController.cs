using System;
using System.Collections.Generic;
using System.Globalization;
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
        // por enquanto, tudo calculado no proprio controller para posteriormente mudar tudo para o uso de mediatr

        [HttpGet("imc")]
        public CalculoICMViewModel Get(string altura, string peso)
        {
            // controller validando altura e peso?
            if (string.IsNullOrEmpty(altura) || string.IsNullOrEmpty(peso))
            {
                throw new ArgumentException("Altura e/ou peso devem ser informados");
            }

            try
            {
                // eu sei o que eu informei para ser calculado... pq preciso receber de volta???
                var result = new CalculoICMViewModel
                {
                    Altura = Convert.ToDecimal(altura, CultureInfo.InvariantCulture),
                    Peso = Convert.ToDecimal(peso, CultureInfo.InvariantCulture)
                };

                // controller calculando imc também?
                result.IMC = result.Peso / (result.Altura * result.Altura);

                // controller fazendo a analise do icm também?
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
                // retornando uma exceção que nao tem nada relacionado com o problema
                throw new HttpRequestException(ex.Message);
            }
        }
    }

    // classe junta com outra no mesmo arquivo
    public class CalculoICMViewModel
    {
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public decimal IMC { get; set; }
        public string Analise { get; set; }
    }
}
