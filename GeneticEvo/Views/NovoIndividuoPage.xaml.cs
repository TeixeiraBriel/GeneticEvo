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


}