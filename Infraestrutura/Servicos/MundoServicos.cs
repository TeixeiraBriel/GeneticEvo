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
        private Mundo _mundo;
        public MundoServicos(Mundo mundo)
        {
            _mundo = mundo;
        }

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
                new InteligenciaServicos().ExecutaDecisao(Individuo, mundo);
            }

            foreach(Action acoes in mundo.AcoesFimGeracao)
            {
                acoes.Invoke();
            }

            mundo.AcoesFimGeracao.Clear();
            mundo.Geracao++;
        }

        public void ZeraMundo()
        {
            _mundo = new Mundo();
        }

        public Mundo GetMundo() => _mundo;
    }
}
