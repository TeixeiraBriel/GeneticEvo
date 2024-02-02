namespace Dominio.ViewModels
{
    public class TelaInicialModel
    {
        public TelaInicialModel()
        {
            NomeJogador = "Takato";
            Conquistas = new List<string>() { "100 ind", "100 geracoes" };
        }

        public string NomeJogador { get; set; }
        public List<string> Conquistas { get; set; }
    }
}
