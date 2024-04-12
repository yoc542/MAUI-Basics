using DogHome.ViewModel;

namespace DogHome.Views;

public partial class CreateAccountPage : ContentView
{
    public CreateAccountPage()
    {
        InitializeComponent();
        BindingContext = new RegisterFormViewModel();
    }
}