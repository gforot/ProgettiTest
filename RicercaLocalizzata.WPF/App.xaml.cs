using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RicercaLocalizzata.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CultureInfo ci = new CultureInfo("it-IT");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }


        private static readonly Regex _regexInt = new Regex("[^0-9-]+");

        private bool IsTextAllowedInt(string text)
        {
            return !_regexInt.IsMatch(text);
        }

        private void TextBox_PreviewTextInputInt(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!IsTextAllowedInt(e.Text))
            {
                e.Handled = true;
            }
        }


        private static readonly Regex _regexDouble = new Regex("/^[0-9]+(\\.[0-9]+)?$");

        private bool IsTextAllowedDouble(string text)
        {
            return !_regexDouble.IsMatch(text);
        }

        private void TextBox_PreviewTextInputDouble(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string currentText = (e.Source as TextBox).Text;
            string newText = string.Concat(currentText, e.Text);

            if ((!IsTextAllowedDouble(newText)) || (!double.TryParse(newText, out double f)))
            {
                e.Handled = true;
            }
        }

    }
}
