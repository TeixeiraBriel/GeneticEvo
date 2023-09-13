using GeneticEvo.Views;

namespace GeneticEvo;

public partial class OpcoesPage : ContentPage
{
	List<Tuple<string, ContentPage>> _pages = new List<Tuple<string, ContentPage>>()
	{
		new Tuple<string, ContentPage>("Grupos", new GruposPage()),
		new Tuple<string, ContentPage>("Novo Individuo", new NovoIndividuoPage())
    };

	public OpcoesPage()
	{
		InitializeComponent();
		inicializaOpcoes();

    }

	void inicializaOpcoes()
	{
        MainPanel.Clear();

        _pages.ForEach(p => {
			Button btn = new Button() { Text = p.Item1 };
			btn.Clicked += (s, e) =>{Navigation.PushAsync(p.Item2);};

            MainPanel.Add(btn);	
		});
	}
}