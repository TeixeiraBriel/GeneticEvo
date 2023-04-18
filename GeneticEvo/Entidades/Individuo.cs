using GeneticEvo.Entidades.Caracteristicas;
using GeneticEvo.Helpers;
using Microsoft.Maui.Controls.Compatibility.Platform.UWP;
using Microsoft.Maui.Controls.Platform;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Entidades
{
    public class Individuo
    {
        public string Nome { get; set; }
        public int Geracao { get; set; }
        public string Filiacao { get; set; }
        public string Especie { get; set; }
        public int DataOrigem { get; set; }
        public double Vida { get; set; }
        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
        public double Fome { get; set; }
        public double Energia { get; set; }
        public double ChaceMutacao { get; set; }
        public int TempoVida { get; set; }

        public Individuo()
        {
            ChaceMutacao = 0.002;
        }

        public List<Caracteristica> Caracteristicas { get; set; }

        public Mundo ExecutaCaracteristicas(Mundo mundo)
        {
            for (int i = 0; i < 10; i++)
            {
                List<Caracteristica> _caracteristicasAtual = Caracteristicas.FindAll(x => x.Prioridade == i);
                foreach (Caracteristica caracteristicas in _caracteristicasAtual)
                {
                    mundo = caracteristicas.Executa(this, mundo);
                }
            }

            TempoVida--;
            if (TempoVida <= 0 && mundo.EspecieList.FirstOrDefault(x => x == this) != null)
            {
                mundo.EspecieList.Remove(this);
            }
            return mundo;
        }

        public void AddCaracteristica(Caracteristica caracteristica)
        {
            List<Caracteristica> listCaracteristicas = new ListaCaracteristicas().TodasCaracteristicas;
            var novaCaracteristica = listCaracteristicas.FirstOrDefault(x => x.Nome == caracteristica.Nome);

            string dadosAntiga = JsonConvert.SerializeObject(caracteristica);
            switch (novaCaracteristica.Nome)
            {
                case "Fotossintese":
                    novaCaracteristica = JsonConvert.DeserializeObject<Fotossintese>(dadosAntiga);
                    break;
                case "Digestao":
                    novaCaracteristica = JsonConvert.DeserializeObject<Digestao>(dadosAntiga);
                    break;
                case "Meiose":
                    novaCaracteristica = JsonConvert.DeserializeObject<Meiose>(dadosAntiga);
                    break;
            }

            Caracteristicas.Add(novaCaracteristica);
        }

        public Individuo GeraFilhoteComum(Individuo individuo)
        {
            List<Caracteristica> list = new ListaCaracteristicas().TodasCaracteristicas;
            List<Caracteristica> caracteristicasFilhote = new List<Caracteristica>();

            foreach (Caracteristica item in list)
            {
                Caracteristica caracteristicaBase = individuo.Caracteristicas.FirstOrDefault(x => x.Nome == item.Nome);
                if (caracteristicaBase != null)
                {
                    item.ClonarCaracteristica(caracteristicaBase);
                    caracteristicasFilhote.Add(item);
                }
            }

            List<Caracteristica> oldCarca = individuo.Caracteristicas;
            individuo.Caracteristicas = new List<Caracteristica>();

            string dados = JsonConvert.SerializeObject(individuo);
            var novo = JsonConvert.DeserializeObject<Individuo>(dados);
            novo.Geracao += 1;
            novo.ChaceMutacao += 0.001;
            novo.Filiacao = novo.Nome;
            novo.Nome = $"{novo.Especie}{novo.Geracao}";

            foreach (var item in oldCarca)
            {
                individuo.Caracteristicas.Add(item);
                novo.AddCaracteristica(item);
            }


            return novo;
        }

        public Individuo GeraFilhoteMutacao(Individuo individuo)
        {
            int sortudo = new Random().Next(individuo.Caracteristicas.Count);
            int campoSortudo = new Random().Next(0, 10);

            var nomeEspecie = gerarNome();
            individuo.Especie = nomeEspecie;
            individuo.Nome = nomeEspecie+1;
            individuo.Geracao = 1;

            double valorMutado = individuo.ChaceMutacao * (new Random().NextDouble() + new Random().NextDouble());

            string campoModificado = string.IsNullOrEmpty(individuo.Caracteristicas[sortudo].DescValores[campoSortudo]) ? "Não utilizado" : individuo.Caracteristicas[sortudo].DescValores[campoSortudo];
            switch (campoSortudo)
            {
                case 10:
                    individuo.Caracteristicas[sortudo].Multiplicador += valorMutado;
                    campoModificado = "Multiplicador";
                    break;
                default:
                    individuo.Caracteristicas[sortudo].Valores[campoSortudo] += valorMutado;
                    break;
            }

            individuo.Caracteristicas[sortudo].Multiplicador += valorMutado;
            individuo.ChaceMutacao = 0.001;

            ServiceHelper.GetService<MainPage>().DisplayAlert("Mutação", $"Mutação no {individuo.Nome} na caracteristica {individuo.Caracteristicas[sortudo].Nome} no campo {campoModificado}", "OK");
            return individuo;
        }

        string gerarNome()
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < 5)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

    }
}
