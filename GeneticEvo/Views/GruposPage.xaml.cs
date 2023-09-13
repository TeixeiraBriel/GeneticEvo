using GeneticEvo.Entidades;
using GeneticEvo.Helpers;

namespace GeneticEvo.Views;

public partial class GruposPage : ContentPage
{
	Grupos _grupos;

	public GruposPage()
    {
		InitializeComponent();
        _grupos = ServiceHelper.GetService<Grupos>();

        inicializaGrupos();
    }

    void inicializaGrupos()
    {
        TabelaGrupos.Clear();

        _grupos.listGrupos.ForEach(grupo => {
            TextCell tc = new TextCell();
            tc.Text = grupo.Nome;
            tc.Detail = $"Indiviudos: {grupo.Individuos.Count}";
            tc.Tapped += (s, e) => { Navigation.PushAsync(new DetalhesGrupoPage(grupo)); };

            TabelaGrupos.Add(tc);
        });
    }
}