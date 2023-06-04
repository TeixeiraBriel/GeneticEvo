using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Helpers
{
    public class AlertaMutacoes
    {
        public bool HouveMutacoes { get; set; }
        public List<string> Mutacoes { get; set; }

        public AlertaMutacoes()
        {
            Mutacoes= new List<string>();
        }

        public void resetAlerta()
        {
            Mutacoes.Clear();
            HouveMutacoes = false;
        }

        public void mostrarAlerta()
        {
            string texto = "";
            foreach (var mut in Mutacoes)
            {
                if (texto == "")
                    texto += $"{mut}";
                else
                    texto += $"\n{mut}";
            }

            ServiceHelper.GetService<MainPage>().DisplayAlert("Mutações", texto, "OK");
        }
    }
}