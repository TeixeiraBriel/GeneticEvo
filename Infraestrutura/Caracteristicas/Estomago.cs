using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Entidades;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Estomago : Caracteristica
    {
        public Estomago()
        {
            Observacoes = "Não tem execução, apenas armazena comida";

            Nome = EnumCaracteristicas.Estomago;
            Executar = false;
            Multiplicador = 1;
            DescValores[0] = "Capacidade Maxima";
            DescValores[1] = "Capacidade Atual";
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            return mundo;
        }
    }
}
