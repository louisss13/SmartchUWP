using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace smartchUWP.Converters
{
    class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime datetime;
            if (value is DateTime)
                datetime = (DateTime)value;
            else
                throw new InvalidCastException("LE parametre accepté est un datetime ");
            DateTime dateTimenew = DateTime.SpecifyKind(datetime,DateTimeKind.Utc);
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTimenew);

            return dateTimeOffset;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset datetimeoffset;
            if (value is DateTimeOffset)
                datetimeoffset = (DateTimeOffset)value;
            else
                throw new InvalidCastException("LE parametre accepté est un datetime ");
            

            return datetimeoffset.DateTime;
        }
    }
}
