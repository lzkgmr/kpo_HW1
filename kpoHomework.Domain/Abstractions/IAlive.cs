namespace kpoHomework.Domain.Abstractions;

/// <summary>
/// Interface for alive objects.
/// </summary>
public interface IAlive
{
    int Food { get; set; }
    bool IsHealthy { get; set; }
    string Name { get; set; }
}
