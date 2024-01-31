using Dominio.Entidades;
using Dominio.Enumeradores;

namespace Dominio.Interfaces
{
    public interface IIndividuoServicos
    {
        Individuo GeraFilhoteComum(Individuo individuo, int Ano);
        Individuo GeraFilhoteMutacao(Individuo novo);
        bool validaMutacao(Individuo individuo);
        void AdicionarIndividuo(Mundo mundo, Individuo individuo);
        void AdicionarCaracteristica(IIndividuo individuo, EnumCaracteristicas caracteristicasEnum, double[] Valores);
        void AdicionarCaracteristica(Individuo individuo, EnumCaracteristicas caracteristicasEnum, double[] Valores);
        void RemoveIndividuo(Mundo mundo, Individuo ind);
    }
}
