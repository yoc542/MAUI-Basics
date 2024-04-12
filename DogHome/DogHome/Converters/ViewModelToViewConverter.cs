using DogHome.ViewModel;
using DogHome.Views;
using System.Globalization;

namespace DogHome.Converters
{
    public class ViewModelToViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PersonalDetailViewModel)
            {
                return new PersonalDetailPage();
            }
            else if (value is SocialProfilesViewModel)
            {
                return new SocialProfilesPage();
            }
            else if (value is RegisterFormViewModel)
            {
                return new CreateAccountPage();
            }
            else
            {
                return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
