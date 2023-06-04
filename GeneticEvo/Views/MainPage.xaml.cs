using GeneticEvo.Controladores;
using GeneticEvo.Entidades;
using GeneticEvo.Helpers;

namespace GeneticEvo;

public partial class MainPage : ContentPage
{
    Controlador _controlador;
    List<Tuple<string, int>> filtroEspecie;

    public MainPage()
    {
        InitializeComponent();

        _controlador = new Controlador();
        _controlador.iniciaMundo();

        atualizaDadosTela();
    }

    void atualizaDadosTela()
    {
        lblAno.Text = $"Ano: {_controlador.mundo.Geracao}";
        lblIndividuos.Text = $"{_controlador.mundo.EspecieList.Count} Individuos";
        CriarListIndividuos();
    }

    private void CriarListIndividuos()
    {
        StackVertical.Children.Clear();
        filtroEspecie = new List<Tuple<string, int>>();
        List<Individuo> Individuos = _controlador.mundo.EspecieList;
        List<RegistroEspecie> listaEspecies = _controlador.mundo.registroEspecies;

        foreach (Individuo individo in Individuos)
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

        foreach (var especie in filtroEspecie)
        {
            StackVertical.Add(new Label()
            {
                Text = $"Especie: {especie.Item1}   Quantidade: {especie.Item2}"
            });

        }
    }

    private void OnEspecieClicked(object sender, EventArgs e)
    {
        atualizaDadosTela();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _controlador.avancaGeracao();
        atualizaDadosTela();
    }

    private void OnReiniciarClicked(object sender, EventArgs e)
    {
        _controlador.iniciaMundo();
        atualizaDadosTela();
    }
}

