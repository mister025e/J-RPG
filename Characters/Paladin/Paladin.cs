using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J_RPG.Characters.Common;

namespace J_RPG.Characters
{
    public class Paladin : Character
    {
        public Paladin(string name, int maxHP, int atkPhy, int atkMag, int dodge, int parry, int spellRES, int speed, int maxMana, int mana)
            : base(name, maxHP, atkPhy, atkMag, ArmorTypes.Plates, dodge, parry, spellRES, speed, maxMana, mana)
        {
        }

        public override void ChooseAction(List<Character> allies, List<Character> enemies)
        {
            // Implement Paladin-specific logic
        }
    }
}
