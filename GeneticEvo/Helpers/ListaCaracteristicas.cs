using GeneticEvo.Entidades;
using GeneticEvo.Entidades.Caracteristicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Helpers
{
    public class ListaCaracteristicas
    {
        public List<Caracteristica> TodasCaracteristicas { get; set; }

        public ListaCaracteristicas()
        {
            TodasCaracteristicas = new List<Caracteristica>()
            {
                new Digestao(),
                new Fotossintese(),
                new Meiose(),
                new Morder(),
                new Estomago(),
                new Regeneracao()
            };
        }
    }
}
