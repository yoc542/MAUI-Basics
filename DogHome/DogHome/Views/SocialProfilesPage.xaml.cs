namespace DogHome.ViewModel;

public partial class SocialProfilesPage : ContentView
{
    public SocialProfilesPage()
    {
        InitializeComponent();
        BindingContext = new SocialProfilesViewModel();
    }
}