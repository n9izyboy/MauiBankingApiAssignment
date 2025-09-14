using MauiBankingExercise.Views;
using MauiBankingExercise.Models;

using MauiBankingExercise.ViewModels;
namespace MauiBankingExercise.Views;

public partial class CustomerSelectionScreenView : ContentPage
{
	public CustomerSelectionScreenView(CustomerSelectionScreenViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

}