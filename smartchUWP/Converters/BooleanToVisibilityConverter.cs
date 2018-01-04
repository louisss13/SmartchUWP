using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace smartchUWP.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is null) throw new ArgumentNullException();
            Boolean? isVisible = value as Boolean?;
            if (isVisible == true)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is null) throw new ArgumentNullException();
            Visibility? visibility = value as Visibility?;
            if (visibility == Visibility.Visible)
            {
                return true;
            }

            return false;
            
        }
    }
}
