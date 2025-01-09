using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace J_RPG.Characters.Common
{
    public abstract class Skill
    {
        public string Name { get; set; }
        public int Cooldown { get; set; }
        public int CurrentCooldown { get; set; }
        public bool CanUse() => CurrentCooldown == 0;

        public virtual void Use(Character user, Character target)
        {
            throw new NotImplementedException("This skill targets a single character.");
        }

        public virtual void Use(Character user, List<Character> targets)
        {
            throw new NotImplementedException("This skill targets multiple characters.");
        }

        public void ReduceCooldown()
        {
            if (CurrentCooldown > 0)
                CurrentCooldown--;
        }
    }
}
