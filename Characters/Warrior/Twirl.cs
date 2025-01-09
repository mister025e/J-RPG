using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J_RPG.Characters.Common;

namespace J_RPG.Characters.Warrior
{
    public class Twirl : Skill
    {
        public Twirl()
        {
            Name = "Twirl";
            Cooldown = 3;
            CurrentCooldown = 0;
        }

        public override void Use(Character user, Character target)
        {
            if (!CanUse())
                throw new InvalidOperationException($"{Name} is on cooldown!");

            int damage = user.Attack(AttackTypes.AtkPhy) + 30;
            target.Hit(damage, user, AttackTypes.AtkPhy);
            CurrentCooldown = Cooldown;
        }

        public override void Use(Character user, List<Character> targets)
        {
            if (!CanUse())
                throw new InvalidOperationException($"{Name} is on cooldown!");

            if (targets == null || targets.Count == 0)
                throw new ArgumentException("No targets provided for Twirl.");

            // Twirl is intended to hit multiple targets; apply the effect to each target.
            int damage = user.Attack(AttackTypes.AtkPhy) + 30;
            foreach (var target in targets)
            {
                target.Hit(damage, user, AttackTypes.AtkPhy);
            }
            CurrentCooldown = Cooldown;
        }
    }
}
