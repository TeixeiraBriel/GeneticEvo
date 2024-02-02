using GeneticEvo.Entidades.Caracteristicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvoBlazor.Model
{
    public class TelaInicialModel
    {
        public TelaInicialModel()
        {
            NomeJogador = "Gabriel";
            Conquistas = new List<string>() { "100 ind", "100 geracoes" };
        }

        public string NomeJogador { get; set; }
        public List<string> Conquistas { get; set; }
    }
}
