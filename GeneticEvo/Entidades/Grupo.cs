using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Entidades
{
    public class Grupo
    {
        public Grupo()
        {
            Individuos = new List<Individuo>();
        }

        public int IdGrupo { get; set; }
        public string Nome { get; set; }
        public List<Individuo> Individuos { get; set; }
        public bool Ativo { get; set; }
    }
}
