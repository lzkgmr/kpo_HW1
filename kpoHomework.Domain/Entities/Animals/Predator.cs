namespace kpoHomework.Domain.Entities.Animals;

/// <summary>
/// Predator abstract class that implementing Animal abstract class.
/// </summary>
/// <param name="name">Animal name.</param>
/// <param name="food">Amount of food (kg) that animal eating.</param>
/// <param name="isHealthy">Is animal healthy</param>
/// <param name="kindness">kindness level (0 - 10)</param>
public abstract class Predator(string name, int food, bool isHealthy, int kindness) : Animal(name, food, isHealthy, kindness)
{
}