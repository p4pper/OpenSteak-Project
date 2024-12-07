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
        void setComponentsToStart();
        void setComponentsToCashout();

        void restartBetAmount();
        string getBetAmount();

        int getSetMinesAmount();
    }
}
