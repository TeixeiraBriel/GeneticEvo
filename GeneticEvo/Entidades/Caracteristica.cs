using GeneticEvo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Entidades
{
    public abstract class Caracteristica
    {
        public string  Nome { get; set; }
        public int Prioridade { get; set; }
        public double Multiplicador { get; set; }

        public string[] DescValores { get; set; }
        public double[] Valores { get; set; }

        public abstract Mundo Executa(Individuo individuo = null, Mundo mundo = null);

        public Caracteristica()
        {
            DescValores = new string[10];
            Valores = new double[10];
        }

        public void ClonarCaracteristica(Caracteristica baseCar)
        {          
            Nome = baseCar.Nome;
            Prioridade= baseCar.Prioridade;
            Multiplicador = baseCar.Multiplicador;
            for (int i = 0; i < 10; i++)
            {
                DescValores[i] = baseCar.DescValores[i];
            }
            for (int i = 0; i < 10; i++)
            {
                Valores[i] = baseCar.Valores[i];
            }
        }
    }
}
