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
            DescValores[0] = "Quantidade minima para começar regenerar";
            DescValores[1] = "Quantidade energia gasta";
            DescValores[2] = "Quantidade vida ganha";
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            if (individuo.Vida < Valores[0])
            {
                double gastoEnergia = (Valores[1] * Multiplicador) * -1;
                double ganhoVida = (Valores[2] * Multiplicador);
                if (individuo.Energia + gastoEnergia > 0)
                {
                    individuo.Energia += gastoEnergia;
                    individuo.Vida += ganhoVida;
                }
            }

            return mundo;
        }
    }
}