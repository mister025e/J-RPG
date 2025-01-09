using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J_RPG.Characters.Common;
using J_RPG.Characters.Warrior;

namespace J_RPG.Game
{
    public class BattleSystem
    {
        public void StartBattle(List<Character> team1, List<Character> team2)
        {
            while (team1.Exists(c => c.IsAlive) && team2.Exists(c => c.IsAlive))
            {
                PlayerTurn(team1, team2);
                if (team2.Exists(c => c.IsAlive)) PlayerTurn(team2, team1);
            }

            Console.WriteLine(team1.Exists(c => c.IsAlive) ? "Team 1 wins!" : "Team 2 wins!");
        }

        private Character SelectCharacter(List<Character> team)
        {
            List<Character> aliveCharacters = team.Where(c => c.IsAlive).ToList();

            if (aliveCharacters.Count == 0)
            {
                Console.WriteLine("No available targets.");
                return null;
            }

            Console.WriteLine("Select a character:");
            for (int i = 0; i < aliveCharacters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {aliveCharacters[i].Name} (HP: {aliveCharacters[i].HP})");
            }

            int choice = GetValidInput(1, aliveCharacters.Count);
            return aliveCharacters[choice - 1];
        }

        private void PlayerTurn(List<Character> playerTeam, List<Character> enemyTeam)
        {
            Character activeCharacter = SelectCharacter(playerTeam);
            Skill chosenSkill = SelectSkill(activeCharacter);

            if (chosenSkill != null)
            {
                // Check if the skill has a method that accepts multiple targets
                if (chosenSkill.GetType().GetMethod("Use", new[] { typeof(Character), typeof(List<Character>) }) != null)
                {
                    // For multi-target skills
                    chosenSkill.Use(activeCharacter, enemyTeam);
                }
                else
                {
                    // For single-target skills
                    Console.WriteLine("Select a target:");
                    Character target = SelectTarget(enemyTeam);
                    if (target != null)
                    {
                        chosenSkill.Use(activeCharacter, target);
                    }
                    else
                    {
                        Console.WriteLine("No valid target selected. Turn skipped.");
                    }
                }
            }
        }

        private Character SelectTarget(List<Character> enemies)
        {
            List<Character> aliveEnemies = enemies.Where(c => c.IsAlive).ToList();

            if (aliveEnemies.Count == 0)
            {
                Console.WriteLine("No available targets.");
                return null;
            }

            Console.WriteLine("Select a target:");
            for (int i = 0; i < aliveEnemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {aliveEnemies[i].Name} (HP: {aliveEnemies[i].HP})");
            }

            int choice = GetValidInput(1, aliveEnemies.Count);
            return aliveEnemies[choice - 1];
        }

        private Skill SelectSkill(Character character)
        {
            Console.WriteLine($"Select a skill for {character.Name}:");
            for (int i = 0; i < character.Skills.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {character.Skills[i].Name} (Cooldown: {character.Skills[i].CurrentCooldown})");
            }

            int choice = GetValidInput(1, character.Skills.Count);
            return character.Skills[choice - 1];
        }

        private int GetValidInput(int min, int max)
        {
            int choice;
            while (true)
            {
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice >= min && choice <= max)
                {
                    break;
                }
                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
            }
            return choice;
        }
    }
}
