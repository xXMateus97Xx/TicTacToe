using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TicTacToe.Converters
{
    public class XValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is char c)
                return c == 'X';
            
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return 'X';
            
            return 'O';
        }
    }
}