using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace TesteIMCApplication.Commands.CalculoIMC
{
    public class CalculoIMCRequest : IRequest<CalculoIMCResponse>
    {
        public string Altura { get; set; }
        public string Peso { get; set; }
    }
}
