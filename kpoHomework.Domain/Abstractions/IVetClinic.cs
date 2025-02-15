namespace kpoHomework.Domain.Abstractions;

public interface IVetClinic : IOrganization
{
    /// <summary>
    /// Check animal health.
    /// </summary>
    /// <param name="animal">Animal which health is checking.</param>
    /// <returns>True if animal is healthy, false otherwise.</returns>
    public bool CheckHealth(IAlive animal)
    {
        return animal.IsHealthy;
    }
}