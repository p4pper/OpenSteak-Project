using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace OpenSteakWPF
{
    public partial class MainWindow : Window
    {
        private GameGUI_WPF gui;
        private GameAPI api;

        public MainWindow()
        {
            InitializeComponent();
            api = new GameAPI();
            gui = new GameGUI_WPF(
                api,
                api.getGridSize(),
                api.GetLayout(),
                MinesGrid,
                this,
                cashOutORStartBtn,
                mineCombo,
                playerBalText,
                payoutMultiplierLbl,
                betAmountTxt

                );
            api.initializeSelf(gui);
            betAmountTxt.TextChanged += betAmountTxt_TextChanged;
        }

        private void t1_Click(object sender, RoutedEventArgs e)
        {
            api.startButton();
        }

        private void betAmountTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            textBox.TextChanged -= betAmountTxt_TextChanged;

            string text = textBox.Text;
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal number))
            {
                textBox.Text = number.ToString("0.00", CultureInfo.InvariantCulture);
                textBox.SelectionStart = textBox.Text.Length;
            }
            else
            {
                textBox.Text = "0.00";
            }

            textBox.TextChanged += betAmountTxt_TextChanged;

            if (double.TryParse(textBox.Text, out double bet) && bet > api.getBalance())
            {
                textBox.Text = "0.00";
            }
        }
    }
}
