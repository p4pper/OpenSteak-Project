// <copyright file="GameAPI.cs" company="openSteak">
// Copyright (c) openSteak. All rights reserved.
// </copyright>
namespace OpenSteakMines
{
    using System;
    using System.IO;

    /// <summary>The main file to handle the game's logic and trigger CoreGUI events.</summary>
    public class GameAPI : GameLogic
    {
        private const double DefaultBalance = 5.0;
        private double balance;
        private double currentBet = 0;

        private INterfaceGUI gUI;
        private GameState gameState = GameState.Off;

        private enum GameState
        {
            Off,
            On,
            Lost,
            Won,
        }

        /// <summary>Initializes the self.
        /// This must be called after a CoreGUI Based class is initialized with its components.</summary>
        /// <param name="gUI">The gUI.</param>
        public void InitializeSelf(INterfaceGUI gUI)
        {
            this.gUI = gUI;

            // Balance is stored as a text file. Check if it exists, or create a new one
            if (File.Exists("balance.txt"))
            {
                this.balance = double.Parse(File.ReadAllText("balance.txt"));
            }
            else
            {
                // Set balance to default
                File.WriteAllText("balance.txt", DefaultBalance.ToString());
                this.balance = DefaultBalance;
            }

            gUI.InitializeMinesAmountComboBox();
            gUI.InitializeGrid(false);
            gUI.UpdateBalance();
        }

        /// <summary>Gets the size of the mines grid in a single-dimension.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public int GetGridSize()
        {
            return GridSize;
        }

        /// <summary>Gets the current balance.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public double GetBalance()
        {
            this.balance = Math.Round(this.balance, 2);
            return this.balance;
        }

        /// <summary>Starts the game. Can also be used to finalize game</summary>
        public void StartGame()
        {
            if (this.gameState == GameState.Off)
            {
                // Check if bet amount is greater than zero
                if ((this.currentBet > 0 && this.currentBet <= this.balance) || this.currentBet == 0)
                {
                    this.gUI.SetComponentsToCashout();
                    this.RestartGame();
                }
                else
                {
                    // TODO Replace this with a GUI call instead.
                   // MessageBox.Show("Please place a valid bet.");
                }
            }
            else if (this.gameState == GameState.On && this.RevealedGems > 0)
            {
                this.gameState = GameState.Won;
                this.EndGame();
            }
        }

        /// <summary>Action if a Button clicked is a Gem.</summary>
        public void MineButtonClickedIsGem()
        {
            this.RevealedGems++;
            this.gUI.UpdateMultiplier();
            this.CheckForWin();
        }

        // So you are wondering how does this get triggered?
        // Well, it's triggered by the GUI button
        //
        // I hate this hooking..
        public void MineButtonClickedIsMine()
        {
            this.gUI.RevealMines();
            this.gameState = GameState.Lost;
            this.EndGame();
        }

        /// <summary>Restarts the game.</summary>
        public void RestartGame()
        {
            if ((double.TryParse(
                this.gUI.GetBetAmountFromTextField(),
                out this.currentBet)
                &&
                this.currentBet > 0)
                ||
                this.currentBet == 0)
            {
                if (this.currentBet <= this.balance)
                {
                    this.balance -= this.currentBet;
                    this.gUI.UpdateBalance();

                    this.RevealedGems = 0;
                    this.gameState = GameState.On;
                    this.MinesCount = this.gUI.GetSelectedMinesAmount();
                    this.Start();
                    this.gUI.InitializeGrid(true);
                }
                else
                {
                    //MessageBox.Show("Insufficient balance.");
                    this.gUI.RestartBetAmount();
                    this.currentBet = 0;
                }
            }
            else
            {
                //MessageBox.Show("Invalid bet amount.");
                this.gUI.RestartBetAmount();
                this.currentBet = 0;
            }
        }

        private void CheckForWin()
        {
            // Check if all gems have been revealed.
            bool isGameWon = this.RevealedGems == ((GridSize * GridSize) - this.MinesCount);
            if (isGameWon)
            {
                this.gameState = GameState.Won;
                this.EndGame();
            }

            if (this.RevealedGems == 1)
            {
                // Enable cashout button
                this.gUI.EnableCashout();
            }
        }

        private void EndGame()
        {
            this.gUI.RevealMines();

            if (this.gameState == GameState.Won)
            {
                this.balance += this.currentBet * this.getCashoutMultiplier();
                this.balance = Math.Round(this.balance, 2);
            }

            // GUI Events
            this.gUI.UpdateBalance();
            this.gUI.SetComponentsToStart();
            this.gameState = GameState.Off;
            File.WriteAllText("balance.txt", this.balance.ToString());
        }
    }
}
