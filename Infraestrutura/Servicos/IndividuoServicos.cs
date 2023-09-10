using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Interfaces;
using GeneticEvo.Entidades.Caracteristicas;
using System.Runtime.CompilerServices;

namespace Infraestrutura.Servicos
{
    public class IndividuoServicos : IIndividuoServicos
    {
        public Mundo ExecutaCaracteristicas(Individuo ind, Mundo mundo)
        {
            if (ind.Vivo)
            {
                for (int i = 10; i > 0; i--)
                {
                    List<Caracteristica> _caracteristicasAtual = ind.Caracteristicas.FindAll(x => x.Prioridade == i && x.Executar);
                    foreach (Caracteristica caracteristicas in _caracteristicasAtual)
                    {
                        mundo = caracteristicas.Executa(ind, mundo);
                    }
                }
            }

            if (ind.Vida <= 0 && ind.Vivo) 
            {
                ind.Vivo = false;
                ind.TempoVida = 0;
            }
                

                ind.TempoVida--;
            if (ind.TempoVida <= 0 && mundo.EspecieList.FirstOrDefault(x => x == ind) != null)
            {
                RemoveIndividuo(mundo, ind);
            }

            return mundo;
        }

        public Individuo GeraFilhoteComum(Individuo individuo, int Ano)
        {
            Individuo filhote = new Individuo();
            Util.CopyProperties<Individuo>(individuo, filhote);
            filhote.DataOrigem = Ano;
            filhote.Nome = individuo.Especie + (individuo.Geracao + 1);
            filhote.Geracao++;
            filhote.Filiacao = individuo.Nome;

            return filhote;
        }

        public Individuo GeraFilhoteMutacao(Individuo individuo)
        {
            //Nasce o Bebezinho
            Individuo filhote = new Individuo();
            Util.CopyProperties<Individuo>(individuo, filhote);

            //Mutação foi em local relevante
            int sortudo = new Random().Next(filhote.Caracteristicas.Count);
            int campoSortudo = new Random().Next(0, 10);
            string campoModificado = string.IsNullOrEmpty(individuo.Caracteristicas[sortudo].DescValores[campoSortudo]) ? "Não utilizado" : individuo.Caracteristicas[sortudo].DescValores[campoSortudo];

            //Irrelevante
            if (campoModificado.Equals("Não utilizado"))
            {
                return filhote;
            }
            //Relevante
            else
            {
                var nomeEspecie = Util.gerarNome();
                filhote.Especie = nomeEspecie;
                filhote.Nome = nomeEspecie + 1;
                filhote.Geracao = 1;

                double valorMutado = filhote.ChaceMutacao * (new Random().NextDouble() + new Random().NextDouble());

                switch (campoSortudo)
                {
                    case 10:
                        filhote.Caracteristicas[sortudo].Multiplicador += valorMutado;
                        campoModificado = "Multiplicador";
                        break;
                    default:
                        filhote.Caracteristicas[sortudo].Valores[campoSortudo] += valorMutado;
                        break;
                }

                filhote.Caracteristicas[sortudo].Multiplicador += valorMutado;
                filhote.ChaceMutacao = 0.01;

                return filhote;
            }
        }

        public bool validaMutacao(Individuo individuo)
        {
            var rand = new Random().Next(1, 5);
            double validador = rand * individuo.ChaceMutacao;

            if (validador > 0.1)
            {
                return true;
            }

            return false;
        }

        public void AdicionarIndividuo(Mundo mundo, Individuo individuo)
        {
            mundo.EspecieList.Add(individuo);
        }

        public void RemoveIndividuo(Mundo mundo, Individuo ind)
        {
            mundo.EspecieList.Remove(ind);
        }

        public void AdicionarCaracteristica(Individuo individuo, EnumCaracteristicas caracteristicasEnum, double[] Valores)
        {
            Caracteristica novaCaracteristica = null;

            switch (caracteristicasEnum)
            {
                case EnumCaracteristicas.Fotossintese:
                    novaCaracteristica = new Fotossintese();
                    break;
                case EnumCaracteristicas.Digestao:
                    novaCaracteristica = new Digestao();
                    break;
                case EnumCaracteristicas.Meiose:
                    novaCaracteristica = new Meiose();
                    break;
                case EnumCaracteristicas.Morder:
                    novaCaracteristica = new Morder();
                    break;
                case EnumCaracteristicas.Estomago:
                    novaCaracteristica = new Estomago();
                    break;
                case EnumCaracteristicas.Regeneracao:
                    novaCaracteristica = new Regeneracao();
                    break;
            }

            novaCaracteristica.Valores = Valores;
            individuo.Caracteristicas.Add(novaCaracteristica);
        }
    }
}
