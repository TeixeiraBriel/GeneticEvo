using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura;
using Microsoft.Extensions.Logging;

namespace GeneticEvoBlazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        var services = builder.Services;
        var configuration = builder.Configuration;

        builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

        _ = new Bootstrapper(services, configuration);

        builder.Services.AddMauiBlazorWebView();
		builder.Services.AddSingleton<IIndividuo, Individuo>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
