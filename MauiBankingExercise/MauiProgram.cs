using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using MauiBankingExercise.Services;
using MauiBankingExercise.ViewModels;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using MauiBankingExercise.Interface;


namespace MauiBankingExercise
{
    public static class MauiProgram
    {
        public static object CreateMauiApp()
        {
           

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

           

#if DEBUG
            builder.Logging.AddDebug();


            builder.Services.AddSingleton<MauiBankingExercise.Configurations.ApplicationSettings>();


            builder.Services.AddSingleton<MauiBankingExercise.Interface.IBankingService, BankingApiService>();
            builder.Services.AddTransient<MauiBankingExercise.ViewModels.CustomerSelectionScreenViewModel>();


            builder.Services.AddTransient<MauiBankingExercise.Views.CustomerSelectionScreenView>();
            builder.Services.AddSingleton<MauiBankingExercise.Views.AccountDetails>();
           
            builder.Services.AddSingleton<MauiBankingExercise.Views.CustomerDashBoard>();
            builder.Services.AddSingleton<MauiBankingExercise.ViewModels.CustomerDashBoardViewModel>();
            builder.Services.AddSingleton<MauiBankingExercise.Views.TransactionScreen>();


#endif

            return builder.Build();
        }
    }
}


