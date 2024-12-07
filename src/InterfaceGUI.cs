namespace OpenSteakMines
{

    /// <summary>
    ///   <br />
    /// </summary>
    public interface INterfaceGUI
    {
        /// <summary>Initializes the mines amount ComboBox.</summary>
        void InitializeMinesAmountComboBox();

        /// <summary>Initializes the mines amount ComboBox.</summary>
        /// <param name="enableInteraction">Enable buttons to be clicked (revealed).</param>
        void InitializeGrid(bool enableInteraction);

        /// <summary>Initializes the mines amount ComboBox.</summary>
        void RevealMines();

        /// <summary>Initializes the mines amount ComboBox.</summary>
        void UpdateMultiplier();

        /// <summary>Initializes the mines amount ComboBox.</summary>
        void UpdateBalance();

        /// <summary>Initializes the mines amount ComboBox.</summary>
        void EnableCashout();

        /// <summary>
        /// Changes the GUI components to their start state. This is when the game is restarted/stopped
        /// </summary>
        void SetComponentsToStart();

        /// <summary>
        /// Changes the GUI components to their cashout state. Note that this happens when game is started
        /// </summary>
        void SetComponentsToCashout();

        /// <summary>Initializes the mines amount ComboBox.</summary>
        void RestartBetAmount();

        /// <summary>Initializes the mines amount ComboBox.</summary>
        /// <returns>A string value.</returns>
        string GetBetAmountFromTextField();

        /// <summary>Initializes the mines amount ComboBox.</summary>
        /// <returns>an integer value.</returns>
        int GetSelectedMinesAmount();
    }
}
