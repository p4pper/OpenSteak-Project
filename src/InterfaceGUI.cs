namespace OpenSteakWPF
{
    /*
     * This file is neccesarry to provide an interface between the GUI_Bridge and GameAPI
     * These functions are called by GameAPI to trigger GUI changes accordingly.
     */
    public interface InterfaceGUI
    {
        void initializeMinesAmountComboBox();
        void initializeGrid(bool enableInteraction);
        void revealMines();
        void updateMultiplier();
        void updateBalance();
        void enableCashout();

        /// <summary>
        /// Changes the GUI components to their start state. This is when the game is restarted/stopped
        /// </summary>
        void setComponentsToStart();

        /// <summary>
        /// Changes the GUI components to their cashout state. Note that this happens when game is started
        /// </summary>
        void setComponentsToCashout();

        void restartBetAmount();
        string GetBetAmountFromTextField();

        int GetSelectedMinesAmount();
    }
}
