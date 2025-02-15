namespace kpoHomework.Domain.Entities.Animals;

/// <summary>
/// Monkey class that implementing Herbo abstract class.
/// </summary>
/// <param name="name">Animal name.</param>
/// <param name="food">Amount of food (kg) that animal eating.</param>
/// <param name="isHealthy">Is animal healthy</param>
/// <param name="kindness">kindness level (0 - 10)</param>
public class Monkey(string name, int food, bool isHealthy, int kindness) : Herbo(name, food, isHealthy, kindness)
{
}