namespace kpoHomework.Domain.Entities.Animals;

/// <summary>
/// Wolf class that implementing Predator abstract class.
/// </summary>
/// <param name="name">Animal name.</param>
/// <param name="food">Amount of food (kg) that animal eating.</param>
/// <param name="isHealthy">Is animal healthy</param>
/// <param name="kindness">kindness level (0 - 10)</param>
public class Wolf(string name, int food, bool isHealthy, int kindness) : Predator(name, food, isHealthy, kindness)
{
}