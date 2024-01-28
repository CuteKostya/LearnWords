using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TestApp1.Models;
using Xamarin.Forms;
using Color = System.Drawing.Color;

namespace TestApp1.Convert
{
    public class ColorChangeIMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == null || values[2] == null)
                return Color.DodgerBlue;

            if ((string)values[1] != (string)values[2])
                return Color.DodgerBlue;
            else
            {
                Choice choiceButton = (Choice)values[0];
                if (choiceButton.PressedButton == choiceButton.Translation)
                {
                    return Color.DarkGreen;
                }
                else
                {
                    return Color.DarkRed;
                }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}