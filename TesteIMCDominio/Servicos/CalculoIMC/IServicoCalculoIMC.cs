using System;
using System.Collections.Generic;
using System.Text;

namespace TesteIMCDominio.Servicos.CalculoIMC
{
    public interface IServicoCalculoIMC
    {
        CalculoIMCResultado CalcularIMC(decimal altura, decimal peso);
    }
}
