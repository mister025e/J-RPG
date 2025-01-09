using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace J_RPG.Utils
{
    public static class InputHandler
    {
        public static int GetUserChoice(int min, int max)
        {
            int choice;
            while (true)
            {
                Console.Write($"Enter a choice ({min}-{max}): ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
                    break;
                Console.WriteLine("Invalid input. Please try again.");
            }
            return choice;
        }
    }
}
