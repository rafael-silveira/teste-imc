namespace TesteIMCDominio.Servicos.CalculoIMC
{
    public interface IServicoCalculoIMC
    {
        CalculoIMCResultado CalcularIMC(decimal altura, decimal peso);
    }
}
