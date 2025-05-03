using Microsoft.Extensions.Logging;
using ShoppingListClient;
using ShoppingListClient.Services;
using Microsoft.EntityFrameworkCore;
using ShoppingListClient.Data;
using Microsoft.Maui.Storage;

namespace ShoppingListClient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		// Initialize SQLite native library
		SQLitePCL.Batteries_V2.Init();
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

		// Add ShoppingListService for API
		builder.Services.AddScoped<ShoppingListService>();

		// Register EF Core local SQLite DbContext
		builder.Services.AddDbContext<ShoppingListDbContext>(options =>
			options.UseSqlite(Path.Combine(FileSystem.AppDataDirectory, "shoppinglist.db")));

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
