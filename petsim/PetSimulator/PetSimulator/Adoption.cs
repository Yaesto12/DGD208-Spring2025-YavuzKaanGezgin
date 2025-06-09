using System;
using System.Collections.Generic;
using System.Linq;

namespace PetSimulator
{
    public class Adoption
    {
        private List<Pet> adoptedPets = new List<Pet>();
        private string lastDeathMessage = "";
        private DateTime lastDeathTime;

        public bool Adopt()
        {
            var types = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();

            Console.WriteLine();
            Console.WriteLine("Choose the type of animal you want to adopt:");
            for (int i = 0; i < types.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {types[i]}");
            }

            int choice = 0;
            while (choice < 1 || choice > types.Count)
            {
                Console.Write("Choose (1-" + types.Count + "): ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            PetType selectedType = types[choice - 1];

            Console.Write("What name do you give your new pet? ");
            string name = Console.ReadLine();

            Pet newPet = new Pet(name, selectedType);
            adoptedPets.Add(newPet);

            Console.WriteLine();
            Console.WriteLine($"You adopted a {selectedType} named {name}!");
            Console.WriteLine("Press Enter to return to your pets.");
            Console.ReadLine();
            return true;
        }

        public List<Pet> GetAllPets()
        {
            return adoptedPets;
        }

        public List<Pet> GetPets()
        {
            return adoptedPets;
        }


        public void UpdateAllPets()
        {
            foreach (var pet in adoptedPets.ToList())
            {
                if (pet.IsAlive)
                {
                    pet.UpdateStats();
                    if (!pet.IsAlive)
                    {
                        lastDeathMessage = $"{pet.Name} the {pet.Type} has died due to neglect.";
                        lastDeathTime = DateTime.Now;
                    }
                }
            }

            adoptedPets = adoptedPets.Where(p => p.IsAlive).ToList();
        }

        public string GetDeathMessageIfRecent()
        {
            if (!string.IsNullOrEmpty(lastDeathMessage) &&
                (DateTime.Now - lastDeathTime).TotalSeconds <= 10)
            {
                return lastDeathMessage;
            }

            return null;
        }
    }
}