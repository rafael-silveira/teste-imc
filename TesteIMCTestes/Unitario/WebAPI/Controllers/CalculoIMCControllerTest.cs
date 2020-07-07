using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteIMCApplication.Commands.CalculoIMC;
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

        [Fact]
        public async Task GetAsync_ResultadoSucesso_Sucesso()
        {
            var response = new CalculoIMCResponse
            {
                IsSuccess = true,
                Analise = "Teste",
                IMC = 1
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CalculoIMCRequest>(), CancellationToken.None))
                .Returns(() => Task.FromResult(response));

            var result = await _controller.GetAsync("2", "100", CancellationToken.None);

            result.As<OkObjectResult>().Value.As<CalculoIMCViewModel>().Altura.Should().Be(2);
            result.As<OkObjectResult>().Value.As<CalculoIMCViewModel>().Analise.Should().Be("Teste");
            result.As<OkObjectResult>().Value.As<CalculoIMCViewModel>().IMC.Should().Be(1);
            result.As<OkObjectResult>().Value.As<CalculoIMCViewModel>().Peso.Should().Be(100);
        }

        [Fact]
        public async Task GetAsync_ResultadoFalha_Erro()
        {
            var response = new CalculoIMCResponse();
            response.SetFail(new List<string> { "Falha 1"});

            _mediatorMock.Setup(x => x.Send(It.IsAny<CalculoIMCRequest>(), CancellationToken.None))
                .Returns(() => Task.FromResult(response));

            var result = await _controller.GetAsync("2", "100", CancellationToken.None);

            result.As<BadRequestObjectResult>().Value.As<string>().Should().Be("Erros:Falha 1");
        }
    }
}