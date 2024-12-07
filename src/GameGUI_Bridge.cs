namespace OpenSteakWPF
{
    public class GameGUI_Bridge : InterfaceGUI
    {
        protected int gridSize;
        protected string[] layout;

        protected GameAPI api;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameGUI_Bridge"/> class.
        /// </summary>
        /// <param name="api">API Class, must be instantiated before.</param>
        public GameGUI_Bridge(GameAPI api)
        {
            // Hook GUI Events with API
            this.api = api;

            this.gridSize = api.getGridSize();
            this.layout = api.GetLayout();
        }

        /// <summary>
        /// Restarts the Bet Text field to zero.
        /// </summary>
        public void restartBetAmount()
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
        public void initializeMinesAmountComboBox()
        {
            this.InitializeMinesAmountComboBoxGUI();
        }

        /// <summary>
        /// Initializes the grid by adding the mines with appropriate actions.
        /// This is optional call and needs GUI Child Hook.
        /// </summary>
        /// <param name="enableInteraction">Enable buttons to be clicked (revealed).</param>
        public void initializeGrid(bool enableInteraction)
        {
            this.InitializeGridGUI(enableInteraction);
        }

        /// <summary>
        /// Reveal all mines through GUI Child hook.
        /// </summary>
        public void revealMines()
        {
            this.RevealAllMinesGUI();
        }

        /// <summary>
        /// Updates the multiplier text label through GUI Child hook.
        /// </summary>
        public void updateMultiplier()
        {
            this.UpdateMultiplierGUI();
        }

        /// <summary>
        /// Updates the balance text label through GUI Child hook.
        /// </summary>
        public void updateBalance()
        {
            this.UpdateBalanceGUI();
        }

        /// <summary>
        /// Enable the cashout button through GUI Child hook.
        /// </summary>
        public void enableCashout()
        {
            this.EnableCashoutGUI();
        }

        public void setComponentsToStart()
        {
            this.SetComponentsToStartGUI();
        }

        public void setComponentsToCashout()
        {
            this.SetComponentsToCashoutGUI();
        }

        protected virtual void RestartBetAmountGUI()
        {
        }

        protected virtual void InitializeMinesAmountComboBoxGUI()
        {
        }

        protected virtual void SetComponentsToStartGUI()
        {
        }

        /// <summary>
        /// Change all GUI Components to allow Cashout/End the game
        /// </summary>
        protected virtual void SetComponentsToCashoutGUI()
        {
        }

        protected virtual string GetBetAmountGUI()
        {
            return "No-Bet-Amount, Child-Did-Not-Pass-Value-GUI-Event";
        }

        protected virtual int GetSelectedMinesAmountGUI()
        {
            return 0;
        }

        protected virtual void EnableCashoutGUI()
        { }

        protected virtual void RevealAllMinesGUI()
        { }

        protected virtual void UpdateMultiplierGUI()
        { }

        protected virtual void UpdateBalanceGUI()
        { }

        protected virtual void InitializeGridGUI(bool enableMinesInteraction)
        { }
    }
}
