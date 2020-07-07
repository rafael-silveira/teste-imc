using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using TesteIMCApplication.Commands.CalculoIMC;
using Xunit;

namespace TesteIMCTestes.Unitario.Application.Commands.CalculoIMC
{
    public class CalculoIMCValidationTest
    {
        private CalculoIMCValidation _validation;

        public CalculoIMCValidationTest()
        {
            _validation = new CalculoIMCValidation();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("10")]
        [InlineData("100")]
        [InlineData("100.10")]
        [InlineData("50.23")]
        public void Validation_ValidarPeso_Sucesso(string peso)
        {
            var request = new CalculoIMCRequest
            {
                Altura = "1",
                Peso = peso
            };

            var result = _validation.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("10")]
        [InlineData("100")]
        [InlineData("100.10")]
        [InlineData("50.23")]
        public void Validation_ValidarAltura_Sucesso(string altura)
        {
            var request = new CalculoIMCRequest
            {
                Altura = altura, 
                Peso = "1"
            };

            var result = _validation.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null, "Peso deve ser informado")]
        [InlineData("", "Peso deve ser informado")]
        [InlineData("1,2", "Peso inválido")]
        [InlineData("-30", "Peso inválido")]
        [InlineData(".", "Peso inválido")]
        [InlineData("asd", "Peso inválido")]
        [InlineData("1.3.4", "Peso inválido")]
        public void Validation_ValidarPeso_Falha(string peso, string mensagemErro)
        {
            var request = new CalculoIMCRequest
            {
                Altura = "1",
                Peso = peso
            };

            var result = _validation.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Be(mensagemErro);
        }

        [Theory]
        [InlineData(null, "Altura deve ser informada")]
        [InlineData("", "Altura deve ser informada")]
        [InlineData("1,2", "Altura inválida")]
        [InlineData("-30", "Altura inválida")]
        [InlineData(".", "Altura inválida")]
        [InlineData("asd", "Altura inválida")]
        [InlineData("1.3.4", "Altura inválida")]
        public void Validation_ValidarAltura_Falha(string altura, string mensagemErro)
        {
            var request = new CalculoIMCRequest
            {
                Altura = altura,
                Peso = "1"
            };

            var result = _validation.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Be(mensagemErro);
        }
    }
}
