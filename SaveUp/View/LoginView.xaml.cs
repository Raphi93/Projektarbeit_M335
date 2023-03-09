using SaveUp.ViewModel;
using System.Net;

namespace SaveUp.View;

public partial class LoginView : ContentPage
{
	private LoginViewModel _login = new LoginViewModel();
	public LoginView()
	{
		BindingContext = _login;
		InitializeComponent();

    }
}