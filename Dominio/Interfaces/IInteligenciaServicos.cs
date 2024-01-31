using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IInteligenciaServicos
    {
        void ExecutaDecisao(Individuo individuo, Mundo mundo);
    }
}
