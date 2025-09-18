namespace MauiBankingExercise;
using MauiBankingExercise.Views;

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("customers", typeof(CustomerSelectionScreenView));
            Routing.RegisterRoute("transaction", typeof(TransactionScreen));
            Routing.RegisterRoute("customerDashBoard", typeof(CustomerDashBoard));
        }
    }

