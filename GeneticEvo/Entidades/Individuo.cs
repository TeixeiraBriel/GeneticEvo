using GeneticEvo.Entidades.Caracteristicas;
using GeneticEvo.Enumeradores;
using GeneticEvo.Helpers;
using Microsoft.Maui.Controls.Platform;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public bool Vivo { get; set; }

        public Individuo()
        {
            ChaceMutacao = 0.02;
            Vivo = true;
        }

        public List<Caracteristica> Caracteristicas { get; set; }

        public Mundo ExecutaCaracteristicas(Mundo mundo)
        {
            if (Vivo)
            {
                for (int i = 10; i > 0; i--)
                {
                    List<Caracteristica> _caracteristicasAtual = Caracteristicas.FindAll(x => x.Prioridade == i && x.Executar);
                    foreach (Caracteristica caracteristicas in _caracteristicasAtual)
                    {
                        mundo = caracteristicas.Executa(this, mundo);
                    }
                }
            }

            if (Vida <= 0 && Vivo)
                definirComoMorto();

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
                case EnumCaracteristicas.Fotossintese:
                    novaCaracteristica = JsonConvert.DeserializeObject<Fotossintese>(dadosAntiga);
                    break;
                case EnumCaracteristicas.Digestao:
                    novaCaracteristica = JsonConvert.DeserializeObject<Digestao>(dadosAntiga);
                    break;
                case EnumCaracteristicas.Meiose:
                    novaCaracteristica = JsonConvert.DeserializeObject<Meiose>(dadosAntiga);
                    break;
                case EnumCaracteristicas.Morder:
                    novaCaracteristica = JsonConvert.DeserializeObject<Morder>(dadosAntiga);
                    break;
                case EnumCaracteristicas.Estomago:
                    novaCaracteristica = JsonConvert.DeserializeObject<Estomago>(dadosAntiga);
                    break;
                case EnumCaracteristicas.Regeneracao:
                    novaCaracteristica = JsonConvert.DeserializeObject<Regeneracao>(dadosAntiga);
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
            novo.ChaceMutacao += 0.01;
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
            string campoModificado = string.IsNullOrEmpty(individuo.Caracteristicas[sortudo].DescValores[campoSortudo]) ? "Não utilizado" : individuo.Caracteristicas[sortudo].DescValores[campoSortudo];

            if (campoModificado.Equals("Não utilizado"))
            {
                return individuo;
            }

            var nomeEspecie = gerarNome();
            individuo.Especie = nomeEspecie;
            individuo.Nome = nomeEspecie + 1;
            individuo.Geracao = 1;

            double valorMutado = individuo.ChaceMutacao * (new Random().NextDouble() + new Random().NextDouble());

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
            individuo.ChaceMutacao = 0.01;

            ServiceHelper.GetService<AlertaMutacoes>().Mutacoes.Add($"Mutação no individuo {individuo.Nome} filho de {individuo.Filiacao} na caracteristica {individuo.Caracteristicas[sortudo].Nome} no campo {campoModificado}");
            ServiceHelper.GetService<AlertaMutacoes>().HouveMutacoes = true;
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

        public void definirComoMorto()
        {
            Vivo = false;
            TempoVida = 0;
        }

        public bool validaMutacao()
        {
            var rand = new Random().Next(1, 5);
            double validador = rand * ChaceMutacao;

            if (validador > 0.7)
            {
                return true;
            }

            return false;
        }
    }
}
