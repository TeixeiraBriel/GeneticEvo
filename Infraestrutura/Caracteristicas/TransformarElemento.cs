using Dominio.Entidades;
using Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Caracteristicas
{
    public class TransformarElemento : Caracteristica
    {
        public TransformarElemento()
        {
            Observacoes = "Consome um Elemento no incio da Geração e gera um segundo no fim da Geração";

            Nome = EnumCaracteristicas.TransformarElemento;
            Multiplicador = 1;
            DescValores[0] = "Elemento Base";
            DescValores[1] = "Elemento Final";
            DescValores[2] = "Quantidade Transformada";
            DescValores[3] = "Energia Ganha";
            DescValores[4] = "Energia Gasta";
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
            //Gasto Energia
            individuo.Energia -= (Valores[4] * Multiplicador);

            double ElementoBase = 0;
            double ElementoFinal = 0;
            bool ElementoReservaSuficiente = false;

            var elementos = individuo.posNoMundo.Elementos;

            ElementoBase = Valores[0] == 0 ? elementos.A :
                           Valores[0] == 1 ? elementos.B :
                           Valores[0] == 2 ? elementos.C :
                                             elementos.D;

            ElementoFinal = Valores[1] == 0 ? elementos.A :
                            Valores[1] == 1 ? elementos.B :
                            Valores[1] == 2 ? elementos.C :
                                              elementos.D;

            ElementoReservaSuficiente = ElementoBase - Valores[2] > 0;

            if (ElementoReservaSuficiente)
            {
                individuo.Energia += (Valores[3] * Multiplicador);

                switch (Valores[0])
                {
                    case 0: elementos.A -= Valores[2]; break;
                    case 1: elementos.B -= Valores[2]; break;
                    case 2: elementos.C -= Valores[2]; break;
                    case 3: elementos.D -= Valores[2]; break;
                }

                Action action = new Action(() =>
                {
                    switch (Valores[1])
                    {
                        case 0: elementos.A += Valores[2]; break;
                        case 1: elementos.B += Valores[2]; break;
                        case 2: elementos.C += Valores[2]; break;
                        case 3: elementos.D += Valores[2]; break;
                    }
                });

                mundo.AcoesFimGeracao.Add(action);
            }
            return mundo;
        }
    }
}
