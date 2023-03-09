using SaveUp.ViewModel;

namespace SaveUp.View;

public partial class SettingView : ContentPage
{
	private SettingViewModel _setting = new SettingViewModel();
	public SettingView()
	{
		BindingContext = _setting;
		InitializeComponent();
	}
}