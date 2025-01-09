using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J_RPG.Characters.Common
{
    public abstract class Character
    {
        // Core properties
        public string Name { get; set; }
        public int MaxHP { get; private set; }
        public int HP { get; private set; }
        public int AtkPhy { get; private set; }
        public int AtkMag { get; private set; }
        public ArmorTypes Armor { get; private set; }
        public int Dodge { get; private set; }
        public int Parry { get; private set; }
        public int SpellRES { get; private set; }
        public int Speed { get; private set; }
        public List<Skill> Skills { get; set; }
        public bool IsAlive => HP > 0;
        public List<Character> Allies { get; private set; }

        // Mana properties
        public int MaxMana { get; private set; }
        public int Mana { get; private set; }
        public bool IsManaUser => MaxMana > 0;

        // Constructor
        protected Character(
            string name, int maxHP, int atkPhy, int atkMag, ArmorTypes armor,
            int dodge, int parry, int spellRES, int speed,
            int maxMana = 0, int mana = 0)
        {
            Name = name;
            MaxHP = maxHP;
            HP = maxHP;
            AtkPhy = atkPhy;
            AtkMag = atkMag;
            Armor = armor;
            Dodge = dodge;
            Parry = parry;
            SpellRES = spellRES;
            Speed = speed;

            // Initialisation de la liste des alliés
            Allies = new List<Character>();

            // Initialisation de la mana
            MaxMana = maxMana;
            Mana = mana;

            Skills = new List<Skill>(); // Initialize the Skills list
        }

        // Basic Attack
        public virtual int Attack(AttackTypes attackType)
        {
            return attackType == AttackTypes.AtkPhy ? AtkPhy : AtkMag;
        }

        public void SetAtkPhy(int value)
        {
            AtkPhy = value;
        }

        public void IncreaseAtkPhy(int value)
        {
            AtkPhy += value;
            Console.WriteLine($"{Name}'s physical attack power increased by {value}. New AtkPhy: {AtkPhy}");
        }

        // Method to handle receiving damage
        public virtual void Hit(int damage, Character attacker, AttackTypes attackType)
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} cannot be attacked as they are already dead.");
                return;
            }

            Console.WriteLine($"{attacker.Name} attacks {Name}!");

            // Apply defenses and calculate final damage
            Defend(attackType, damage);
        }

        // Defense logic
        public void Defend(AttackTypes attackType, int rawDamage)
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} is already dead and cannot be attacked.");
                return;
            }

            Random rng = new Random();

            // Dodge chance
            if (attackType == AttackTypes.AtkPhy && rng.Next(100) < Dodge)
            {
                Console.WriteLine($"{Name} dodged the attack!");
                return;
            }

            // Parry chance
            if (attackType == AttackTypes.AtkPhy && rng.Next(100) < Parry)
            {
                rawDamage /= 2;
                Console.WriteLine($"{Name} parried the attack! Damage halved to {rawDamage}.");
            }

            // Spell resistance chance
            if (attackType == AttackTypes.AtkMag && rng.Next(100) < SpellRES)
            {
                Console.WriteLine($"{Name} resisted the spell!");
                return;
            }

            // Armor reduction
            rawDamage = CalculateDamageAfterArmor(attackType, rawDamage);

            // Apply damage
            HP -= rawDamage;
            HP = Math.Max(HP, 0); // Prevent HP from dropping below 0

            Console.WriteLine($"{Name} received {rawDamage} damage. Current HP: {HP}");

            // Check if the character dies
            if (HP == 0)
            {
                Console.WriteLine($"{Name} has died.");
            }
        }

        // Heal logic
        public void Heal(int healingAmount)
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} cannot be healed as they are dead.");
                return;
            }

            HP += healingAmount;
            if (HP > MaxHP)
            {
                HP = MaxHP; // Cap HP at MaxHP
            }

            Console.WriteLine($"{Name} healed for {healingAmount} HP. Current HP: {HP}");
        }

        // Mana management
        public void ConsumeMana(int amount)
        {
            if (!IsManaUser)
            {
                Console.WriteLine($"{Name} does not use mana.");
                return;
            }

            if (Mana < amount)
            {
                Console.WriteLine($"{Name} does not have enough mana! Current Mana: {Mana}");
                return;
            }

            Mana -= amount;
            Console.WriteLine($"{Name} used {amount} mana. Remaining Mana: {Mana}");
        }

        public void RegainMana(int amount)
        {
            if (!IsManaUser)
            {
                Console.WriteLine($"{Name} does not use mana.");
                return;
            }

            Mana += amount;
            if (Mana > MaxMana)
            {
                Mana = MaxMana; // Cap Mana at MaxMana
            }

            Console.WriteLine($"{Name} regained {amount} mana. Current Mana: {Mana}");
        }

        // Calculate damage after armor reduction
        private int CalculateDamageAfterArmor(AttackTypes attackType, int damage)
        {
            double reduction = 0;

            if (attackType == AttackTypes.AtkPhy)
            {
                reduction = Armor switch
                {
                    ArmorTypes.Fabric => 0.0,
                    ArmorTypes.Leather => 0.15,
                    ArmorTypes.Mesh => 0.30,
                    ArmorTypes.Plates => 0.45,
                    _ => 0.0
                };
            }
            else if (attackType == AttackTypes.AtkMag)
            {
                reduction = Armor switch
                {
                    ArmorTypes.Fabric => 0.30,
                    ArmorTypes.Leather => 0.20,
                    ArmorTypes.Mesh => 0.10,
                    ArmorTypes.Plates => 0.0,
                    _ => 0.0
                };
            }

            return (int)(damage * (1 - reduction));
        }

        // Display character details
        public override string ToString()
        {
            string manaInfo = IsManaUser ? $", Mana: {Mana}/{MaxMana}" : "";
            return $"{Name} - HP: {HP}/{MaxHP}, AtkPhy: {AtkPhy}, AtkMag: {AtkMag}, Armor: {Armor}, Speed: {Speed}, Dodge: {Dodge}%, Parry: {Parry}%, SpellRES: {SpellRES}%{manaInfo}";
        }

        // Abstract method for selecting an action
        public abstract void ChooseAction(List<Character> allies, List<Character> enemies);
    }
}