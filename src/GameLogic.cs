using System;
using System.Threading.Tasks;

namespace OpenSteakWPF
{
    public class GameLogic
    {
        // Configuration
        public int RevealedGems { get; set; }
        public int MinesCount { get; set; } // Number of mines to place
        private const double houseEdge = 0.1; // Example house edge (10%)
        protected const int gridSize = 5; // Fixed grid size

        // Game
        private string[] layout;

        public GameLogic()
        {
            layout = new string[gridSize * gridSize]; // Initialize array for 5x5 grid
            InitializeLayout(); // Initialize layout with 'g'
        }

        public void Start()
        {
            Task.Run(() => StartGame()); // Run game logic on background thread
        }

        public string[] GetLayout()
        {
            return layout;
        }

        private void StartGame()
        {
            PlaceMines(); // Place mines based on the house edge
        }

        private void PlaceMines()
        {
            Random random = new Random();
            InitializeLayout(); // Ensure layout is reset

            int placedMines = 0;
            while (placedMines < MinesCount)
            {
                int currentIndex = GetBiasedRandomIndex(random);
                if (layout[currentIndex] == "g")
                {
                    layout[currentIndex] = "m";
                    placedMines++;
                }
            }
        }

        public double getCashoutMultiplier()
        {
            double Payout = 1;
            for (int i = 0; i < MinesCount; i++)
            {
                Payout = Payout * ((gridSize * gridSize) - RevealedGems - i) / ((gridSize * gridSize) - i);
            }
            return Math.Round(0.99 / Payout, 2);
        }

        private void InitializeLayout()
        {
            for (int i = 0; i < layout.Length; i++)
            {
                layout[i] = "g"; // Initialize all cells as 'g'

                // g,g,g,g,g,g,g,,g
            }
        }

        private int GetBiasedRandomIndex(Random random)
        {
            // Get a random index
            int index = random.Next(layout.Length);

            // Introduce bias based on the house edge
            if (random.NextDouble() < houseEdge)
            {
                // Apply bias to select an index in a specific region more frequently
                int offset = random.Next(5); // Small offset for bias
                index = (index + offset) % layout.Length;
            }

            return index;
        }
    }
}
