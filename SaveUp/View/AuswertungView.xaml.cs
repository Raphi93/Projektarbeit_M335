using SaveUp.ViewModel;

namespace SaveUp.View;

public partial class AuswertungView : ContentPage
{
	private AuswertungViewModel _auswertung;
	public AuswertungView()
	{
		_auswertung = new AuswertungViewModel();
		BindingContext = _auswertung;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_auswertung = new AuswertungViewModel();
        BindingContext = _auswertung;
    }
}