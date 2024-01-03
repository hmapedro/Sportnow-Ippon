using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace SportNow;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        Microsoft.Maui.Hosting.MauiAppBuilder builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
			{
                fonts.AddFont("futura medium condensed bt.ttf", "futuracondensedmedium");
				//fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

#if DEBUG
        builder.Logging.AddDebug();

#endif
        //builder.Services.AddTransient<IDeviceOrientationService, DeviceOrientationService>();
#if IOS
      
#endif
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk4MzIzN0AzMjM0MmUzMDJlMzBLSEZ0a3U0Rlk3UVNjZWxBWmNtclJkOW5jVG1tWm52aGlUNng2THJsWkhnPQ==");

        return builder.Build();
	}
}

