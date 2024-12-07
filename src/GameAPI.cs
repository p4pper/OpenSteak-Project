// -------------------------------------------------------------------------------------------------
// Software License: Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International (CC BY-NC-ND 4.0)
// -------------------------------------------------------------------------------------------------
//
// You are free to:
//
// - Share: Copy and redistribute the material in any medium or format for non-commercial purposes
//   only. You must give appropriate credit to the original creator, provide a link to the license, and
//   indicate if changes were made. You may do so in any reasonable manner, but not in any way that
//   suggests the creator endorses you or your use.
//
// You may not:
// - Use the material for commercial purposes.
// - Create derivative works based on the material. No modifications or adaptations of this code are allowed.
//
// License details: https://creativecommons.org/licenses/by-nc-nd/4.0/
//
// This software is provided "as-is" without any warranties. The creator is not liable for any damage or loss caused by the use or misuse of the software.
//
/*
 *  This files is essential for the functions of the game, it also triggers GUI events via GameGUI_Bridge
 */
namespace OpenSteakWPF
{
    using System;
    using System.IO;
    using System.Windows;

    public class GameAPI : GameLogic //Based off core
    {
        private const double defaultBalance = 5.0;
        private double balance;
        private double currentBet = 0;

        private enum GameState { Off, On, Lost, Won }
        private GameState gameState = GameState.Off;

        private InterfaceGUI GUI;

        public void initializeSelf(InterfaceGUI GUI)
        {
            this.GUI = GUI;
            //
            // Balance is stored as a text file. Check if it exists, or create a new one
            //
            if (File.Exists("balance.txt"))
            {
                this.balance = Double.Parse(File.ReadAllText("balance.txt"));
            }
            else
            {
                // Set balance to default
                File.WriteAllText("balance.txt", defaultBalance.ToString());
            }

            GUI.initializeMinesAmountComboBox();
            GUI.initializeGrid(false);
            GUI.updateBalance();
        }

        public int getGridSize()
        {
            return gridSize;
        }

        public double getBalance()
        {
            return balance;
        }

        public void startButton()
        {
            if (gameState == GameState.Off)
            {
                // Check if bet amount is greater than zero
                if ((currentBet > 0 && currentBet <= balance) || currentBet == 0)
                {
                    GUI.setComponentsToCashout();
                    RestartGame();
                }
                else
                {
                    // TODO Replace this with a GUI call instead.
                    MessageBox.Show("Please place a valid bet.");
                }
            }
            else if (gameState == GameState.On && RevealedGems > 0)
            {
                gameState = GameState.Won;
                EndGame();
            }
        }

        // So you are wondering how does this get triggered?
        // Well, it's triggered by the GUI button
        //
        // I hate this hooking..
        public void mineButtonClickedIsMine()
        {
            GUI.revealMines();
            gameState = GameState.Lost;
            EndGame();
        }

        public void mineButtonClickedIsGem()
        {
            RevealedGems++;
            GUI.updateMultiplier();
            CheckForWin();
        }

        private void CheckForWin()
        {
            // Check if all gems have been revealed.
            bool isGameWon = RevealedGems == (gridSize * gridSize - MinesCount);
            if (isGameWon)
            {
                gameState = GameState.Won;
                EndGame();
            }

            if (RevealedGems == 1)
            {
                // Enable cashout button
                GUI.enableCashout();
            }
        }

        public void RestartGame()
        {
            if ((double.TryParse(GUI.GetBetAmountFromTextField(), out currentBet) && currentBet > 0) || currentBet == 0)
            {
                if (currentBet <= balance)
                {
                    balance -= currentBet;
                    GUI.updateBalance();

                    RevealedGems = 0;
                    gameState = GameState.On;
                    MinesCount = GUI.GetSelectedMinesAmount();
                    Start();
                    GUI.initializeGrid(true);
                }
                else
                {
                    MessageBox.Show("Insufficient balance.");
                    GUI.restartBetAmount();
                    currentBet = 0;
                }
            }
            else
            {
                MessageBox.Show("Invalid bet amount.");
                GUI.restartBetAmount();
                currentBet = 0;
            }
        }

        private void EndGame()
        {

            GUI.revealMines();

            if (gameState == GameState.Won)
            {
                balance += currentBet * getCashoutMultiplier();
                balance = Math.Round(balance, 2);
            }

            // GUI Events
            GUI.updateBalance();
            GUI.setComponentsToStart();
            gameState = GameState.Off;
            File.WriteAllText("balance.txt", balance.ToString());
        }
    }
}
