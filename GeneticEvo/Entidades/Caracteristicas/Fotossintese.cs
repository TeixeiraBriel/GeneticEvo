
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
            Nome = "Fotossintese";
            Prioridade = 0;
            Multiplicador = 1;
            DescValores[0] = "Quantidade fome modifica";
            Valores[0] = 5;
            DescValores[1] = "Quantidade Energia gasta";
            Valores[1] = 2;
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null)
        {
            double gastoEnergia = -1 * (Valores[1] * Multiplicador);
            double consumoFome = -1 * (Valores[0] * Multiplicador);
            if (individuo.Energia + gastoEnergia > 0)
            {
                individuo.Energia += gastoEnergia;
                individuo.Fome += consumoFome;
            }

            return mundo;
        }
    }
}
