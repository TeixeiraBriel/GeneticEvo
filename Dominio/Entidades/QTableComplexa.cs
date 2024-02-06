using Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class QTableComplexa
    {
        public double PesoVida { get; set; }
        public double PesoEnergia { get; set; }
        public double PesoDecendentes { get; set; }
        public double PesoLocalizacao { get; set; }
        public List<NeuronioVida> VidaOpcoes { get; set; }
        public List<NeuronioEnergia> EnergiaOpcoes { get; set; }
        public List<NeuronioDecendentes> DecendentesOpcoes { get; set; }
        public List<NeuronioLocalizacao> LocalizacaoOpcoes { get; set; }
        public List<QTableBaseOption> Opcoes { get; set; }

        public QTableComplexa()
        {
            VidaOpcoes = new List<NeuronioVida>();
            EnergiaOpcoes = new List<NeuronioEnergia>();
            DecendentesOpcoes = new List<NeuronioDecendentes>();
            LocalizacaoOpcoes = new List<NeuronioLocalizacao>();
            Opcoes = new List<QTableBaseOption>();
            PesoVida = 1;
            PesoEnergia = 1;
            PesoDecendentes = 0.5;
            PesoLocalizacao = 0;
        }
    }


    public class NeuronioVida : QTableBaseOption
    {
        public double Vida { get; set; }
    }

    public class NeuronioEnergia : QTableBaseOption
    {
        public double Energia { get; set; }
    }

    public class NeuronioDecendentes : QTableBaseOption
    {
        public int Decendentes { get; set; }
    }

    public class NeuronioLocalizacao : QTableBaseOption
    {
        public regiaoMundo Localizacao { get; set; }
    }
}
