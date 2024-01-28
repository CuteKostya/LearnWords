using System;
using System.Globalization;
using SQLite;
using Xamarin.Forms;

namespace TestApp1.Models
{
    public class FillingsClassChoiceButtonIMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == null)
                return null;

            return new ChoiceButton()
            {
                Choice = (Choice)values[0],
                Button = (string)values[1],
            };
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}