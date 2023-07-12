using GeneticEvo.Entidades;
using GeneticEvo.Helpers;

namespace GeneticEvo;

public partial class NovoIndividuoPage : ContentPage
{
    Mundo mundo;
    ListaCaracteristicas listaCaracteristicas;
    Individuo NovoIndividuo;
    List<string> individuos;

    public NovoIndividuoPage()
    {
        InitializeComponent();
        mundo = ServiceHelper.GetService<Mundo>();
        listaCaracteristicas = new ListaCaracteristicas();
        NovoIndividuo = new Individuo();

        individuos = new List<string>() { "biel", "Takato", "Theus", "Takita" };
        atribuiListView("");
    }


    void atribuiListView(string filtro)
    {
        listView.Clear();
        foreach (var item in individuos)
        {
            if (item.ToLower().Contains(filtro.ToLower()))
            {
                listView.Add(new Label() { Text = item });
            }
        }
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
            DisplayAlert("Sucesso", "Seus dados foram enviados!", "okay");
        }
    }

    private void btnAddEspecie_Clicked(object sender, EventArgs e)
    {
        listView.IsVisible = !listView.IsVisible;
    }

    private void inputFiltro_PropertyChanged(object sender, TextChangedEventArgs e)
    {
        atribuiListView(e.NewTextValue);

    }

    private void escolherEspecie(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var buttonText = button.Text;

        switch (buttonText)
        {
            case "Digestão":
                ContentLabel.Text = "Classe de Digestão";
                break;
            //case "Fotossíntese":
            //    ContentLabel.Text = "Classe de Fotossíntese";
            //    break;
            //case "Meiose":
            //    ContentLabel.Text = "Classe de Meiose";
            //    break;
            default:
                break;
        }

        // Armazena a escolha feita em preferências de aplicativo
        Preferences.Set("ClasseSelecionada", buttonText);
    }

    private void btnMostrarEspecie_Clicked(Object sender, EventArgs e)
    {
        containerCaracteristicas.IsVisible= !containerCaracteristicas.IsVisible;
    }
}