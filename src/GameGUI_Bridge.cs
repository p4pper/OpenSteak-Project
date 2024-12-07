namespace OpenSteakWPF
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class GameGUI_Bridge : INterfaceGUI
    {
        protected readonly int gridSize;
        protected readonly string[] layout;

        protected GameAPI api;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameGUI_Bridge"/> class.
        /// </summary>
        /// <param name="api">API Class, must be instantiated before.</param>
        public GameGUI_Bridge(GameAPI api)
        {
            // Hook GUI Events with API
            this.api = api;

            this.gridSize = api.GetGridSize();
            this.layout = api.GetLayout();
        }

        /// <summary>
        /// Restarts the Bet Text field to zero.
        /// </summary>
        public void RestartBetAmount()
        {
            this.RestartBetAmountGUI();
        }

        /// <summary>
        /// Retrieves the current bet amount from GUI Child Hook.
        /// </summary>
        /// <returns>The current bet amount as a string.</returns>
        public string GetBetAmountFromTextField()
        {
            return this.GetBetAmountGUI();
        }

        /// <summary>
        /// Retrieves the selected number of mines from the GUI Child Hook.
        /// </summary>
        /// <returns>The amount of selected mines.</returns>
        public int GetSelectedMinesAmount()
        {
            return this.GetSelectedMinesAmountGUI();
        }

        /// <summary>
        /// Initializes the combobox field by adding integers from 1 to 24 as items.
        /// This is optional call and needs GUI Child Hook.
        /// </summary>
        public void InitializeMinesAmountComboBox()
        {
            this.InitializeMinesAmountComboBoxGUI();
        }

        /// <summary>
        /// Initializes the grid by adding the mines with appropriate actions.
        /// This is optional call and needs GUI Child Hook.
        /// </summary>
        /// <param name="enableInteraction">Enable buttons to be clicked (revealed).</param>
        public void InitializeGrid(bool enableInteraction)
        {
            this.InitializeGridGUI(enableInteraction);
        }

        /// <summary>
        /// Reveal all mines through GUI Child hook.
        /// </summary>
        public void RevealMines()
        {
            this.RevealAllMinesGUI();
        }

        /// <summary>
        /// Updates the multiplier text label through GUI Child hook.
        /// </summary>
        public void UpdateMultiplier()
        {
            this.UpdateMultiplierGUI();
        }

        /// <summary>
        /// Updates the balance text label through GUI Child hook.
        /// </summary>
        public void UpdateBalance()
        {
            this.UpdateBalanceGUI();
        }

        /// <summary>
        /// Enable the cashout button through GUI Child hook.
        /// </summary>
        public void EnableCashout()
        {
            this.EnableCashoutGUI();
        }

        /// <summary>Changes the GUI components to their start state. This is when the game is restarted/stopped.</summary>
        public void SetComponentsToStart()
        {
            this.SetComponentsToStartGUI();
        }

        /// <summary>Changes the GUI components to their cashout state.</summary>
        public void SetComponentsToCashout()
        {
            this.SetComponentsToCashoutGUI();
        }

        /// <summary>Restarts the bet amount GUI.</summary>
        protected virtual void RestartBetAmountGUI()
        {
        }

        /// <summary>Restarts the payout multiplier GUI.</summary>
        protected virtual void RestartPayoutMultiplierGUI()
        {
        }

        /// <summary>Initializes the mines amount ComboBox GUI.</summary>
        protected virtual void InitializeMinesAmountComboBoxGUI()
        {
        }

        /// <summary>Sets the components to start GUI.</summary>
        protected virtual void SetComponentsToStartGUI()
        {
        }

        /// <summary>
        /// Change all GUI Components to allow Cashout/End the game
        /// </summary>
        protected virtual void SetComponentsToCashoutGUI()
        {
        }

        /// <summary>Gets the bet amount GUI.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        protected virtual string GetBetAmountGUI()
        {
            return "No-Bet-Amount, Child-Did-Not-Pass-Value-GUI-Event";
        }

        /// <summary>Gets the selected mines amount GUI.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        protected virtual int GetSelectedMinesAmountGUI()
        {
            return 0;
        }

        /// <summary>Enables the cashout GUI.</summary>
        protected virtual void EnableCashoutGUI()
        {
        }

        /// <summary>Reveals all mines GUI.</summary>
        protected virtual void RevealAllMinesGUI()
        {
        }

        /// <summary>Updates the multiplier GUI.</summary>
        protected virtual void UpdateMultiplierGUI()
        {
        }

        /// <summary>Updates the balance GUI.</summary>
        protected virtual void UpdateBalanceGUI()
        {
        }

        /// <summary>Initializes the grid GUI.</summary>
        /// <param name="enableMinesInteraction">if set to <c>true</c> [enable mines interaction].</param>
        protected virtual void InitializeGridGUI(bool enableMinesInteraction)
        {
        }
    }
}