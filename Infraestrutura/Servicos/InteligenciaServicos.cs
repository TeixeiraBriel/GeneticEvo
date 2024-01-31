using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Servicos
{
    public class InteligenciaServicos : IInteligenciaServicos
    {
        public List<Caracteristica> Caracteristicas { get; set; }
        Individuo individuoBase;
        List<QLerningOpcao> QTableBase;

        public void ExecutaDecisao(Individuo individuo, Mundo mundo)
        {
            if (!individuo.Vivo)
                return;

            individuoBase = new Individuo();
            QTableBase = new List<QLerningOpcao>();

            Util.CopyProperties<Individuo>(individuo, individuoBase);
            if(individuo.QTable.Count > 0)
                Util.CopyProperties<List<QLerningOpcao>>(individuo.QTable, QTableBase);

            if (!QTableBase.Any(x => x.Estado == retornaEstado()))
                AdicionaEstado();

            EnumCaracteristicas action = EscolherAcao();
            new IndividuoServicos().ExecutaCaracteristicaEspecifica(individuo, mundo, action);
            double recompensa = CalcularRecompensa(individuo);
            AtualizarPeso(action, recompensa);

            individuo = individuoBase;
            individuo.QTable = QTableBase;
        }

        private void AdicionaEstado()
        {
            foreach (var caracteristica in individuoBase.Caracteristicas)
            {
                QTableBase.Add(new QLerningOpcao() { Estado = retornaEstado(), Caracteristica = caracteristica.Nome, Peso = new Random().NextDouble() });
            }
        }

        private EstadoIndividuo retornaEstado()
        {
            EstadoIndividuo estadoIndividuo = new EstadoIndividuo()
            {
                ChaceMutacao = individuoBase.ChaceMutacao,
                Energia = individuoBase.Energia,
                posNoMundo = individuoBase.posNoMundo,
                TempoVida = individuoBase.TempoVida,
                Vida = individuoBase.Vida,
            };

            return estadoIndividuo;
        }

        private EnumCaracteristicas EscolherAcao()
        {
            EnumCaracteristicas bestAction = individuoBase.Caracteristicas.FirstOrDefault().Nome;

            var chanceExploracao = new Random().NextDouble();
            if (chanceExploracao < individuoBase.taxaExploraca)
            {
                var Acoes = individuoBase.Caracteristicas.FindAll(x => x.Executar).ToList();
                int NumSorteado = new Random().Next(Acoes.Count);
                bestAction = Acoes[NumSorteado].Nome;
            }
            else
            {
                double bestQValue = 0;
                bool first = true;

                var estadoBase = retornaEstado();
                List<QLerningOpcao> acoesValidas = QTableBase.FindAll(x =>
                    x.Estado.Vida == estadoBase.Vida &&
                    x.Estado.TempoVida == estadoBase.TempoVida &&
                    x.Estado.Energia == estadoBase.Energia &&
                    x.Estado.ChaceMutacao == estadoBase.ChaceMutacao &&
                    x.Estado.posNoMundo == estadoBase.posNoMundo
                    ).ToList();

                foreach (var acoes in acoesValidas)
                {
                    if (first)
                    {
                        first = false;
                        bestAction = acoes.Caracteristica;
                        bestQValue = acoes.Peso;
                    }
                    else if (acoes.Peso > bestQValue)
                    {
                        bestAction = acoes.Caracteristica;
                        bestQValue = acoes.Peso;
                    }
                }
            }

            return bestAction;
        }

        private double CalcularRecompensa(Individuo individuo)
        {
            //BUSCA Não Se Ferir e Manter Energia
            double totalRecompensa = 0;

            totalRecompensa += individuo.Energia == individuoBase.Energia ? 0.5 :
                                individuo.Energia > individuoBase.Energia ? 1 : 0;

            totalRecompensa += individuo.Vida == individuoBase.Vida ? 0.5 :
                                individuo.Vida > individuoBase.Vida ? 1 : 0;

            var DecendentesSomados = individuo.Decendentes - individuoBase.Decendentes;
            totalRecompensa += individuo.Decendentes > individuoBase.Decendentes ? 0.2 * DecendentesSomados : 0;

            return totalRecompensa;
        }

        private void AtualizarPeso(EnumCaracteristicas action, double recompensa)
        {
            var estadoBase = retornaEstado();
            QLerningOpcao opcao = QTableBase.FirstOrDefault(x =>
                x.Estado.Vida == estadoBase.Vida &&
                x.Estado.TempoVida == estadoBase.TempoVida &&
                x.Estado.Energia == estadoBase.Energia &&
                x.Estado.ChaceMutacao == estadoBase.ChaceMutacao &&
                x.Estado.posNoMundo == estadoBase.posNoMundo
                && x.Caracteristica == action);

            var valorAtual = opcao.Peso;

            var resultadoQtable = (1 - individuoBase.learningRate) * opcao.Peso + individuoBase.learningRate * (recompensa + individuoBase.discountFactor * MaxQValue());
            opcao.Peso = resultadoQtable;
        }

        private double MaxQValue()
        {
            var estadoBase = retornaEstado();
            List<QLerningOpcao> acoesValidas = QTableBase.FindAll(x =>
                x.Estado.Vida == estadoBase.Vida &&
                x.Estado.TempoVida == estadoBase.TempoVida &&
                x.Estado.Energia == estadoBase.Energia &&
                x.Estado.ChaceMutacao == estadoBase.ChaceMutacao &&
                x.Estado.posNoMundo == estadoBase.posNoMundo
                ).ToList();
            double maxQValue = acoesValidas.FirstOrDefault().Peso;

            foreach (var acao in acoesValidas)
            {
                maxQValue = acao.Peso > maxQValue ? acao.Peso : maxQValue;
            }

            return maxQValue;
        }
    }
}
