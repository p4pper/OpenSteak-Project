// <copyright file="GameLogic.cs" company="openSteak">
// Copyright (c) openSteak. All rights reserved.
// </copyright>

namespace OpenSteakMines
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameLogic"/> class.
    /// </summary>
    public class GameLogic
    {
        /// <summary>The grid size</summary>
        protected const int GridSize = 5; // Fixed grid size

        /// <summary>The house edge</summary>
        private const double HouseEdge = 0.1; // Example house edge (10%)

        private readonly string[] layout;
        /// <summary>Gets or sets the revealed gems.</summary>
        /// <value>The revealed gems.</value>
        public int RevealedGems { get; set; }

        /// <summary>Gets or sets the mines count.</summary>
        /// <value>The mines count.</value>
        public int MinesCount { get; set; } // Number of mines to place


        public GameLogic()
        {
            layout = new string[GridSize * GridSize]; // Initialize array for 5x5 grid
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
            double payout = 1;
            for (int i = 0; i < this.MinesCount; i++)
            {
                payout = payout * ((GridSize * GridSize) - RevealedGems - i) / ((GridSize * GridSize) - i);
            }
            return Math.Round(0.99 / payout, 2);
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
            if (random.NextDouble() < HouseEdge)
            {
                // Apply bias to select an index in a specific region more frequently
                int offset = random.Next(5); // Small offset for bias
                index = (index + offset) % layout.Length;
            }

            return index;
        }

       
    }
}
