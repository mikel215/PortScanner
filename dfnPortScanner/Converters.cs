using System;
using System.Windows.Data;
using System.Globalization;

namespace dfnPortScanner
{
	public class StatusConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object param, CultureInfo culture)
        {
            string portStatus = ((string)value).ToLower();
            if(portStatus == "closed")
            {
                return "Red";
            }
            else
            {
                return "Green";
            }
        }

        public object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
	}
}
