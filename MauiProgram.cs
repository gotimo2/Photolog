using Microsoft.Extensions.Logging;
using Photolog.Helpers;
using Plugin.LocalNotification;

namespace Photolog;

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
            })
            .UseLocalNotification(config =>
            {
                config.AddAndroid(android =>
                {
                    android.AddChannel(new Plugin.LocalNotification.AndroidOption.NotificationChannelRequest
                    {
                        Id = NotificationScheduler.CHANNEL_NAME,
                        Name = "general",
                        Description = "General photolog notification category"
                    }); ;
                });
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
