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
            Observacoes = "";

            Prioridade = 10;
            Nome = EnumCaracteristicas.TransformarElemento;
            Multiplicador = 1;
            DescValores[0] = "Elemento Base";
            DescValores[1] = "Elemento Final";
            DescValores[2] = "Quantidade Transformada";
            DescValores[3] = "Energia Ganha";
        }

        public override Mundo Executa(Individuo individuo = null, Mundo mundo = null, TipoCaracteristicas tipoCaracteristicas = TipoCaracteristicas.Acao)
        {
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
                individuo.Energia += Valores[3];

                switch (Valores[0])
                {
                    case 0: elementos.A -= Valores[2]; break;
                    case 1: elementos.B -= Valores[2]; break;
                    case 2: elementos.C -= Valores[2]; break;
                    case 3: elementos.D -= Valores[2]; break;
                }

                switch (Valores[1])
                {
                    case 0: elementos.A += Valores[2]; break;
                    case 1: elementos.B += Valores[2]; break;
                    case 2: elementos.C += Valores[2]; break;
                    case 3: elementos.D += Valores[2]; break;
                }
            }


            /*
            double ElementoBase = 0;
            double ElementoFinal = 0;
            bool ElementoReservaSuficiente = false;

            switch (Valores[0])
            {
                case 0:
                    //Elemento A
                    ElementoBase = individuo.posNoMundo.Elementos.A;
                    ElementoReservaSuficiente = individuo.posNoMundo.Elementos.A - Valores[2] > 0;
                    break;
                case 1:
                    //Elemento B
                    ElementoBase = individuo.posNoMundo.Elementos.B;
                    ElementoReservaSuficiente = individuo.posNoMundo.Elementos.B - Valores[2] > 0;
                    break;
                case 2:
                    //Elemento C
                    ElementoBase = individuo.posNoMundo.Elementos.C;
                    ElementoReservaSuficiente = individuo.posNoMundo.Elementos.C - Valores[2] > 0;
                    break;
                case 3:
                    //Elemento D
                    ElementoBase = individuo.posNoMundo.Elementos.D;
                    ElementoReservaSuficiente = individuo.posNoMundo.Elementos.D - Valores[2] > 0;
                    break;
            }

            ElementoFinal = Valores[1] == 0 ? individuo.posNoMundo.Elementos.A :
                            Valores[1] == 1 ? individuo.posNoMundo.Elementos.B :
                            Valores[1] == 2 ? individuo.posNoMundo.Elementos.C :
                            individuo.posNoMundo.Elementos.A;

            if (ElementoReservaSuficiente)
            {
                individuo.Energia += (Valores[3]);
                ElementoBase -= Valores[2];
                ElementoFinal += Valores[2];
            }
            */
            return mundo;
        }
    }
}
