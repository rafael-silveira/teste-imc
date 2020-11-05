namespace TesteIMCDominio.Servicos.CalculoIMC
{
    public class ServicoCalculoIMC : IServicoCalculoIMC
    {
        public CalculoIMCResultado CalcularIMC(decimal altura, decimal peso)
        {
            decimal imc = peso / (altura * altura);
            string analise;
            if (imc < 18.5m)
                analise = "Magreza";
            else if (imc < 25)
                analise = "Normal";
            else if (imc < 30)
                analise = "Sobrepeso";
            else if (imc < 40)
                analise = "Obesidade";
            else
                analise = "Obesidade grave";

            return new CalculoIMCResultado(imc, analise);
        }
    }
}