using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogHome.Interface;
using DogHome.Model;


namespace DogHome.ViewModel
{
    [QueryProperty(nameof(Auth), "Auth")]
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        Auth _Auth;

        IAuthRepository _AuthRepository;
        public ProfileViewModel(IAuthRepository authRepository)
        {
            _AuthRepository = authRepository;
        }

        [RelayCommand]
        private void Back()
        {

            var navigationParameter = new Dictionary<string, object>{
              { "Auth", Auth}
               };

            Shell.Current.GoToAsync("//SellerDogsPage", navigationParameter);
        }

        [RelayCommand]
        private async void Edit()
        {
            var seller = await _AuthRepository.GetByEmailAsync(Auth.Email);
            if (seller != null)
            {
                if (!string.IsNullOrWhiteSpace(Auth.FullName) && !string.IsNullOrWhiteSpace(Auth.Phone) && !string.IsNullOrWhiteSpace(Auth.Location) && !string.IsNullOrWhiteSpace(Auth.ShopName))
                {
                    await _AuthRepository.UpdateAsync(Auth);
                    CreateToast("Data updated");
                }
                else
                    await Shell.Current.DisplayAlert("Error", "Fill all data", "Ok");
            }

            else
                await Shell.Current.DisplayAlert("error", "error updating", "a");
        }

        private async void CreateToast(string text)
        {
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show();
        }

    }
}

