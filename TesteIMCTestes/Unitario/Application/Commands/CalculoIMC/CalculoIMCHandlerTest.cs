using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TesteIMCApplication.Commands.CalculoIMC;
using Xunit;

namespace TesteIMCTestes.Unitario.Application.Commands.CalculoIMC
{
    public class CalculoIMCHandlerTest
    {
        private CalculoIMCHandler _handler;
        private Mock<IValidator<CalculoIMCRequest>> _validatorMock;

        public CalculoIMCHandlerTest()
        {
            _validatorMock = new Mock<IValidator<CalculoIMCRequest>>();
            _handler = new CalculoIMCHandler(_validatorMock.Object);
        }

        [Fact]
        public async void Handle_ValidacaoOk_Sucesso()
        {
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CalculoIMCRequest>(), CancellationToken.None))
                .Returns(() => Task.FromResult(new ValidationResult()));

            var request = new CalculoIMCRequest
            {
                Altura = "2",
                Peso = "100"
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.IMC.Should().Be(25);
        }

        [Fact]
        public async void Handle_ValidacaoFalha_Falha()
        {
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CalculoIMCRequest>(), CancellationToken.None))
                .Returns(() => Task.FromResult(new ValidationResult(new List<ValidationFailure>()
                {
                    new ValidationFailure("Peso", "Peso Inválido")
                })));

            var request = new CalculoIMCRequest
            {
                Altura = "2",
                Peso = "100"
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            response.IsSuccess.Should().BeFalse();
            response.Errors[0].Should().Be("Peso Inválido");
        }
    }
}
