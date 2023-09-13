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
        public Grupos grupos;

        public Controlador()
        {
            mundo = ServiceHelper.GetService<Mundo>();
            grupos = ServiceHelper.GetService<Grupos>();
        }

        public void iniciaMundo()
        {
            mundo.EspecieList = new List<Individuo>();
            mundo.Geracao = 1;
            mundo.registroEspecies = new List<RegistroEspecie>();

            iniciaGrupo();
        }

        void iniciaGrupo()
        {
            if (grupos.GrupoAtivo == null)
                iniciaGrupoBase();

            grupos.GrupoAtivo.Individuos.ForEach(x =>
            {
                mundo.EspecieList.Add(x);
                mundo.registroEspecies.Add(new RegistroEspecie() { Nome = x.Especie, AnoOrigem = 0, EspecieOrigem = "N/A", UltimoRegistro = 1 });
            });
        }

        void iniciaGrupoBase()
        {
            Grupo grupoBase = new Grupo();

            Individuo Alga = new Individuo()
            {
                Nome = "Alga",
                Especie = "Alga",
                Geracao = 1,
                DataOrigem = 0,
                Vida = 100,
                Energia = 100,
                TempoVida = 20,
                Filiacao = "N/A",
                Caracteristicas = new List<Caracteristica> { new Fotossintese(), new Meiose(), new Regeneracao() }
            };

            Individuo Predador = new Individuo()
            {
                Nome = "Gafanhoto",
                Especie = "Gafanhoto",
                Geracao = 1,
                DataOrigem = 0,
                Vida = 100,
                Energia = 100,
                TempoVida = 20,
                Filiacao = "N/A",
                Caracteristicas = new List<Caracteristica> { new Estomago(), new Morder(), new Digestao(), new Regeneracao(), new Meiose() }
            };

            grupoBase.Individuos.Add(Predador);
            grupoBase.Individuos.Add(Alga);
            grupoBase.Individuos.Add(Alga);
            grupoBase.Individuos.Add(Alga);
            grupoBase.Individuos.Add(Alga);
            grupoBase.Individuos.Add(Alga);

            grupoBase.Nome = "Grupo Base";
            grupoBase.Ativo = true;
            grupos.listGrupos.Add(grupoBase);
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
