namespace GeneticEvo.Entidades
{
    public class Mundo
    {
        public Mundo()
        {
            Nome = "Mundo Base";
            EspecieList = new List<Individuo>();
            Geracao = 1;
            registroEspecies = new List<RegistroEspecie>();
        }

        public string Nome { get; set; }
        public List<Individuo> EspecieList { get; set; }
        public int Geracao { get; set; }
        public List<RegistroEspecie> registroEspecies { get; set; }
    }
}
