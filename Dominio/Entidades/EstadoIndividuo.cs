using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class EstadoIndividuo
    {
        public double Vida { get; set; }
        public double Energia { get; set; }
        public int Decendentes { get; set; }
        public regiaoMundo posNoMundo { get; set; }

    }
}
