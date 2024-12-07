using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace OpenSteakWPF
{
    // This class connects GameAPI with the WPF Module using the GameGUI_Bridge.
    // Of course, you can use MinesAPI and GameGUI_Bridge to connect it with other GUI frameworks
    // Start by creating a new file; responsbile for executing GUI events based off this file.
    public class GameGUI_WPF : GameGUI_Bridge
    {
        private Grid MinesGrid;
        private MainWindow MinesWindow;
        private Button cashOutORStartButton;
        private ComboBox mineCombo;
        private Label balanceLabel;
        private Label payoutMultiplierLabel;
        private TextBox betAmountText;

        public GameGUI_WPF(
            GameAPI api,
            int gridSize, 
            string[] layout, 
            Grid minesGrid, 
            MainWindow minesWindow, 
            Button cashOutORStartButton,
            ComboBox mineCombo,
            Label balanceLabel,
            Label payoutMultiplierLabel,
            TextBox betAmountText
        ) 
        : base(api)
        {
            this.MinesGrid = minesGrid;
            this.MinesWindow = minesWindow;
            this.cashOutORStartButton = cashOutORStartButton;
            this.mineCombo = mineCombo;
            this.balanceLabel = balanceLabel;
            this.payoutMultiplierLabel = payoutMultiplierLabel;
            this.betAmountText = betAmountText;
        }

        protected override void RevealAllMinesGUI()
        {
            foreach (var child in MinesGrid.Children)
            {
                if (child is Button btn)
                {
                    btn.IsEnabled = false;
                    ((Image)btn.Content).Visibility = Visibility.Visible;
                }
            }
        }

        protected override void InitializeGridGUI(bool enableMinesInteraction)
        {
            MinesGrid.RowDefinitions.Clear();
            MinesGrid.ColumnDefinitions.Clear();
            MinesGrid.Children.Clear();

            for (int i = 0; i < gridSize; i++)
            {
                MinesGrid.RowDefinitions.Add(new RowDefinition());
                MinesGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button button = CreateMineButtonGUI(enableMinesInteraction, row, col);
                    MinesGrid.Children.Add(button);
                }
            }
        }

        protected override void InitializeMinesAmountComboBoxGUI()
        {
            for (int i = 1; i <= 24; i++)
            {
                mineCombo.Items.Add(i);
            }
        }

        private Button CreateMineButtonGUI(bool enableMines, int row, int col)
        {
            int index = row * gridSize + col;
            Button button = new Button
            {
                Margin = new Thickness(2),
                Style = (Style)MinesWindow.Resources["MineButton"],
                IsEnabled = enableMines
            };

            if (enableMines)
            {
                string iconType = layout[index];
                Image iconImage = CreateIconImageGUI(iconType);
                button.Content = iconImage;
                button.Tag = iconType;
            }

            button.Click += MineButton_Click_Event;
            Grid.SetRow(button, row);
            Grid.SetColumn(button, col);

            return button;
        }

        private Image CreateIconImageGUI(string iconType)
        {
            string imagePath = iconType == "m"
                ? "pack://application:,,,/Resources/mine.png"
                : "pack://application:,,,/Resources/gem.png";

            Image iconImage = new Image
            {
                Source = new BitmapImage(new Uri(imagePath)),
                Width = 20,
                Height = 20,
                Visibility = Visibility.Collapsed,
                RenderTransform = new ScaleTransform(3, 3),
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            return iconImage;
        }

        private void MineButton_Click_Event(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            if ((string)clickedButton.Tag == "m")
            {
                Task.Delay(new Random().Next(2000, 4000));
                api.mineButtonClickedIsMine();
            }
            else
            {
                api.mineButtonClickedIsGem();;
            }

            clickedButton.Style = (Style)MinesWindow.Resources["MineButtonRevealed"];
            Image iconImage = clickedButton.Content as Image;
            iconImage.Visibility = Visibility.Visible;
            clickedButton.IsEnabled = false;
        }

        protected override void SetComponentsToStartGUI()
        {
            cashOutORStartButton.Content = "Start";
            cashOutORStartButton.IsEnabled = true;
            mineCombo.IsEnabled = true;
        }

        protected override void SetComponentsToCashoutGUI()
        {
            cashOutORStartButton.Content = "Cashout";
            cashOutORStartButton.IsEnabled = false;
            mineCombo.IsEnabled = false;
            
        }

        protected override void UpdateBalanceGUI()
        {
            balanceLabel.Content = "Balance: " + api.getBalance().ToString("0.0000");
        }

        protected override void UpdateMultiplierGUI()
        {
            payoutMultiplierLabel.Content = api.getCashoutMultiplier().ToString("0.00") + "x";
        }

        protected override void EnableCashoutGUI()
        {
            cashOutORStartButton.IsEnabled = true;
        }

        protected override string getBetAmountGUI()
        {
            return betAmountText.Text;
        }

        protected override int getMinesAmountGUI()
        {
            return Int32.Parse(mineCombo.SelectedValue.ToString());
        }

        protected override void restartBetAmountGUI()
        {
            betAmountText.Text = "0.00";
        }
    }
}
