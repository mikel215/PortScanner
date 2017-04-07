using System;
using System.Windows.Data;
using System.Globalization;

namespace dfnPortScanner
{
	public class StatusConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object param, CultureInfo culture)
        {
            bool portStatus = ((bool)value); ;
            if(portStatus == false)
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

    public class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object param, CultureInfo culture)
        {
            bool portStatus = ((bool)value);
            if (portStatus == true)
            {
                return "Open";
            }
            else
            {
                return "Closed";
            }
        }

        public object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
