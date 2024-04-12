using DogHome.ViewModel;

namespace DogHome.Views;

public partial class PersonalDetailPage : ContentView
{
    public PersonalDetailPage()
    {
        InitializeComponent();
        BindingContext = new PersonalDetailViewModel();
    }
}