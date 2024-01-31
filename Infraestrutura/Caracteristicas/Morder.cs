﻿using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Entidades;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Morder : Caracteristica
    {
        public Morder()
        {
            Observacoes = "Depende de um estomago para gerar beneficios";

            Nome = EnumCaracteristicas.Morder;
            Multiplicador = 1;
            DescValores[0] = "Dano a Vida";
            DescValores[1] = "Quantidade Comida Coletada";
            DescValores[2] = "Gasto Energia";
            DescValores[3] = "Quantidade estomago minima para morder";
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            Caracteristica estomago = individuo.Caracteristicas.FirstOrDefault(x => x.Nome == EnumCaracteristicas.Estomago);
            if (estomago.Valores[1] >= Valores[3])
                return mundo;

            if (individuo.Energia - Valores[2] < 0)
                return mundo;

            Individuo presa = mundo.EspecieList.FirstOrDefault(x => (x.PosicaoY == individuo.PosicaoY && x.PosicaoX == individuo.PosicaoX) && x.Especie != individuo.Especie && !x.Filiacao.Contains(individuo.Nome) && x.Vida > 0);

            Caracteristica canibal = individuo.Caracteristicas.FirstOrDefault(x => x.Nome == EnumCaracteristicas.Canibal); 
            if (presa == null && canibal != null)
            {
                Canibal execCanibal = canibal as Canibal;
                presa = execCanibal.buscaPresa(individuo, mundo);
            }

            if (presa != null)
            {
                presa.Vida += -Valores[0];
                if (presa.Vida <= 0)
                    //presa.definirComoMorto();

                if (estomago != null)
                {
                    estomago.Valores[1] += Valores[1];
                    if (estomago.Valores[1] > estomago.Valores[0]) //Capacidade Atual > Capacidade Maxima
                    {
                        estomago.Valores[1] = estomago.Valores[0];
                    }
                }
                individuo.Energia += Valores[2] * -1;
            }

            return mundo;
        }
    }
}
