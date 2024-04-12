using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogHome.Interface;
using DogHome.Model;
using System.Collections.ObjectModel;

namespace DogHome.ViewModel
{
    public partial class SellersListViewModel : ObservableObject
    {
        [ObservableProperty]
        bool _IsLoading;

        IAuthRepository _AuthRepository;
        public ObservableCollection<Auth> Sellers { get; set; }
        public SellersListViewModel(IAuthRepository authRepository)
        {
            _AuthRepository = authRepository;
            Sellers = new ObservableCollection<Auth>();
            IsLoading = true;
            GetAllSellers();
            IsLoading = false;
        }

        public async void GetAllSellers()
        {
            try
            {
                Sellers.Clear();
                var sellers = await _AuthRepository.GetAllAsync();

                foreach (var seller in sellers)
                {
                    Sellers.Add(seller);
                }
            }
            catch (Exception ex)
            {
                CreateToast("Error: " + ex);
            }
        }
        private async void CreateToast(string text)
        {
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show();
        }

        [RelayCommand]
        private void Back()
        {
            Shell.Current.GoToAsync("//MainDogsPage");
        }

        [RelayCommand]
        private async void Form(Auth seller)
        {
            try
            {
                var parameter = new Dictionary<string, object> { { "Seller", seller } };
                await Shell.Current.GoToAsync("//FormSubmitPage", parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
