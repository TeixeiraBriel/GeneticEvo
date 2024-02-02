using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Entidades;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Regeneracao : Caracteristica
    {
        public Regeneracao()
        {
            Observacoes = "";

            Nome = EnumCaracteristicas.Regeneracao;
            Multiplicador = 1;
            DescValores[0] = "Quantidade energia gasta";
            DescValores[1] = "Quantidade vida ganha";
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            double gastoEnergia = (Valores[0] * Multiplicador);
            double ganhoVida = (Valores[1] * Multiplicador);
            
            individuo.Energia -= gastoEnergia;
            individuo.Vida += ganhoVida;

            return mundo;
        }
    }
}