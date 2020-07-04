using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using TesteIMCWebAPI.Controllers;
using Xunit;

namespace TesteIMCTestes.Unitario.WebAPI.Controllers
{
    public class CalculoIMCControllerTest
    {
        private CalculoIMCController _controller;

        public CalculoIMCControllerTest()
        {
            _controller = new CalculoIMCController();
        }

        [Fact]
        public void Get_AlturaPesoCorretos_Sucesso()
        {
            var result = _controller.Get("2", "100");

            result.Altura.Should().Be(2);
            result.Peso.Should().Be(100);
            result.IMC.Should().Be(25);
            result.Analise.Should().Be("Sobrepeso");
        }

        [Fact]
        public void Get_AlturaNaoInformada_Erro()
        {
            _controller.Invoking(x => x.Get(null, "100"))
                .Should().Throw<ArgumentException>()
                .WithMessage("Altura e/ou peso devem ser informados");
        }

        [Fact]
        public void Get_PesoNaoInformada_Erro()
        {
            _controller.Invoking(x => x.Get("2", null))
                .Should().Throw<ArgumentException>()
                .WithMessage("Altura e/ou peso devem ser informados");
        }
    }
}
