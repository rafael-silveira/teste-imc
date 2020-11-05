using FluentAssertions;
using TesteIMCDominio.Servicos.CalculoIMC;
using Xunit;

namespace TesteIMCTestes.Unitario.Dominio.Servicos
{
    public class ServicoCalculoIMCTest
    {
        private ServicoCalculoIMC _servicoCalculoImc;

        public ServicoCalculoIMCTest()
        {
            _servicoCalculoImc = new ServicoCalculoIMC();
        }

        [Theory]
        [InlineData(2, 60, 15, "Magreza")]
        [InlineData(2, 80, 20, "Normal")]
        [InlineData(2, 100, 25, "Sobrepeso")]
        [InlineData(2, 120, 30, "Obesidade")]
        [InlineData(2, 160, 40, "Obesidade grave")]
        public void CalcularIMC_ValoresValidosInformados_IMCCalculado(decimal altura, decimal peso, decimal imcEsperado, string analiseEsperada) 
        {
            var resultado = _servicoCalculoImc.CalcularIMC(altura, peso);

            resultado.IMC.Should().Be(imcEsperado);
            resultado.Analise.Should().Be(analiseEsperada);
        }
    }
}