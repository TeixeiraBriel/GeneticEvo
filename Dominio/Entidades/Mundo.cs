namespace Dominio.Entidades
{
    public class Mundo
    {
        public Mundo()
        {
            Nome = "Mundo Base";
            EspecieList = new List<Individuo>();
            Geracao = 1;
            registroEspecies = new List<RegistroEspecie>();
            regiaoMundo = CriarMapeamentoMundo(5,5,10);
            AcoesFimGeracao = new List<Action>();
        }

        public string Nome { get; set; }
        public List<Individuo> EspecieList { get; set; }
        public int Geracao { get; set; }
        public List<RegistroEspecie> registroEspecies { get; set; }

        public List<regiaoMundo> regiaoMundo { get; set;}

        public List<Action> AcoesFimGeracao { get; set; }//TODA AÇÃO DE CONSUMO NO MUNDO DEVE SER APLICADO NA HORA, TODA AÇÂO DE REAÇÃO NO MUNDO DEVE SER APLICADA NO FIM DO TURNO

        List<regiaoMundo> CriarMapeamentoMundo(int tamanhoX, int tamanhoY, int tamanhoZ, double QtdElementoA = 10, double QtdElementoB = 10, double QtdElementoC = 10, double QtdElementoD = 10)
        {
            Elementos elementos = new Elementos() { A = QtdElementoA, B = QtdElementoB, C = QtdElementoC, D = QtdElementoD, };
            return Enumerable.Range(0, tamanhoX)
                .SelectMany(x => Enumerable.Range(0, tamanhoY)
                    .SelectMany(y => Enumerable.Range(0, tamanhoZ)
                        .Select(z => new regiaoMundo { EixoX = x, EixoY = y, EixoZ = z , Elementos = elementos, QtdIndividuos = 0 })))
                .ToList();
        }
    }
}
