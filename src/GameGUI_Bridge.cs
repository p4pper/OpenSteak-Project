namespace OpenSteakWPF
{
    public class GameGUI_Bridge : InterfaceGUI
    {
        protected int gridSize;
        protected string[] layout;

        protected GameAPI api;

        public GameGUI_Bridge(GameAPI api)
        {
            // Hook GUI Events with API
            this.api = api;

            this.gridSize = api.getGridSize();
            this.layout = api.GetLayout();
        }

        // Give Public Triggers
        public void restartBetAmount()
        {
            restartBetAmountGUI();
        }

        public string getBetAmount()
        {
            return getBetAmountGUI();
        }

        public int getSetMinesAmount()
        {
            return getMinesAmountGUI();
        }

        public void initializeMinesAmountComboBox()
        {
            InitializeMinesAmountComboBoxGUI();
        }

        public void initializeGrid(bool enableInteraction)
        {
            InitializeGridGUI(enableInteraction);
        }
       
        public void revealMines()
        {
            RevealAllMinesGUI();
        }

        public void updateMultiplier()
        {
            UpdateMultiplierGUI();
        }

        public void updateBalance()
        {
            UpdateBalanceGUI();
        }

        public void enableCashout()
        {
            EnableCashoutGUI();
        }

        // --- GUI ---

        public void setComponentsToStart()
        {
            SetComponentsToStartGUI();
        }

        public void setComponentsToCashout()
        {
            SetComponentsToCashoutGUI();
        }

        protected virtual void restartBetAmountGUI()
        {}

        protected virtual void InitializeMinesAmountComboBoxGUI()
        {}

        protected virtual void SetComponentsToStartGUI()
        {}

        protected virtual void SetComponentsToCashoutGUI()
        {}

        protected virtual string getBetAmountGUI()
        {
            return "No-Bet-Amount, Child-Did-Not-Pass-Value-GUI-Event";
        }

        protected virtual int getMinesAmountGUI()
        {
            return 0;
        }

        protected virtual void EnableCashoutGUI()
        {}

        protected virtual void RevealAllMinesGUI()
        {}

        protected virtual void UpdateMultiplierGUI()
        {}

        protected virtual void UpdateBalanceGUI()
        {}

        protected virtual void InitializeGridGUI(bool enableMinesInteraction)
        {}

    }
}
