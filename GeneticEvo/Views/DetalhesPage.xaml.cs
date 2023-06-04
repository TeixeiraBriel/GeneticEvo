using GeneticEvo.Entidades;
using GeneticEvo.Helpers;
using GeneticEvo.Views;

namespace GeneticEvo;

public partial class DetalhesPage : ContentPage
{
    Mundo mundo;
    List<RegistroEspecie> Especies;
    Individuo especie = null;
    int indiceAtual = 0;    


    public DetalhesPage()
    {
        InitializeComponent();
        mundo = ServiceHelper.GetService<Mundo>();
        Especies = mundo.registroEspecies;

        especie = mundo.EspecieList.FirstOrDefault(x => x.Especie == Especies[indiceAtual].Nome);
        AtualizaTela();
    }

    void AtualizaTela()
    {
        lblNomeEspecie.Text = especie.Especie;
        CarregaCaracteristicas();
    }

    void CarregaCaracteristicas()
    {
        PanelCaracteristicas.Children.Clear();
        foreach (Caracteristica caracteristica in especie.Caracteristicas)
        {
            Button newBtn = new Button() { Text = $"{caracteristica.Nome}" };
            newBtn.Clicked += (s, e) => Navigation.PushAsync(new DetalhesCaracteristica(caracteristica));
            PanelCaracteristicas.Add(newBtn);
        }
    }

    private void EspecieAnt(object sender, EventArgs e)
    {
        if ((indiceAtual - 1) >= 0)
            indiceAtual--;
        else
            indiceAtual = Especies.Count - 1;
        
        especie = mundo.EspecieList.FirstOrDefault(x => x.Especie == Especies[indiceAtual].Nome);
        AtualizaTela();
    }

    private void EspecieProx(object sender, EventArgs e)
    {
        if (Especies.Count > (indiceAtual + 1))
            indiceAtual++;
        else
            indiceAtual = 0;

        especie = mundo.EspecieList.FirstOrDefault(x => x.Especie == Especies[indiceAtual].Nome);
        AtualizaTela();
    }
}

