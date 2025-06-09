using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSimulator
{
    public static class ItemUsage
    {
        public static async Task UseItemOnPet(List<Pet> pets)
        {
            if (pets == null || pets.Count == 0)
            {
                Console.WriteLine("You don't have any pets.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a pet:");
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pets[i].Name} ({pets[i].Type})");
            }

            int petIndex = GetUserChoice(pets.Count);
            Pet selectedPet = pets[petIndex];

          
            var compatibleItems = ItemDatabase.AllItems
                .Where(item => item.CompatibleWith.Contains(selectedPet.Type))
                .ToList();

            if (compatibleItems.Count == 0)
            {
                Console.WriteLine("No items are compatible with this pet.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nItems available for {selectedPet.Name}:");
            for (int i = 0; i < compatibleItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {compatibleItems[i].Name} (Affects {compatibleItems[i].AffectedStat}: {compatibleItems[i].EffectAmount}, Duration: {compatibleItems[i].Duration}s)");
            }

            int itemIndex = GetUserChoice(compatibleItems.Count);
            Item selectedItem = compatibleItems[itemIndex];

            Console.WriteLine($"\nUsing {selectedItem.Name} on {selectedPet.Name}... (please wait {selectedItem.Duration} seconds)");
            await Task.Delay((int)(selectedItem.Duration * 1000));

            ApplyItemEffect(selectedPet, selectedItem);

            Console.WriteLine($"{selectedItem.Name} used! {selectedItem.AffectedStat} increased by {selectedItem.EffectAmount}.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static int GetUserChoice(int count)
        {
            int choice = -1;
            while (choice < 0 || choice >= count)
            {
                Console.Write("Choose a number: ");
                int.TryParse(Console.ReadLine(), out choice);
                choice--;
            }
            return choice;
        }

        private static void ApplyItemEffect(Pet pet, Item item)
        {
            switch (item.AffectedStat)
            {
                case PetStat.Hunger:
                    pet.Hunger = Clamp(pet.Hunger - item.EffectAmount); 
                    break;
                case PetStat.Fun:
                    pet.Fun = Clamp(pet.Fun + item.EffectAmount);
                    break;
                case PetStat.Sleep:
                    pet.Sleep = Clamp(pet.Sleep + item.EffectAmount);
                    break;
            }
        }

        private static int Clamp(int value)
        {
            if (value < 0) return 0;
            if (value > 100) return 100;
            return value;
        }
    }
}
