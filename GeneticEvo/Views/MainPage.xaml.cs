using GeneticEvo.Controladores;
using GeneticEvo.Entidades;
using GeneticEvo.Helpers;

namespace GeneticEvo;

public partial class MainPage : ContentPage
{
    public bool Inicializado = false;
    int count = 0;
    Controlador _controlador;
    List<Tuple<string, int>> filtroEspecie;
    bool FiltrarPorEspecie = false;

    public MainPage()
    {
        InitializeComponent();

        _controlador = new Controlador();
        _controlador.iniciaAlgaBase();

        lblTitulo.Text = $"Ano: {_controlador.mundo.Geracao} com {_controlador.mundo.EspecieList.Count} Individuos";
        CriarListIndividuos();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _controlador.avancaGeracao();

        lblTitulo.Text = $"Ano: {_controlador.mundo.Geracao} com {_controlador.mundo.EspecieList.Count} Individuos";

        SemanticScreenReader.Announce(CounterBtn.Text);
        CriarListIndividuos();
    }

    private void CriarListIndividuos()
    {
        StackVertical.Children.Clear();
        filtroEspecie = new List<Tuple<string, int>>();
        List<Individuo> Individuos = _controlador.mundo.EspecieList;

        foreach (Individuo individo in Individuos)
        {
            if (FiltrarPorEspecie)
            {
                var reg = filtroEspecie.Find(x => x.Item1 == individo.Especie);
                if (reg == null)
                {
                    filtroEspecie.Add(new Tuple<string, int>(individo.Especie, 1));
                }
                else
                {
                    filtroEspecie.Add(new Tuple<string, int>(reg.Item1, reg.Item2 + 1));
                    filtroEspecie.Remove(reg);
                }
            }
            else
            {
                StackVertical.Add(new Label()
                {
                    Text =
                    $"Nome:{individo.Nome}    " +
                    $"Especie:{individo.Especie}    " +
                    $"Filiacao:{individo.Filiacao}    " +
                    $"Vida:{individo.Vida}   " +
                    $"Fome:{individo.Fome}   " +
                    $"Energia:{individo.Energia}   " +
                    $"TempoVida:{individo.TempoVida}   " +
                    $"Origem:{individo.DataOrigem}"
                });
            }
        }

        if (FiltrarPorEspecie)
        {
            foreach (var especie in filtroEspecie)
            {
                StackVertical.Add(new Label()
                {
                    Text = $"Especie: {especie.Item1}   Quantidade: {especie.Item2}"
                });

            }
        }
    }

    private void OnTodosClicked(object sender, EventArgs e)
    {
        FiltrarPorEspecie = false;
        CriarListIndividuos();
    }

    private void OnEspecieClicked(object sender, EventArgs e)
    {
        FiltrarPorEspecie = true;
        CriarListIndividuos();

    }
}

