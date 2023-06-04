using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Entidades
{
    public class RegistroEspecie
    {
        public string Nome { get; set; }
        public int AnoOrigem { get; set; }
        public string EspecieOrigem { get; set; }
        public int UltimoRegistro { get; set; }
        public int QuantidadeAtual { get; set; }
        public int QuantidadeExistente { get; set; }
    }
}
