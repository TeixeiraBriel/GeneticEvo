using Dominio.Entidades;
using Dominio.Entidades;
using Dominio.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Servicos
{
    public class MundoServicos : IMundoServicos
    {
        public void AvancaGeracao(Mundo mundo)
        {


            List<Individuo> EspecieListAtual = new List<Individuo>();
            foreach (var item in mundo.EspecieList)
            {
                EspecieListAtual.Add(item);
            }

            //List<Caracteristica> _caracteristicasAtual = Caracteristicas.FindAll(x => x.Prioridade == i && x.Executar);//ValidarVelocidade
            foreach (Individuo Individuo in EspecieListAtual)
            {
                mundo = new IndividuoServicos().ExecutaCaracteristicas(Individuo, mundo);
            }
            mundo.Geracao++;
        }

    }
}
