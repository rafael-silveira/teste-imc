using System;
using System.Collections.Generic;
using System.Text;

namespace TesteIMCDominio.Servicos.CalculoIMC
{
    public class CalculoIMCResultado
    {
        public CalculoIMCResultado(decimal imc, string analise)
        {
            this.IMC = imc;
            this.Analise = analise;
        }

        public decimal IMC { get; }
        public string Analise { get; }
    }
}
