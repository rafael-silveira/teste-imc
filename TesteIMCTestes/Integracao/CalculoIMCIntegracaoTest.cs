using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using TesteIMCWebAPI.Controllers;
using Xunit;

namespace TesteIMCTestes.Integracao
{
    public class CalculoIMCIntegracaoTest : IClassFixture<CalculoIMCIntegracaoFixture>
    {
        private CalculoIMCIntegracaoFixture _fixture;

        public CalculoIMCIntegracaoTest(CalculoIMCIntegracaoFixture fixture)
        {
            _fixture = fixture;
        }

        // na teoria, com a modificação para mediatr, o unico teste que realmente não vai mudar é o de integração
        // se eu fiz a chamada para /api/calculo/imc, ele deve retornar um json com { IMC(decimal); Analise(string) }
        // e era isso... mudando com a criacao de dominio, mediatr, commands, etc... nada deve mudar o comportamento
        // quero SEMPRE chamar o endereco /api/calculo/imc com os parametros altura e peso e receber o icm e analise
        // como vai ser feito... nao interessa
        [Fact]
        public async Task GetAsync_AcessoAPICalculoIMCComDadosCorretos_Sucesso()
        {
            var client = _fixture.GetClient();
            var request = await client.GetAsync("/api/calculo/imc?altura=2&peso=100");
            request.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<CalculoIMCIntegracaoResult>(await request.Content.ReadAsStringAsync());

            result.IMC.Should().Be(25);
            result.Analise.Should().Be("Sobrepeso");
        }
    }

    class CalculoIMCIntegracaoResult
    {
        public decimal IMC { get; set; }
        public string Analise { get; set; }
    }
}
