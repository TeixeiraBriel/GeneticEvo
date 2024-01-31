using Dominio.Entidades;
using Dominio.Enumeradores;
using Dominio.Entidades;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Canibal : Caracteristica
    {
        public Canibal()
        {
            Observacoes = "Não tem execução, apenas armazena comida";

            Nome = EnumCaracteristicas.Canibal;
            DescValores[0] = "Canibalismo: Positivo(Sim), Negativo(Não)";
            DescValores[1] = "Canibalismo Filhos: Positivo(Sim), Negativo(Não)";
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            return mundo;
        }

        public Individuo buscaPresa(Individuo individuo, Mundo mundo)
        {
            Individuo presa = new Individuo();

            if (Valores[4] > 0 && Valores[5] > 0)
            {
                //Canibal geral
                presa = mundo.EspecieList.FirstOrDefault(x => (x.PosicaoY == individuo.PosicaoY && x.PosicaoX == individuo.PosicaoX) && x != individuo && x.Vida > 0);
            }
            else if (Valores[4] > 0 && Valores[5] < 0)
            {
                //Canibal não filhotes
                presa = mundo.EspecieList.FirstOrDefault(x => (x.PosicaoY == individuo.PosicaoY && x.PosicaoX == individuo.PosicaoX) && x != individuo && x.Vida > 0);
            }
            else if (Valores[4] > 0 && presa == null && Valores[5] < 0)
            {
                //Canibal Apenas Filhotes
                presa = mundo.EspecieList.FirstOrDefault(x => (x.PosicaoY == individuo.PosicaoY && x.PosicaoX == individuo.PosicaoX) && x != individuo && x.Vida > 0);
            }

            return presa;
        }
    }
}