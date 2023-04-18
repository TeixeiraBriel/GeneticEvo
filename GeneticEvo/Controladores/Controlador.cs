using GeneticEvo.Entidades;
using GeneticEvo.Entidades.Caracteristicas;
using GeneticEvo.Helpers;

namespace GeneticEvo.Controladores
{
    public class Controlador
    {
        public Mundo mundo;

        public Controlador()
        {
            mundo = ServiceHelper.GetService<Mundo>();
        }

        public void iniciaAlgaBase()
        {
            Individuo Alga = new Individuo()
            {
                Nome = "Alga1",
                Especie = "Alga",
                Geracao = 1,
                DataOrigem = 0,
                Vida = 100,
                Energia = 100,
                TempoVida = 20,
                Caracteristicas = new List<Caracteristica> { new Fotossintese(), new Meiose(), new Digestao() }
            };

            mundo.registroEspecies.Add(new RegistroEspecie() { Nome = "Alga", AnoOrigem = 0, EspecieOrigem = "N/A", UltimoRegistro = 1});
            mundo.EspecieList.Add(Alga);
        }

        public void avancaGeracao()
        {
            //mundo.EspecieList.RemoveAll(x => x.Especie == "Alga");
            var especies = mundo.registroEspecies;
            List<Individuo> EspecieListAtual = atribuiLista(mundo.EspecieList);

            foreach (Individuo especie in EspecieListAtual)
            {
                mundo = especie.ExecutaCaracteristicas(mundo);
            }

            mundo.Geracao++;
        }

        List<Individuo> atribuiLista(List<Individuo> lista)
        {
            List<Individuo> listSaida = new List<Individuo>();
            foreach (var item in lista)
            {
                listSaida.Add(item);
            }
            return listSaida;
        }
    }
}
