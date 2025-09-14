namespace MauiBankingExercise.Views;
using MauiBankingExercise.ViewModels;

public partial class TransactionScreen : ContentPage
{
	public TransactionScreen()
	{
		InitializeComponent();
	}

		public TransactionScreen(TransactionScreenViewModel vm)
		{
			InitializeComponent();
			BindingContext = vm;
    }
}
