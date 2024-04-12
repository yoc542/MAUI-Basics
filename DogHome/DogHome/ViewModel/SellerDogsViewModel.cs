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
    public partial class SellerDogsViewModel : ObservableObject
    {
        [ObservableProperty]
        Auth _Auth;
        public ObservableCollection<Dog> Dogs { get; set; }

        IRepository _DogRepository;

        IBreedRepository _BreedRepository;

        [ObservableProperty]
        bool _IsLabelvisible;

        [ObservableProperty]
        float _Opacity;

        private string _SearchTerm;
        public string SearchTerm
        {
            get => _SearchTerm;
            set
            {
                SetProperty(ref _SearchTerm, value);
                FilterBySearchTerm();
            }
        }
        private string _PickerValue;
        public string PickerValue
        {
            get => _PickerValue;
            set
            {
                SetProperty(ref _PickerValue, value);
                IsLabelvisible = false;
                Opacity = 1;
                Sort();
            }
        }
        private async void Sort()
        {
            Dogs.Clear();
            var sellerDogs = await _DogRepository.GetAllAsync();
            var dogs = sellerDogs.Where(dog => dog.SellerId == Auth.Id);

            switch (PickerValue)
            {
                case "Sort by size: small to larger":
                    var smallDogs = new ObservableCollection<Dog>(dogs.OrderBy(d => d.Size));
                    foreach (var newdog in smallDogs)
                    {
                        Dogs.Add(newdog);
                    }
                    break;
                case "Sort by price: low to high":
                    var priceDogs = new ObservableCollection<Dog>(dogs.OrderBy(d => d.Price));
                    foreach (var newdog in priceDogs)
                    {
                        Dogs.Add(newdog);
                    }
                    break;
                case "Sort by size: larger to small":
                    var bigDogs = new ObservableCollection<Dog>(dogs.OrderBy(d => d.Size));
                    foreach (var newdog in bigDogs)
                    {
                        Dogs.Add(newdog);
                    }
                    break;
                case "Sort by price: high to low":
                    var priceDogsHigh = new ObservableCollection<Dog>(dogs.OrderByDescending(d => d.Price));
                    foreach (var newdog in priceDogsHigh)
                    {
                        Dogs.Add(newdog);
                    }
                    break;
                case "Sort by age: small to big":
                    var sortDogs = new ObservableCollection<Dog>(dogs.OrderBy(d => d.Age));
                    foreach (var newdog in sortDogs)
                    {
                        Dogs.Add(newdog);
                    }
                    break;
                case "Sort by age: big to small":
                    var ageDogs = new ObservableCollection<Dog>(dogs.OrderByDescending(d => d.Age));
                    foreach (var newdog in ageDogs)
                    {
                        Dogs.Add(newdog);
                    }
                    break;
                default:
                    break;
            }
        }

        private async void FilterBySearchTerm()
        {
            Dogs.Clear();
            var dogs = await _DogRepository.GetAllAsync();
            var sellerDogs = dogs.Where(dog => dog.SellerId == Auth.Id);
            var breeds = await _BreedRepository.GetAllAsync();

            if (breeds != null)
            {
                foreach (var breed in breeds)
                {
                    if (breed.BreedName.ToLower().Contains(SearchTerm.ToLower()))
                    {
                        var searchDogs = sellerDogs.Where(dog => dog.BreedId == breed.Id);

                        foreach (var newdog in searchDogs)
                        {
                            Dogs.Add(newdog);
                        }
                    }
                }
            }
        }

        public SellerDogsViewModel(IRepository dogRepository, IBreedRepository breedRepository)
        {
            _DogRepository = dogRepository;
            Dogs = new ObservableCollection<Dog>();
            IsLabelvisible = true;
            _BreedRepository = breedRepository;
        }

        public async void GetAllDogs()
        {
            try
            {
                Dogs.Clear();
                var dogs = await _DogRepository.GetAllAsync();
                var sellerDogs = dogs.Where(dog => dog.SellerId == Auth.Id);

                foreach (var dog in sellerDogs)
                {
                    Dogs.Add(dog);
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
            var parameter = new Dictionary<string, object>
                {
                  { "Auth", Auth}
                };
            Shell.Current.GoToAsync("//SellerDashPage", parameter);
        }

        [RelayCommand]
        private async void Logout()
        {
            bool logoutConfirmed = await Shell.Current.DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (logoutConfirmed)
            {
                await Shell.Current.GoToAsync("//OnboardingPage");
            }
        }

        [RelayCommand]
        private async void Profile()
        {
            var navigationParameter = new Dictionary<string, object>
             {
               { "Auth", Auth}
                   };

            await Shell.Current.GoToAsync("//ProfilePage", navigationParameter);
        }

        [RelayCommand]
        private void AddDog()
        {
            var parameter = new Dictionary<string, object>
                {
                  { "Auth", Auth}
                };

            Shell.Current.GoToAsync("//AddDogsPage", parameter);
        }

        [RelayCommand]
        private void YourDogs()
        {
            var navigationParameter = new Dictionary<string, object>
             {
               { "Auth", Auth}
                   };
            Shell.Current.GoToAsync("//SellerDogsPage", navigationParameter);
        }

        [RelayCommand]
        private void BuyerForm()
        {
            var parameter = new Dictionary<string, object> { { "Auth", Auth } };
            Shell.Current.GoToAsync("//BuyerFormPage", parameter);
        }
        [RelayCommand]
        private async void DisplayOptions(Dog dog)
        {
            if (dog == null)
                return;
            bool update = true;
            var parameter = new Dictionary<string, object>
            {
                { "Dog", dog },
                { "Auth", Auth },
                { "Update", update }
            };

            var response = await Shell.Current.DisplayActionSheet("Select option", "Ok", null, "Edit", "Delete");

            if (response == "Edit")
            {
                await Shell.Current.GoToAsync("//AddDogsPage", parameter);
            }
            if (response == "Delete")
            {
                try
                {
                    var res = await Shell.Current.DisplayAlert("PetMatch", "Are you sure you want to delete this data?", "Yes", "No");
                    if (res)
                    {
                        await _DogRepository.DeleteAsync(dog.Id);
                        CreateToast("Data deleted");
                    }
                }

                catch (Exception ex)
                {
                    CreateToast("Error occured");
                }
            }

        }

        [RelayCommand]
        private async void Refresh()
        {
            PickerValue = string.Empty;
            IsLabelvisible = true;
            Dogs.Clear();
            Opacity = 0.1F;
            RefreshAllDogs();
        }
        private async void RefreshAllDogs()
        {
            try
            {
                var dogs = await _DogRepository.GetAllAsync();
                var newdogs = dogs.Where(dog => dog.SellerId == Auth.Id);
                foreach (var dog in newdogs)
                {
                    Dogs.Add(dog);
                }
            }
            catch (Exception ex)
            {
                CreateToast("Error: " + ex);
            }
        }

    }
}
