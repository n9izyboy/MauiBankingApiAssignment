using System.Security.Cryptography.X509Certificates;
using MauiBankingExercise.ViewModels;
using MauiBankingExercise.Views;

namespace MauiBankingExercise.Views;

public partial class CustomerDashBoard : ContentPage
{
	public CustomerDashBoard(CustomerDashBoardViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;


    }
}