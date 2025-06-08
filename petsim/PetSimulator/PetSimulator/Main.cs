using System;
using System.Timers;
using PetSimulator;

namespace PetSimulator
{
    class Program
    {
        static Adoption adoption = new Adoption();
        static Timer displayTimer;
        static Timer statTimer;

        static void Main()
        {
            StartTimers();

            while (true)
            {
                string input = Console.ReadLine();
                if (input?.ToLower() == "a")
                {
                    ShowAdoptionCenter();
                }
                else if (input?.ToLower() == "q")
                {
                    Console.WriteLine("Exiting...");
                    return;
                }
            }
        }

        static void StartTimers()
        {
            displayTimer = new Timer(1000);
            displayTimer.Elapsed += (sender, e) =>
            {
                Console.Clear();
                Console.WriteLine("YOUR PETS (update per sec):");

                var pets = adoption.GetAllPets();
                if (pets.Count == 0)
                {
                    Console.WriteLine("You don't have any pets yet.");
                }
                else
                {
                    foreach (var pet in pets)
                    {
                        pet.ShowInformation();
                    }
                }

                string deathMessage = adoption.GetDeathMessageIfRecent();
                if (!string.IsNullOrEmpty(deathMessage))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine(deathMessage);
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.WriteLine("Type 'a' to adopt a new pet.");
                Console.WriteLine("Type 'q' to quit.");
            };
            displayTimer.AutoReset = true;
            displayTimer.Enabled = true;

            statTimer = new Timer(3000);
            statTimer.Elapsed += (sender, e) =>
            {
                adoption.UpdateAllPets();
            };
            statTimer.AutoReset = true;
            statTimer.Enabled = true;
        }

        static void ShowAdoptionCenter()
        {
            displayTimer.Stop();
            statTimer.Stop();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("ADOPTION CENTER");
                Console.WriteLine("1. Adopt a new pet");
                Console.WriteLine("2. Return to pet screen");
                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        bool adopted = adoption.Adopt();
                        if (adopted)
                        {
                            displayTimer.Start();
                            statTimer.Start();
                            return;
                        }
                        break;
                    case "2":
                        displayTimer.Start();
                        statTimer.Start();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}