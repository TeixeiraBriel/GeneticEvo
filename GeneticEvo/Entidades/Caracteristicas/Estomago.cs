using GeneticEvo.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEvo.Entidades.Caracteristicas
{
    public class Estomago : Caracteristica
    {
        public Estomago()
        {
            Observacoes = "Não tem execução, apenas armazena comida";

            Prioridade = 0;
            Nome = EnumCaracteristicas.Estomago;
            Executar = false;
            Multiplicador = 1;
            DescValores[0] = "Capacidade Maxima";
            Valores[0] = 50;
            DescValores[1] = "Capacidade Atual";
            Valores[1] = 0;
            Executar = false;
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            return mundo;
        }
    }
}
