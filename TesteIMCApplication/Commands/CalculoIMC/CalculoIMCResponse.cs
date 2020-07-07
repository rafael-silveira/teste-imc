﻿using System;
using System.Collections.Generic;
using System.Text;
using TesteIMCApplication.Base;

namespace TesteIMCApplication.Commands.CalculoIMC
{
    public class CalculoIMCResponse : BaseResponse
    {
        public decimal IMC { get; set; }
        public string Analise { get; set; }
    }
}
