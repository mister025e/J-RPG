using System;
using System.Collections.Generic;
using J_RPG.Characters;
using J_RPG.Characters.Common;
using J_RPG.Characters.Warrior;

namespace J_RPG.Game
{
    public class GameEngine
    {
        public void StartGame()
        {
            Console.WriteLine("Welcome to the RPG Game!");

            // Player 1 team setup
            Console.WriteLine("Player 1, set up your team:");
            List<Character> player1Team = CreatePlayerTeam();

            // Player 2 team setup
            Console.WriteLine("Player 2, set up your team:");
            List<Character> player2Team = CreatePlayerTeam();

            // Display selected characters for both teams
            Console.WriteLine("\nPlayer 1's characters:");
            DisplayTeam(player1Team);

            Console.WriteLine("\nPlayer 2's characters:");
            DisplayTeam(player2Team);

            // Create an instance of BattleSystem
            BattleSystem battleSystem = new BattleSystem();

            // Start the battle with both teams
            battleSystem.StartBattle(player1Team, player2Team);
        }

        private List<Character> CreatePlayerTeam()
        {
            List<Character> playerTeam = new List<Character>();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Select character {i + 1}:");
                Console.WriteLine("1. Warrior");
                Console.WriteLine("2. Mage");
                Console.WriteLine("3. Paladin");
                Console.WriteLine("4. Priest");
                Console.WriteLine("5. Thief");
                int choice = int.Parse(Console.ReadLine());

                Character character = choice switch
                {
                    1 => new Warrior($"Warrior {i + 1}"),
                    // Add cases for other character types when implemented
                    _ => throw new InvalidOperationException("Invalid choice")
                };

                Console.WriteLine($"Enter name for {character.GetType().Name}:");
                character.Name = Console.ReadLine();

                playerTeam.Add(character);
            }
            return playerTeam;
        }

        private void DisplayTeam(List<Character> team)
        {
            foreach (var character in team)
            {
                Console.WriteLine(character);
            }
        }
    }
}
