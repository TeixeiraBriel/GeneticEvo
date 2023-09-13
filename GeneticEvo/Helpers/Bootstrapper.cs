using GeneticEvo;
using GeneticEvo.Entidades;
using GeneticEvo.Helpers;
using Microsoft.Extensions.Configuration;

namespace IoC
{
    public class Bootstrapper
    {
        public Bootstrapper(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<Grupos>();
            services.AddSingleton<Mundo>();
            services.AddSingleton<MainPage>();
            services.AddSingleton<AlertaMutacoes>();
        }
    }
}