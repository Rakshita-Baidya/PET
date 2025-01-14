using Blazored.Toast;
using Microsoft.Extensions.Logging;
using PET.Helper;
using PET.Interfaces;
using PET.Models;
using PET.Services;

namespace PET
{
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredToast();
            builder.Services.AddSingleton<MapUser>();
            builder.Services.AddSingleton<PreferencesStoreClone>();

            // register services
            builder.Services.AddSingleton<PreferencesStoreClone>();
            builder.Services.AddSingleton<PageTitleService>();
            builder.Services.AddTransient<IUser, UserService>();
            builder.Services.AddTransient<ICurrency, CurrenyService>();
            builder.Services.AddTransient<ITag, TagService>();
            builder.Services.AddTransient<ITransaction, TransactionService>();
            builder.Services.AddTransient<IDebt, DebtService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
