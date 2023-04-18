using GeneticEvo.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Meiose : Caracteristica
    {
        public Meiose()
        {
            Nome = "Meiose";
            Prioridade = 1;
            DescValores[0] = "Gasto Energia";
            Valores[0] = -50;
            DescValores[1] = "Filhotes Gerados";
            Valores[1] = 2;
            DescValores[2] = "Tempo de Vida";
            Valores[2] = 20;
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null)
        {
            if (individuo.Energia + Valores[0] > 0)
            {
                individuo.Energia += Valores[0];
                for (int i = 0; i < Valores[1]; i++)
                {
                    Individuo filhote = new Individuo();
                    if (validaMutacao(individuo))
                    {
                        var novo = GeraFilhoteComum(individuo);
                        filhote = novo.GeraFilhoteMutacao(novo);
                    }
                    else
                    {
                        filhote = GeraFilhoteComum(individuo);
                    }

                    filhote.DataOrigem = mundo.Geracao + 1;
                    RegistroEspecie registro = mundo.registroEspecies.FirstOrDefault(x => x.Nome == filhote.Especie);
                    if (registro != null)
                    {
                        registro.UltimoRegistro = mundo.Geracao;
                    }
                    else
                    {
                        mundo.registroEspecies.Add(new RegistroEspecie() { Nome = filhote.Especie, AnoOrigem = mundo.Geracao, EspecieOrigem = filhote.Filiacao, UltimoRegistro = mundo.Geracao });
                    }
                    mundo.EspecieList.Add(filhote);
                }
                mundo.EspecieList.Remove(individuo);
            }
            return mundo;
        }

        bool validaMutacao(Individuo individuo)
        {
            Random rand = new Random();
            double validador = rand.NextDouble();

            if (individuo.ChaceMutacao > validador)
            {
                return true;
            }

            return false;
        }

        Individuo GeraFilhoteComum(Individuo individuo)
        {
            Individuo novo = new Individuo().GeraFilhoteComum(individuo);
            novo.Energia = novo.Energia / 2;
            novo.Vida = novo.Vida / 2;
            novo.TempoVida = (int)Valores[2];

            return novo;
        }
    }
}
