using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J_RPG.Characters.Common;

namespace J_RPG.Characters
{
    public class Thief : Character
    {
        public Thief(string name, int maxHP, int atkPhy, int atkMag, int dodge, int parry, int spellRES, int speed)
            : base(name, maxHP, atkPhy, atkMag, ArmorTypes.Leather, dodge, parry, spellRES, speed)
        {
        }

        public override void ChooseAction(List<Character> allies, List<Character> enemies)
        {
            // Implement Thief-specific logic
        }
    }
}
