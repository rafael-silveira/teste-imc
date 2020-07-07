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
