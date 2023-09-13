using GeneticEvo.Entidades;

namespace GeneticEvo.Views;

public partial class DetalhesGrupoPage : ContentPage
{
	Grupo _grupo;

    public DetalhesGrupoPage(Grupo grupo)
	{
		InitializeComponent();
		_grupo = grupo;

        inicializaIndividuos();

    }

	void inicializaIndividuos()
    {
        TabelaIndividuos.Clear();

        _grupo.Individuos.ForEach(Individuo => {
            TextCell tc = new TextCell();
            tc.Text = Individuo.Nome;
            tc.Detail = $"Caracteristicas: {Individuo.Caracteristicas.Count}";

            TabelaIndividuos.Add(tc);
        });
    }
}