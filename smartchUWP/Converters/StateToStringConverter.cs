using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace smartchUWP.Converters
{
    class StateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is null) throw new ArgumentNullException();
            TournamentState? state = value as TournamentState?;
            switch (state)
            {
                case TournamentState.EnCours:
                    return  "En cours";
                case TournamentState.EnPreparation:
                    return "En Préparation";
                case TournamentState.Fini:
                    return "Fini";
                default:
                    return "Valeur non traduite";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
