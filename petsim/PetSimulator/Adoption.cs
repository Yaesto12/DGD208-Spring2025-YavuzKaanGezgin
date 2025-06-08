using System;

public class Adoption
{
	private List<Pets> adoptedPets = new();


	public Adopt()
	{
		var Types = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();

		Console.WriteLine("\nChoose the type of animal you want to adopt.");
		for (int i = 0; i < Types.Count; i++)
		{
			Console.WriteLine($"{i + 1}. {Types[i]}");
		}

		int Choose = 0;
		while (Choose < 1 || Choose > Types.Count)
		{
			Console.Write("Choose (1-" + Types.Count + "): ");
			int.TryParse(Console.ReadLine(), out Choose);
		}


		PetType choosenType = Types[Choose - 1];

		Console.Write("What name do you see fit for your adopted animal? ");
		string name = Console.ReadLine();

		Pets newPet = new Pets(name, choosenType);
		adoptedPets.Add(newPet);

		Console.WriteLine($"\Congratulations! You have a new pet which is a {choosenType}.\n");

	}


	public void List()
	{
		Console.WriteLine("\n Adopted pets:");
		if (adoptedPets.count == 0)
		{
			Console.WriteLine("You did not adopted a pet yet.");
			return;
		}


		foreach (var pet in adoptedPets)
		{
			pet.ShowInformation();

		}
	}
}
