using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace smartchUWP.Converters
{
    class BooleanToIsVisibleBorderThikness : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is null) throw new ArgumentNullException();
            Boolean? isVisible = value as Boolean?;
            if (isVisible == true)
            {
                return new Thickness(2);
            }
            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is null) throw new ArgumentNullException();
            Thickness? borderThinkness = value as Thickness?;
            if (borderThinkness.Value.Top > 0)
            {
                return true;
            }

            return false;
        }
    }
}
