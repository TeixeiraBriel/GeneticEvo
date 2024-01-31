using Dominio.Enumeradores;

namespace Dominio.Entidades
{
    public abstract class Caracteristica
    {
        public EnumCaracteristicas Nome { get; set; }
        public bool Executar { get; set; }
        public double Multiplicador { get; set; }
        public string Observacoes { get; set; }
        public string[] DescValores { get; set; }
        public double[] Valores { get; set; }

        public abstract Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao);

        public Caracteristica()
        {
            DescValores = new string[10];
            Valores = new double[10];
            Executar = true;
        }
    }
}
