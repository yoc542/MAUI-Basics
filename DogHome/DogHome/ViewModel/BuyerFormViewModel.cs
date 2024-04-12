using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogHome.Interface;
using DogHome.Model;
using System.Collections.ObjectModel;

namespace DogHome.ViewModel
{
    [QueryProperty(nameof(Auth), "Auth")]

    public partial class BuyerFormViewModel : ObservableObject
    {
        [ObservableProperty]
        Auth _Auth;

        [ObservableProperty]
        bool _IsLoading;

        IFormRepository _FormRepository;

        public ObservableCollection<BuyerForm> Buyers { get; set; }
        public BuyerFormViewModel(IFormRepository formRepository)
        {
            _FormRepository = formRepository;
            Buyers = new ObservableCollection<BuyerForm>();
            IsLoading = true;
        }

        public async void GetAllBuyers()
        {
            try
            {
                Buyers.Clear();
                var buyers = await _FormRepository.GetAllAsync();
                var filteredBuyers = buyers.Where(buyer => buyer.SellerId == Auth.Id);

                foreach (var buyer in filteredBuyers)
                {
                    Buyers.Add(buyer);
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                CreateToast("Error: " + ex);
            }
        }
        [RelayCommand]
        private void Back()
        {
            var parameter = new Dictionary<string, object> { { "Auth", Auth } };
            Shell.Current.GoToAsync("//SellerDogsPage", parameter);
        }

        [RelayCommand]
        private async void DisplayDetail(BuyerForm buyer)
        {
            var response = await Shell.Current.DisplayActionSheet("Select option", "Ok", null, "Delete");
            if (response == "Delete")
            {
                try
                {
                    await _FormRepository.DeleteAsync(buyer.Id);
                    CreateToast("Data Deleted successfully");
                }
                catch (Exception ex)
                {
                    CreateToast("Error occured while deleting data");
                }
            }
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
