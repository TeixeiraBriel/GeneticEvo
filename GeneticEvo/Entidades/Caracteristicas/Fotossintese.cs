
using GeneticEvo.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Fotossintese : Caracteristica
    {
        public Fotossintese()
        {
            Observacoes = "";

            Prioridade = 8;
            Nome = EnumCaracteristicas.Fotossintese;
            Multiplicador = 1;
            DescValores[0] = "Quantidade Energia ganha";
            Valores[0] = 15;
            DescValores[1] = "Quantidade Energia gasta";
            Valores[1] = 0.5;
            DescValores[2] = "Quantidade Energia para priorizar Fotossintese";
            Valores[2] = 15;
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            double gastoEnergia = -1 * (Valores[1] * Multiplicador);
            double ganhoEnergia = (Valores[0] * Multiplicador);
            if (individuo.Energia + gastoEnergia > 0)
            {
                individuo.Energia += gastoEnergia;
                individuo.Energia += ganhoEnergia;
            }

            return mundo;
        }
    }
}
