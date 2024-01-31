using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Entidades;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Fotossintese : Caracteristica
    {
        public Fotossintese()
        {
            Observacoes = "Remove da região onde esta o Elemento A e transforma em Elemento B assim gerando energia.";

            Prioridade = 8;
            Nome = EnumCaracteristicas.Fotossintese;
            Multiplicador = 1;
            DescValores[0] = "Quantidade Energia ganha";
            DescValores[1] = "Quantidade Energia gasta";
            DescValores[2] = "Quantidade Energia para priorizar Fotossintese";
            DescValores[3] = "Quantidade transição ElementoAB";
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            double gastoEnergia = -1 * (Valores[1] * Multiplicador);
            double ganhoEnergia = (Valores[0] * Multiplicador);
            if (individuo.Energia + gastoEnergia > 0 && individuo.posNoMundo.Elementos.A - Valores[3] > 0)
            {
                individuo.Energia += gastoEnergia;
                individuo.Energia += ganhoEnergia;
                individuo.posNoMundo.Elementos.A -= Valores[3];
                individuo.posNoMundo.Elementos.B += Valores[3];
            }
            else
            {

            }

            return mundo;
        }
    }
}
