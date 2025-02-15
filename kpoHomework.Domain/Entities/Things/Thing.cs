using kpoHomework.Domain.Abstractions;

namespace kpoHomework.Domain.Entities.Things;

/// <summary>
/// Things abstract class that implementing IInventory interface.
/// </summary>
public abstract class Thing : IInventory
{
    // Counting number.
    public int Number { get; set; }
    
    /// <summary>
    /// Overriding ToString method.
    /// </summary>
    /// <returns>String with description.</returns>
    public override string ToString()
    {
        return $"{GetType().Name}: Number: {Number}";
    }
}