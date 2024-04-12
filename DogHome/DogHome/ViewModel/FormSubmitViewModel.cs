using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogHome.Interface;
using DogHome.Model;

namespace DogHome.ViewModel
{
    [QueryProperty(nameof(Seller), "Seller")]
    public partial class FormSubmitViewModel : ObservableObject
    {
        [ObservableProperty]
        Auth _Seller;

        [ObservableProperty]
        TimeSpan _TimeOnly;

        [ObservableProperty]
        string _DateOnly;

        [ObservableProperty]
        BuyerForm _Buyer;

        IFormRepository _FormRepositry;
        public FormSubmitViewModel(IFormRepository formRepository)
        {
            _FormRepositry = formRepository;
            Buyer = new BuyerForm();
        }

        [RelayCommand]
        private async void FormSubmit()
        {
            if (DateOnly == null || TimeOnly == null)
            {
                await Shell.Current.DisplayAlert("Form", "Fill all the details", "Ok");
                return;
            }
            string Newtime = $"{TimeOnly}";
            DateTime parsedTime = DateTime.Parse(Newtime);
            DateTime parsedDate = DateTime.Parse(DateOnly);
            DateTime combinedDateTime = new DateTime(
             parsedDate.Year,
             parsedDate.Month,
             parsedDate.Day,
             parsedTime.Hour,
             parsedTime.Minute,
             parsedTime.Second
         );

            Buyer.VisitTime = combinedDateTime;

            Buyer.SellerId = Seller.Id;

            if (string.IsNullOrWhiteSpace(Buyer.BuyerName) || string.IsNullOrWhiteSpace(Buyer.BuyerContact) || string.IsNullOrWhiteSpace(Buyer.Query) || string.IsNullOrWhiteSpace(Buyer.InterestedDogs))
            {
                await Shell.Current.DisplayAlert("Form", "Fill all the details", "Ok");
                return;
            }
            try
            {
                await _FormRepositry.CreateAsync(Buyer);
                await Shell.Current.GoToAsync("//SellersListPage");
                CreateToast("Form submitted Successfully");
                Buyer = null;
            }
            catch (Exception ex)
            {
                CreateToast("Error" + ex);
            }
        }

        [RelayCommand]
        private async void Back()
        {
            Shell.Current.GoToAsync("//SellersListPage");
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
