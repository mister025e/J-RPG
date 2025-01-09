using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using J_RPG.Characters.Common;

namespace J_RPG.Characters.Warrior
{
    public class BattleCry : Skill
    {
        public BattleCry()
        {
            Name = "Battle Cry";
            Cooldown = 2;
            CurrentCooldown = 0;
        }

        public override void Use(Character user, List<Character> allies)
        {
            foreach (var ally in allies)
            {
                // Apply the Battle Cry effect to each ally
                // Example: Increase attack power
                ally.SetAtkPhy(ally.AtkPhy + 25);
                Console.WriteLine($"{ally.Name}'s attack power increased by 10!");
            }

            // Réinitialiser le temps de recharge de la compétence
            CurrentCooldown = Cooldown;
        }

        public void ReduceCooldown()
        {
            if (CurrentCooldown > 0)
                CurrentCooldown--;
        }
    }
}
