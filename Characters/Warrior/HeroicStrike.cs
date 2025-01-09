using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J_RPG.Characters.Common;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace J_RPG.Characters.Warrior
{
    public class HeroicStrike : Skill
    {
        public HeroicStrike()
        {
            Name = "Heroic Strike";
            Cooldown = 2;
            CurrentCooldown = 0;
        }

        public override void Use(Character user, Character target)
        {
            if (!CanUse())
                throw new InvalidOperationException($"{Name} is on cooldown!");

            int damage = user.Attack(AttackTypes.AtkPhy) + 50;
            target.Hit(damage, user, AttackTypes.AtkPhy);
            CurrentCooldown = Cooldown;
        }

        public override void Use(Character user, List<Character> targets)
        {
            // HeroicStrike is intended for a single target; apply the effect to the first target in the list.
            if (targets == null || targets.Count == 0)
                throw new ArgumentException("No targets provided for Heroic Strike.");

            Use(user, targets[0]);
        }
    }
}
