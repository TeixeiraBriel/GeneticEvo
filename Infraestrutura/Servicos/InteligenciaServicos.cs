using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Interfaces;
using GeneticEvo.Entidades.Caracteristicas;
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
        QTableComplexa _QTableComplexa;

        #region QTableComplexa

        public void ExecutaDecisao(Individuo individuo, Mundo mundo)
        {
            if (!individuo.Vivo)
                return;

            individuoBase = new Individuo();
            QTableBase = individuo.QTable;
            _QTableComplexa = individuo.QTableComplexa;

            Util.CopyProperties<Individuo>(individuo, individuoBase);

            _QTableComplexa.Opcoes = BuscaOpcoes();

            EnumCaracteristicas action = EscolherAcao();
            new IndividuoServicos().ExecutaCaracteristicaEspecifica(individuo, mundo, action);
            List<double> recompensas = CalcularRecompensa(individuo);
            AtualizarPesos(action, recompensas);

            individuo = individuoBase;
        }

        List<QTableBaseOption> BuscaOpcoes()
        {
            List<QTableBaseOption> Opcoes = new List<QTableBaseOption>();

            var opcoesNovas = AnalisarOpcoesVida();
            SomaPesoOpcoes(Opcoes, opcoesNovas, individuoBase.QTableComplexa.PesoVida);

            opcoesNovas = AnalisarOpcoesEnergia();
            SomaPesoOpcoes(Opcoes, opcoesNovas, individuoBase.QTableComplexa.PesoEnergia);

            opcoesNovas = AnalisarOpcoesDecentes();
            SomaPesoOpcoes(Opcoes, opcoesNovas, individuoBase.QTableComplexa.PesoDecendentes);

            opcoesNovas = AnalisarOpcoesLocalizacao();
            SomaPesoOpcoes(Opcoes, opcoesNovas, individuoBase.QTableComplexa.PesoLocalizacao);

            return Opcoes;
        }

        void SomaPesoOpcoes(List<QTableBaseOption> OpcoesOut, List<QTableBaseOption> OpcoesIn, double Peso)
        {
            OpcoesIn.ForEach(y =>
            {
                if (OpcoesOut.Any(x => x.Caracteristica == y.Caracteristica))
                {
                    OpcoesOut.FirstOrDefault(x => x.Caracteristica == y.Caracteristica).Peso += y.Peso * Peso;
                }
                else
                {
                    QTableBaseOption item = y;
                    item.Peso *= Peso;
                    OpcoesOut.Add(item);
                }
            });
        }

        List<QTableBaseOption> AnalisarOpcoesVida()
        {
            List<QTableBaseOption> lista = new List<QTableBaseOption>();
            if (_QTableComplexa.VidaOpcoes.Any(x => x.Vida == individuoBase.Vida))
            {
                _QTableComplexa.VidaOpcoes.Where(x => x.Vida == individuoBase.Vida).ToList().ForEach(y => lista.Add(new QTableBaseOption() { Caracteristica = y.Caracteristica, Peso = y.Peso }));
            }
            else
            {
                foreach (var caracteristica in individuoBase.Caracteristicas)
                {
                    double peso = new Random().NextDouble();
                    lista.Add(new QTableBaseOption() { Caracteristica = caracteristica.Nome, Peso = peso });
                    _QTableComplexa.VidaOpcoes.Add(new NeuronioVida() { Caracteristica = caracteristica.Nome, Peso = peso, Vida = individuoBase.Vida });
                }
            }

            return lista;
        }

        List<QTableBaseOption> AnalisarOpcoesEnergia()
        {
            List<QTableBaseOption> lista = new List<QTableBaseOption>();
            if (_QTableComplexa.EnergiaOpcoes.Any(x => x.Energia == individuoBase.Energia))
            {
                _QTableComplexa.EnergiaOpcoes.Where(x => x.Energia == individuoBase.Energia).ToList().ForEach(y => lista.Add(new QTableBaseOption() { Caracteristica = y.Caracteristica, Peso = y.Peso }));
            }
            else
            {
                foreach (var caracteristica in individuoBase.Caracteristicas)
                {
                    double peso = new Random().NextDouble();
                    lista.Add(new QTableBaseOption() { Caracteristica = caracteristica.Nome, Peso = peso });
                    _QTableComplexa.EnergiaOpcoes.Add(new NeuronioEnergia() { Caracteristica = caracteristica.Nome, Peso = peso, Energia = individuoBase.Energia });
                }
            }

            return lista;
        }

        List<QTableBaseOption> AnalisarOpcoesDecentes()
        {
            List<QTableBaseOption> lista = new List<QTableBaseOption>();
            if (_QTableComplexa.DecendentesOpcoes.Any(x => x.Decendentes == individuoBase.Decendentes))
            {
                _QTableComplexa.DecendentesOpcoes.Where(x => x.Decendentes == individuoBase.Decendentes).ToList().ForEach(y => lista.Add(new QTableBaseOption() { Caracteristica = y.Caracteristica, Peso = y.Peso }));
            }
            else
            {
                foreach (var caracteristica in individuoBase.Caracteristicas)
                {
                    double peso = new Random().NextDouble();
                    lista.Add(new QTableBaseOption() { Caracteristica = caracteristica.Nome, Peso = peso });
                    _QTableComplexa.DecendentesOpcoes.Add(new NeuronioDecendentes() { Caracteristica = caracteristica.Nome, Peso = peso, Decendentes = individuoBase.Decendentes });
                }
            }

            return lista;
        }

        List<QTableBaseOption> AnalisarOpcoesLocalizacao()
        {
            List<QTableBaseOption> lista = new List<QTableBaseOption>();
            if (_QTableComplexa.LocalizacaoOpcoes.Any(x => x.Localizacao == individuoBase.posNoMundo))
            {
                _QTableComplexa.LocalizacaoOpcoes.Where(x => x.Localizacao == individuoBase.posNoMundo).ToList().ForEach(y => lista.Add(new QTableBaseOption() { Caracteristica = y.Caracteristica, Peso = y.Peso }));
            }
            else
            {
                foreach (var caracteristica in individuoBase.Caracteristicas)
                {
                    double peso = new Random().NextDouble();
                    lista.Add(new QTableBaseOption() { Caracteristica = caracteristica.Nome, Peso = peso });
                    _QTableComplexa.LocalizacaoOpcoes.Add(new NeuronioLocalizacao() { Caracteristica = caracteristica.Nome, Peso = peso, Localizacao = individuoBase.posNoMundo });
                }
            }

            return lista;
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

                List<QTableBaseOption> acoesValidas = _QTableComplexa.Opcoes;

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

        private List<double> CalcularRecompensa(Individuo individuo)
        {
            double RecompensaVida = individuo.Vida < individuoBase.Vida ? -1 :
                                individuo.Vida > individuoBase.Vida ? 0.5 : 0;

            double RecompensaEnergia = individuo.Energia < individuoBase.Energia ? -1 :
                                individuo.Energia > individuoBase.Energia ? 0.5 : 0;

            var DecendentesSomados = individuo.Decendentes - individuoBase.Decendentes;
            double RecompensaDecendentes = DecendentesSomados > 0 ? 0.2 * DecendentesSomados : 0;
            
            //TODO: pensar em como recompesar por localização
            double RecompensaLocalizacao = 0;

            return new List<double>() { RecompensaVida, RecompensaEnergia, RecompensaDecendentes, RecompensaLocalizacao};
        }
        
        private void AtualizarPeso(QTableBaseOption OpcaoAtualizada, double recompensa, double MaxQValue)
        {
            QTableBaseOption opcao = OpcaoAtualizada;
            var valorAtual = opcao.Peso;

            var resultadoQtable = (1 - individuoBase.learningRate) * opcao.Peso + individuoBase.learningRate * (recompensa + individuoBase.discountFactor * MaxQValue);
            opcao.Peso = resultadoQtable;
        }
        
        private double MaxQValue(List<QTableBaseOption> acoesValidas)
        {
            double maxQValue = acoesValidas.FirstOrDefault().Peso;

            foreach (var acao in acoesValidas)
            {
                maxQValue = acao.Peso > maxQValue ? acao.Peso : maxQValue;
            }

            return maxQValue;
        }
        
        private void AtualizarPesos(EnumCaracteristicas action, List<double> recompensas)
        {
            double maxQValue = MaxQValue(individuoBase.QTableComplexa.VidaOpcoes.Select(x => new QTableBaseOption() { Caracteristica = x.Caracteristica, Peso = x.Peso }).ToList());
            AtualizarPeso(individuoBase.QTableComplexa.VidaOpcoes.First(x => x.Caracteristica == action), recompensas[0], maxQValue);

            maxQValue = MaxQValue(individuoBase.QTableComplexa.EnergiaOpcoes.Select(x => new QTableBaseOption() { Caracteristica = x.Caracteristica, Peso = x.Peso }).ToList());
            AtualizarPeso(individuoBase.QTableComplexa.EnergiaOpcoes.First(x => x.Caracteristica == action), recompensas[1], maxQValue);

            maxQValue = MaxQValue(individuoBase.QTableComplexa.DecendentesOpcoes.Select(x => new QTableBaseOption() { Caracteristica = x.Caracteristica, Peso = x.Peso }).ToList());
            AtualizarPeso(individuoBase.QTableComplexa.DecendentesOpcoes.First(x => x.Caracteristica == action), recompensas[2], maxQValue);

            maxQValue = MaxQValue(individuoBase.QTableComplexa.LocalizacaoOpcoes.Select(x => new QTableBaseOption() { Caracteristica = x.Caracteristica, Peso = x.Peso }).ToList());
            AtualizarPeso(individuoBase.QTableComplexa.LocalizacaoOpcoes.First(x => x.Caracteristica == action), recompensas[3], maxQValue);
        }
        #endregion

        /*METODO COM QTABLE SIMPLES
         
        public void ExecutaDecisao(Individuo individuo, Mundo mundo)
        {
            if (!individuo.Vivo)
                return;

            individuoBase = new Individuo();
            QTableBase = individuo.QTable;

            Util.CopyProperties<Individuo>(individuo, individuoBase);

            if (!QTableBase.Any(x => x.Estado == retornaEstado()))
                AdicionaEstado();

            EnumCaracteristicas action = EscolherAcao();
            new IndividuoServicos().ExecutaCaracteristicaEspecifica(individuo, mundo, action);
            double recompensa = CalcularRecompensa(individuo);
            AtualizarPeso(action, recompensa);

            individuo = individuoBase;
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
                Energia = individuoBase.Energia,
                posNoMundo = individuoBase.posNoMundo,
                Decendentes = individuoBase.Decendentes,
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
                    x.Estado.Decendentes == estadoBase.Decendentes &&
                    x.Estado.Energia == estadoBase.Energia &&
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

            totalRecompensa += individuo.Energia < individuoBase.Energia ? -1 :
                                individuo.Energia > individuoBase.Energia ? 0.5 : 0;

            totalRecompensa += individuo.Vida > individuoBase.Vida ? 1 :
                                individuo.Vida < individuoBase.Vida ? -1 : 0;

            var DecendentesSomados = individuo.Decendentes - individuoBase.Decendentes;
            totalRecompensa += individuo.Decendentes > individuoBase.Decendentes ? 0.5 * DecendentesSomados :
                                individuo.TempoVida < individuo.TempoVida / 2 && individuo.Decendentes == 0 ? -1 : 0;

            return totalRecompensa;
        }

        private void AtualizarPeso(EnumCaracteristicas action, double recompensa)
        {
            var estadoBase = retornaEstado();
            QLerningOpcao opcao = QTableBase.FirstOrDefault(x =>
                x.Estado.Vida == estadoBase.Vida &&
                x.Estado.Decendentes == estadoBase.Decendentes &&
                x.Estado.Energia == estadoBase.Energia &&
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
                x.Estado.Decendentes == estadoBase.Decendentes &&
                x.Estado.Energia == estadoBase.Energia &&
                x.Estado.posNoMundo == estadoBase.posNoMundo
                ).ToList();
            double maxQValue = acoesValidas.FirstOrDefault().Peso;

            foreach (var acao in acoesValidas)
            {
                maxQValue = acao.Peso > maxQValue ? acao.Peso : maxQValue;
            }

            return maxQValue;
        }
        */
    }
}
