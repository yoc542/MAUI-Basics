using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DogHome.ViewModel
{
    public partial class OnboardingViewModel : ObservableObject
    {

        [RelayCommand]
        private void DisplayLoginPage()
        {
            Shell.Current.GoToAsync("//LoginPage");
        }
        [RelayCommand]
        private void DisplayHomePage()
        {
            Shell.Current.GoToAsync("//MainDogsPage");
        }
    }
}
