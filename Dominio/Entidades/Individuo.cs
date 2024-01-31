using Dominio.Interfaces;

namespace Dominio.Entidades
{
    public class Individuo : IIndividuo
    {
        public string Nome { get; set; }
        public int Geracao { get; set; }
        public string Filiacao { get; set; }
        public string Especie { get; set; }
        public int DataOrigem { get; set; }
        public double Vida { get; set; }
        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
        public double Energia { get; set; }
        public double ChaceMutacao { get; set; }
        public int TempoVida { get; set; }
        public bool Vivo { get; set; }
        public int Decendentes { get; set; }
        public regiaoMundo posNoMundo { get; set; }
        public List<Caracteristica> Caracteristicas { get; set; }
        public Elementos ComposicaoOrganica { get; set; }
        public Elementos ToleranciaOrganica { get; set; }
        public List<QLerningOpcao> QTable { get; set; }
        public double taxaExploraca { get; set; }
        public double learningRate { get; set; }
        public double discountFactor { get; set; }

        public Individuo()
        {
            ChaceMutacao = 0.02;
            Vivo = true;
            Caracteristicas = new List<Caracteristica>();
            QTable = new List<QLerningOpcao>();
            taxaExploraca = 0.2;
            learningRate = 0.1;
            discountFactor = 0.7;
        }

    }
}
