using SaveUp.ViewModel;

namespace SaveUp.View;

public partial class EinfuegenView : ContentPage
{
	private EinfuegenViewModel _einfuegen = new EinfuegenViewModel();
	public EinfuegenView()
	{
		BindingContext = _einfuegen;
		InitializeComponent();
	}
}