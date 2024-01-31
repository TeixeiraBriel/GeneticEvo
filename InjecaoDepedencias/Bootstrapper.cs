using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura.Servicos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestrutura
{
    public class Bootstrapper
    {
        public Bootstrapper(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<Mundo>();
            services.AddScoped<IIndividuoServicos, IndividuoServicos>();
            services.AddScoped<IMundoServicos, MundoServicos>();
            services.AddScoped<IInteligenciaServicos, InteligenciaServicos>();
        }
    }
}
