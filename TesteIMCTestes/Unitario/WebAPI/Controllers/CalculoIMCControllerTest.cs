using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using TesteIMCWebAPI.Controllers;
using Xunit;

namespace TesteIMCTestes.Unitario.WebAPI.Controllers
{
    public class CalculoIMCControllerTest
    {
        private CalculoIMCController _controller;
        private Mock<IMediator> _mediatorMock;

        public CalculoIMCControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();

            _controller = new CalculoIMCController(_mediatorMock.Object);
        }

        [Theory]
        [InlineData("2", "60", 2, 60, 15, "Magreza")]
        [InlineData("2", "80", 2, 80, 20, "Normal")]
        [InlineData("2", "100", 2, 100, 25, "Sobrepeso")]
        [InlineData("2", "120", 2, 120, 30, "Obesidade")]
        [InlineData("2", "160", 2, 160, 40, "Obesidade grave")]
        public async Task Get_AlturaPesoCorretos_Sucesso(string altura, string peso, 
            decimal alturaEsperada, decimal pesoEsperado, decimal imcEsperado, string analiseEsperada)
        {
            var result = await _controller.Get(altura, peso);

            result.Altura.Should().Be(alturaEsperada);
            result.Peso.Should().Be(pesoEsperado);
            result.IMC.Should().Be(imcEsperado);
            result.Analise.Should().Be(analiseEsperada);
        }

        [Fact]
        public async Task Get_AlturaNaoInformada_Erro()
        {
            _controller.Invoking(async x => await x.Get(null, "100"))
                .Should().Throw<ArgumentException>()
                .WithMessage("Altura e/ou peso devem ser informados");
        }

        [Fact]
        public async Task Get_PesoNaoInformada_Erro()
        {
            _controller.Invoking(async x => await x.Get("2", null))
                .Should().Throw<ArgumentException>()
                .WithMessage("Altura e/ou peso devem ser informados");
        }

        [Fact]
        public async Task Get_PesoInformadoErrado_Erro()
        {
            _controller.Invoking(async x => await x.Get("2", "dkdkd"))
                .Should().Throw<System.Net.Http.HttpRequestException>()
                .WithMessage("Input string was not in a correct format.");
        }

    }
}