namespace Dominio.Entidades
{
    public class Individuo
    {
        public string Nome { get; set; }
        public int Geracao { get; set; }
        public string Filiacao { get; set; }
        public string Especie { get; set; }
        public int DataOrigem { get; set; }
        public double Vida { get; set; }
        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
        public double Fome { get; set; }
        public double Energia { get; set; }
        public double ChaceMutacao { get; set; }
        public int TempoVida { get; set; }
        public bool Vivo { get; set; }
        public List<Caracteristica> Caracteristicas { get; set; }

        public Individuo()
        {
            ChaceMutacao = 0.02;
            Vivo = true;
            Caracteristicas = new List<Caracteristica>();
        }

    }
}
