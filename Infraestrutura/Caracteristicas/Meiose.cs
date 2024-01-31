﻿using Dominio.Entidades;
using Dominio.Enumeradores;
using Infraestrutura.Servicos;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Meiose : Caracteristica
    {
        public Meiose()
        {
            Observacoes = "";

            Prioridade = 10;
            Nome = EnumCaracteristicas.Meiose;
            DescValores[0] = "Gasto Energia";
            Valores[0] = -40;
            DescValores[1] = "Filhotes Gerados";
            Valores[1] = 1;
            DescValores[2] = "Tempo de Vida";
            Valores[2] = 10;
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            Valores[0] = Valores[0] > 0 ? Valores[0] * -1 : Valores[0];
            if (individuo.Energia + Valores[0] > 0)
            {
                individuo.Energia += Valores[0];
                for (int i = 0; i < Valores[1]; i++)
                {
                    Individuo filhote = new Individuo();
                    if (new IndividuoServicos().validaMutacao(individuo))
                    {
                        filhote = new IndividuoServicos().GeraFilhoteMutacao(individuo);
                    }
                    else
                    {
                        filhote = GeraFilhoteComum(individuo, mundo.Geracao);
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
            }
            return mundo;
        }

        Individuo GeraFilhoteComum(Individuo individuo, int ano)
        {
            Individuo novo = new IndividuoServicos().GeraFilhoteComum(individuo, ano);
            novo.Energia = novo.Energia / 2;
            novo.Vida = novo.Vida / 2;
            novo.TempoVida = (int)Valores[2];

            return novo;
        }
    }
}
