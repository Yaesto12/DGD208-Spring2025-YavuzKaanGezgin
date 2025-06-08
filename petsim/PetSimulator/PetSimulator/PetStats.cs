using System;

namespace PetSimulator
{
    public class Pet
    {
        public string Name { get; set; }
        public PetType Type { get; set; }
        public int Fun { get; set; }
        public int Sleep { get; set; }
        public int Hunger { get; set; }
        public bool IsAlive { get; private set; } = true;

        public Pet(string name, PetType type)
        {
            Name = name;
            Type = type;
            Fun = 50;
            Sleep = 50;
            Hunger = 50;
        }
     
        public void ShowInformation()
        {
            Console.WriteLine();
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"Name: {Name}");

            WriteColoredStat("Fun", Fun);
            WriteColoredStat("Sleep", Sleep);
            WriteColoredStat("Hunger", Hunger);
        }

        public void UpdateStats()
        {
            if (!IsAlive)
                return;

            Fun = Clamp(Fun - 1);
            Sleep = Clamp(Sleep - 1);
            Hunger = Clamp(Hunger + 1);

            if (Fun == 0 || Sleep == 0 || Hunger == 100)
            {
                IsAlive = false;
            }
        }

        private int Clamp(int value)
        {
            if (value < 0) return 0;
            if (value > 100) return 100;
            return value;
        }

        private void WriteColoredStat(string label, int value)
        {
            Console.Write($"{label}: ");
            Console.ForegroundColor = label == "Hunger" ? GetHungerColor(value) : GetGenericStatColor(value);
            Console.WriteLine($"{value}/100");
            Console.ResetColor();
        }

        private ConsoleColor GetHungerColor(int value)
        {
            if (value >= 61)
                return ConsoleColor.Red;
            else if (value >= 31)
                return ConsoleColor.Yellow;
            else
                return ConsoleColor.Green;
        }

        private ConsoleColor GetGenericStatColor(int value)
        {
            if (value <= 30)
                return ConsoleColor.Red;
            else if (value <= 60)
                return ConsoleColor.Yellow;
            else
                return ConsoleColor.Green;
        }
    }
}