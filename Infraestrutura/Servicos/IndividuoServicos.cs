using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Interfaces;
using GeneticEvo.Entidades.Caracteristicas;
using Infraestrutura.Caracteristicas;
using System.Buffers.Text;
using System.Runtime.CompilerServices;

namespace Infraestrutura.Servicos
{
    public class IndividuoServicos : IIndividuoServicos
    {
        public Mundo ExecutaCaracteristicaEspecifica(Individuo ind, Mundo mundo, EnumCaracteristicas caracteristica)
        {
            if (ind.Vivo)
            {
                Caracteristica caracteristicas = ind.Caracteristicas.FirstOrDefault(x => x.Nome == caracteristica);
                mundo = caracteristicas.Executa(ind, mundo);
            }

            if(ind.Energia > ind.EnergiaMaxima)
                ind.Energia = ind.EnergiaMaxima;

            if (ind.Vida > ind.VidaMaxima)
                ind.Vida = ind.VidaMaxima;


            if (ind.Energia < 0)
            {
                ind.Vida += ind.Energia;
                ind.Energia = 0;
            }

            if ((ind.Vida <= 0 || ind.TempoVida <= 0) && ind.Vivo)
            {
                ind.Vivo = false;
                ind.TempoVida = 0;
            }

            ind.TempoVida--;
            return mundo;
        }

        public Individuo GeraFilhoteComum(Individuo individuo, int Ano)
        {
            if (individuo.Especie == null)
                individuo.Especie = individuo.Nome;

            Individuo filhote = new Individuo();
            Util.CopyProperties<Individuo>(individuo, filhote);
            filhote.QTable = new List<QLerningOpcao>();
            filhote.DataOrigem = Ano;
            filhote.Nome = individuo.Especie + (individuo.Geracao + 1);
            filhote.Geracao++;
            filhote.Filiacao = individuo.Nome;
            filhote.Vida = filhote.VidaMaxima;
            filhote.Energia = filhote.EnergiaMaxima;
            List<QLerningOpcao> QtableFilhote = new List<QLerningOpcao>();
            foreach (var opcao in individuo.QTable)
            {
                var baseQ = new QLerningOpcao() { Estado = new EstadoIndividuo(), Peso = opcao.Peso, Caracteristica = opcao.Caracteristica};
                Util.CopyProperties<EstadoIndividuo>(opcao.Estado, baseQ.Estado);
                QtableFilhote.Add(baseQ);
            }
            filhote.QTable = QtableFilhote;

            return filhote;
        }

        public Individuo GeraFilhoteMutacao(Individuo individuo)
        {
            //Nasce o Bebezinho
            Individuo filhote = new Individuo();
            Util.CopyProperties<Individuo>(individuo, filhote);
            filhote.Vida = filhote.VidaMaxima;
            filhote.Energia = filhote.EnergiaMaxima;

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
                filhote.ChaceMutacao = 0.1;

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
            Individuo novoIndividuo = new Individuo();
            Util.CopyProperties<Individuo>(individuo, novoIndividuo);
            novoIndividuo.Especie = novoIndividuo.Especie ?? novoIndividuo.Nome;
            novoIndividuo.VidaMaxima = novoIndividuo.Vida;
            novoIndividuo.EnergiaMaxima = novoIndividuo.Energia;
            novoIndividuo.TempoVidaMaximo = novoIndividuo.TempoVida;

            novoIndividuo.posNoMundo = mundo.regiaoMundo.FirstOrDefault();
            novoIndividuo.posNoMundo.QtdIndividuos++;
            mundo.EspecieList.Add(novoIndividuo);
        }

        public void RemoveIndividuo(Mundo mundo, Individuo ind)
        {
            mundo.EspecieList.Remove(ind);
        }

        public void AdicionarCaracteristica(IIndividuo individuo, EnumCaracteristicas caracteristicasEnum, double[] Valores)
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
                case EnumCaracteristicas.TransformarElemento:
                    novaCaracteristica = new TransformarElemento();
                    break;
            }

            novaCaracteristica.Valores = Valores;
            individuo.Caracteristicas.Add(novaCaracteristica);
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
