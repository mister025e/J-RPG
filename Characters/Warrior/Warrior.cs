using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J_RPG.Characters.Common;
using J_RPG.Characters.Warrior;

namespace J_RPG.Characters.Warrior
{
    public class Warrior : Character
    {
        public HeroicStrike HeroicStrike { get; private set; }
        public BattleCry BattleCry { get; private set; }
        public Twirl Twirl { get; private set; }

        public Warrior(string name)
            : base(name, 100, 50, 0, ArmorTypes.Plates, 5, 25, 10, 50)
        {
            Skills.Add(new HeroicStrike());
            Skills.Add(new Twirl());
            Skills.Add(new BattleCry());
        }

        public override void Hit(int damage, Character attacker, AttackTypes attackType)
        {
            base.Hit(damage, attacker, attackType); // Call the base class implementation to handle damage

            // Counterattack logic
            if (attackType == AttackTypes.AtkPhy) // Counterattack only triggers for physical attacks
            {
                Random rng = new Random();
                int counterChance = rng.Next(1, 101); // Generate a random number between 1 and 100

                bool parried = ParriedAttack(attacker); // Determine if the attack was parried

                if (counterChance <= 25 || parried) // 25% chance or guaranteed if parried
                {
                    int counterDamage = parried ? (int)(damage * 1.5) : (int)(damage * 0.5);
                    Console.WriteLine($"{Name} counterattacks {attacker.Name} for {counterDamage} damage!");

                    // Apply counter damage to the attacker
                    // Assuming the counterattack is a physical attack; adjust AttackTypes as needed
                    attacker.Hit(counterDamage, this, AttackTypes.AtkPhy);
                }
            }
        }

        private bool ParriedAttack(Character attacker)
        {
            Random rng = new Random();
            int parryChance = rng.Next(1, 101);

            if (parryChance <= Parry)
            {
                Console.WriteLine($"{Name} parried the attack from {attacker.Name}!");
                return true;
            }
            return false;
        }

        public override void ChooseAction(List<Character> allies, List<Character> enemies)
        {
            // Implement user input or AI to choose between Heroic Strike, Battle Cry, or Twirl.
        }

        public void ReduceSkillCooldowns()
        {
            HeroicStrike.ReduceCooldown();
            BattleCry.ReduceCooldown();
            Twirl.ReduceCooldown();
        }
    }
}