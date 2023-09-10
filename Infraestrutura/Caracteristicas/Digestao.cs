using Dominio.Entidades;
using Dominio.Enumeradores;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Digestao : Caracteristica
    {
        //CAPAZ DE DIGERIR QUALQUER ALIMENTO????
        public Digestao()
        {
            Observacoes = "";

            Prioridade = 8;
            Nome = EnumCaracteristicas.Digestao;
            DescValores[0] = "Multiplo ganho Energia";
            Valores[0] = 5;
            DescValores[1] = "Quantidade comida capaz de digerir";
            Valores[1] = 10;
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            if (individuo.Caracteristicas.Exists(x => x.Nome == EnumCaracteristicas.Estomago))
            {
                double qtdComidaEstomago = individuo.Caracteristicas.FirstOrDefault(x => x.Nome == EnumCaracteristicas.Estomago).Valores[1];
                double qtdComidaConsumida = Valores[1];

                if (qtdComidaEstomago < 0)
                    qtdComidaConsumida = 0;
                else if (qtdComidaEstomago - qtdComidaConsumida < 0)
                    qtdComidaConsumida = qtdComidaEstomago;

                individuo.Caracteristicas.FirstOrDefault(x => x.Nome == EnumCaracteristicas.Estomago).Valores[1] += qtdComidaConsumida * -1;
                individuo.Energia += qtdComidaConsumida * Valores[0];

            }

            return mundo;
        }
    }
}
