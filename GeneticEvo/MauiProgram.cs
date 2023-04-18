using GeneticEvo.Entidades;
using IoC;
using Microsoft.Extensions.Logging;
//HOM
namespace GeneticEvo;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		_ = new Bootstrapper(services, configuration);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
		return app;
	}
}
