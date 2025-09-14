using MauiBankingExercise.Services;
using MauiBankingExercise.ViewModels;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Extensions.Logging;
using SQLitePCL; 

namespace MauiBankingExercise
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Batteries_V2.Init(); 

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = BankingDataBaseServices.GetInstance().GetDatabasePath();
            var db = new SQLite.SQLiteConnection(dbPath);

            builder.Services.AddSingleton(db);
            builder.Services.AddSingleton<BankingSeeder>();

#if DEBUG
            builder.Logging.AddDebug();



            builder.Services.AddSingleton<MauiBankingExercise.Services.BankingSeeder>();
            builder.Services.AddSingleton<MauiBankingExercise.ViewModels.BankViewModel>();
            builder.Services.AddTransient<MauiBankingExercise.Views.CustomerSelectionScreenView>();
            builder.Services.AddSingleton<MauiBankingExercise.Views.AccountDetails>();
            builder.Services.AddSingleton<MauiBankingExercise.ViewModels.CustomerSelectionScreenViewModel>();
            builder.Services.AddSingleton<MauiBankingExercise.Views.CustomerDashBoard>();
            builder.Services.AddSingleton<MauiBankingExercise.ViewModels.CustomerDashBoardViewModel>();
            builder.Services.AddSingleton<MauiBankingExercise.Views.TransactionScreen>();


#endif

            return builder.Build();
        }
    }
}


