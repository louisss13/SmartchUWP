using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace smartchUWP.Converters
{
    public class DateTimeToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime birthday = (DateTime)value;
            DateTime reference;
            if (parameter == null)
                reference = DateTime.Now;
            else
                reference = (DateTime)parameter;
            int age = reference.Year - birthday.Year;
            if (reference < birthday.AddYears(age)) age--;
            return age;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
