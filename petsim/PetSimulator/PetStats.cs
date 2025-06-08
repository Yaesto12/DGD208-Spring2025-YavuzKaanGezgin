using System;

public class Pet
{
	public string Name { get; set; }
	public PetType Type { get; set; }
	public int Fun { get; set; }
	public int Sleep { get; set; }
	public int Hunger { get; set; }



	public Pet(string name, string type)
	{
		Name = name;
		Type = type;
		Fun = 50
		Sleep = 50
		Hunger = 50
	}


	public void ShowInformation()
	{

		Console.Writeline($"\nYour pet is a {Type}.");
		Console.Writeline($"Your pets name is {Name}.");
       








    }
}
