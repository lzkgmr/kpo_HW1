using kpoHomework.Domain.Abstractions;

namespace kpoHomework.Domain.Entities.Animals;

/// <summary>
/// Animal abstract class that implementing IAlive, IInventory, IKind interfaces.
/// </summary>
/// <param name="name">Animal name.</param>
/// <param name="food">Amount of food (kg) that animal eating.</param>
/// <param name="isHealthy">Is animal healthy</param>
/// <param name="kindness">kindness level (0 - 10)</param>
public abstract class Animal(string name, int food, bool isHealthy, int kindness) : IAlive, IInventory, IKind
{
    /// <summary>
    /// Properies
    /// </summary>
    public string? Name { get; set; } = name;
    public int Food { get; set; } = food;
    public bool  IsHealthy { get; set; } = isHealthy;
    public int Number { get; set; }
    // Predators can be kind too. Cats for example.
    public int Kindness { get; set; } = kindness;
    
    /// <summary>
    /// Overriding ToString method.
    /// </summary>
    /// <returns>String with animal description.</returns>
    public override string ToString()
    {
        return $"{GetType().Name}: Name: {Name}, Food: {Food}, IsHealthy: {IsHealthy}, Number: {Number}, Kindness: {Kindness}";
    }
}