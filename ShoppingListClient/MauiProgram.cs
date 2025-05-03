using Microsoft.Extensions.Logging;
using ShoppingListClient;
using ShoppingListClient.Services;

namespace ShoppingListClient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddMauiBlazorWebView();

		// Configure HTTP client
		builder.Services.AddSingleton(sp =>
			new HttpClient { BaseAddress = new Uri("http://localhost:5254") });

		// Add ShoppingListService
		builder.Services.AddScoped<ShoppingListService>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
