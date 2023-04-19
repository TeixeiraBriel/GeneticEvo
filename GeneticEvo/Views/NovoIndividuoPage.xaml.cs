using GeneticEvo.Entidades;
using GeneticEvo.Helpers;

namespace GeneticEvo;

public partial class NovoIndividuoPage : ContentPage
{
    Mundo mundo;
    ListaCaracteristicas listaCaracteristicas;
    Individuo NovoIndividuo;

    public NovoIndividuoPage()
    {
		InitializeComponent();
        mundo = ServiceHelper.GetService<Mundo>();
        listaCaracteristicas = new ListaCaracteristicas();
        NovoIndividuo = new Individuo();
    }

    private void Button_Confirmar(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(inputNome.Text) ||
        string.IsNullOrWhiteSpace(inputEspecie.Text) ||
        string.IsNullOrWhiteSpace(inputFiliacao.Text) ||
        string.IsNullOrWhiteSpace(inputVida.Text) ||
        string.IsNullOrWhiteSpace(inputFome.Text) ||
        string.IsNullOrWhiteSpace(inputEnergia.Text) ||
        string.IsNullOrWhiteSpace(inputTempoVida.Text) ||
        string.IsNullOrWhiteSpace(inputOrigem.Text))
        {
            DisplayAlert("Erro", "Por favor preencha todos os campos.", "OK");
        }
        else
        {
            DisplayAlert("Sucesso", "Seus dados foram enviados!", "ok");
        }
    }
}