using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Entidades
{
    public class Grupos
    {
        public Grupos() 
        {
            listGrupos = new List<Grupo>();
        }

        public Grupo GrupoAtivo
        {
            get { return listGrupos.FirstOrDefault(x => x.Ativo); }
        }


        public List<Grupo> listGrupos { get; set; }
    }
}
