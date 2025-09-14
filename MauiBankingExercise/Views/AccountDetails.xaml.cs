namespace MauiBankingExercise.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using MauiBankingExercise.ViewModels;
using System;

public partial class AccountDetails : ContentPage
{
	public AccountDetails(AccountDetails vm)
	{
		InitializeComponent();

		BindingContext = vm;
    }
}