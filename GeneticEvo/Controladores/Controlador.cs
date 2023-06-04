using GeneticEvo.Entidades;
using GeneticEvo.Entidades.Caracteristicas;
using GeneticEvo.Enumeradores;
using GeneticEvo.Helpers;
using System.Collections.Generic;

namespace GeneticEvo.Controladores
{
    public class Controlador
    {
        public Mundo mundo;

        public Controlador()
        {
            mundo = ServiceHelper.GetService<Mundo>();
        }

        public void iniciaMundo()
        {
            mundo.EspecieList = new List<Individuo>();
            mundo.Geracao = 1;
            mundo.registroEspecies = new List<RegistroEspecie>();

            iniciaAlgaBase();
            iniciaPredadorBase();
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
                Filiacao = "N/A",
                Caracteristicas = new List<Caracteristica> { new Fotossintese(), new Meiose(), new Regeneracao() }
            };

            Alga.Caracteristicas.Where(a => a.Nome == EnumCaracteristicas.Regeneracao).Last().Valores[1] = 15;
            Alga.Caracteristicas.Where(a => a.Nome == EnumCaracteristicas.Meiose).Last().Valores[2] = 8;//Tempo de Vida
            Alga.Caracteristicas.Where(a => a.Nome == EnumCaracteristicas.Meiose).Last().Valores[1] = 4;//Filhotes Gerados
            mundo.registroEspecies.Add(new RegistroEspecie() { Nome = "Alga", AnoOrigem = 0, EspecieOrigem = "N/A", UltimoRegistro = 1});
            mundo.EspecieList.Add(Alga);
        }
        public void iniciaPredadorBase()
        {
            Individuo Predador = new Individuo()
            {
                Nome = "Predador1",
                Especie = "Predador",
                Geracao = 1,
                DataOrigem = 0,
                Vida = 100,
                Energia = 100,
                TempoVida = 20,
                Filiacao = "N/A",
                Caracteristicas = new List<Caracteristica> { new Estomago(), new Morder(), new Digestao(), new Regeneracao(), new Meiose() }
            };

            mundo.registroEspecies.Add(new RegistroEspecie() { Nome = "Predador", AnoOrigem = 0, EspecieOrigem = "N/A", UltimoRegistro = 1 });
            mundo.EspecieList.Add(Predador);
        }

        public void avancaGeracao()
        {
            var especies = mundo.registroEspecies;
            List<Individuo> EspecieListAtual = atribuiLista(mundo.EspecieList);

            foreach (Individuo especie in EspecieListAtual)
            {
                mundo = especie.ExecutaCaracteristicas(mundo);
            }

            var alerta = ServiceHelper.GetService<AlertaMutacoes>();
            if (alerta.HouveMutacoes)
            {
                alerta.mostrarAlerta();
                alerta.resetAlerta();
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
