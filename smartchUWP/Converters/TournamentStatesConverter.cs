using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace smartchUWP.Converters
{
    class TournamentStatesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (TournamentState)value;
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            KeyValuePair<TournamentState, string> keyValuePair = (KeyValuePair<TournamentState, string>) value;
            return keyValuePair.Key;
            throw new NotImplementedException();
        }
    }
}
