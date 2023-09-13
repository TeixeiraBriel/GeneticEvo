using GeneticEvo.Entidades;

namespace GeneticEvo.Views;

public partial class DetalhesCaracteristica : ContentPage
{
	Caracteristica _caracteristica;
	public DetalhesCaracteristica(Caracteristica carcateristica)
	{
		InitializeComponent();
        _caracteristica = carcateristica;
        this.Title = carcateristica.Nome.ToString();
		inicializaDados();

    }

	void inicializaDados()
    {
        PainelCentral.Children.Add(
            new Label() { Text = $"Prioridade: {_caracteristica.Prioridade}" });
        PainelCentral.Children.Add(
            new Label() { Text = $"Observação: {_caracteristica.Observacoes}" });
        PainelCentral.Children.Add(
            new Label() { Text = $"Multiplicador: {_caracteristica.Multiplicador}" });

        for (int i = 0; i < 10; i++)
		{
			if (string.IsNullOrEmpty(_caracteristica.DescValores[i]))
				continue;

			PainelCentral.Children.Add(
				new Label() { Text = $"{_caracteristica.DescValores[i]}:{_caracteristica.Valores[i]}" });
		}
	}
}