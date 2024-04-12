using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogHome.Interface;
using DogHome.Model;
using System.Collections.ObjectModel;

namespace DogHome.ViewModel
{
    public partial class MainDogsViewModel : ObservableObject
    {
        public ObservableCollection<Dog> Dogs { get; set; }
        IRepository _DogRepository;
        IBreedRepository _BreedRepository;

        [ObservableProperty]
        bool _IsLoading;

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
            IsLoading = true;
            var dogs = await _DogRepository.GetAllAsync();

            switch (PickerValue)
            {
                case "Sort by size: small to larger":
                    break;
                case "Sort by price: low to high":
                    var priceDogs = dogs.OrderBy(d => d.Price);
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
                    var priceDogsHigh = dogs.OrderByDescending(d => d.Price);
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
            IsLoading = false;
        }

        private async void FilterBySearchTerm()
        {
            Dogs.Clear();
            var dogs = await _DogRepository.GetAllAsync();
            var breeds = await _BreedRepository.GetAllAsync();

            if (breeds != null)
            {
                foreach (var breed in breeds)
                {
                    if (breed.BreedName.ToLower().Contains(SearchTerm.ToLower()))
                    {
                        var searchDogs = dogs.Where(dog => dog.BreedId == breed.Id);

                        foreach (var newdog in searchDogs)
                        {
                            Dogs.Add(newdog);
                        }
                    }
                }
            }
        }
        public MainDogsViewModel(IRepository dogRepository, IBreedRepository breedRepository)
        {
            _DogRepository = dogRepository;
            _BreedRepository = breedRepository;
            Dogs = new ObservableCollection<Dog>();
            IsLabelvisible = true;
        }

        public async void GetAllDogs()
        {
            try
            {
                ;
                Dogs.Clear();
                var dogs = await _DogRepository.GetAllAsync();
                foreach (var dog in dogs)
                {
                    Dogs.Add(dog);
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                CreateToast("Error: " + ex);
                IsLoading = false;
            }
        }

        private async void RefreshAllDogs()
        {
            try
            {
                var dogs = await _DogRepository.GetAllAsync();
                foreach (var dog in dogs)
                {
                    Dogs.Add(dog);
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                CreateToast("Error: " + ex);
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void Back()
        {
            Shell.Current.GoToAsync("//OnboardingPage");
        }

        [RelayCommand]
        private void DisplayDetail(Dog dog)
        {
            var dogparameter = new Dictionary<string, object> { { "DogDetail", dog } };
            Shell.Current.GoToAsync("//DogDetailPage", dogparameter);
        }


        [RelayCommand]
        private async void Refresh()
        {
            PickerValue = string.Empty;
            IsLabelvisible = true;
            Dogs.Clear();
            IsLoading = true;
            Opacity = 0.1F;
            RefreshAllDogs();
        }

        [RelayCommand]
        private void SellerList()
        {
            Shell.Current.GoToAsync("//SellersListPage");
        }
        async void CreateToast(string text)
        {
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show();
        }
    }
}
