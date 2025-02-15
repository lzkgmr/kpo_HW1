using kpoHomework.Domain.Abstractions;
using kpoHomework.Domain.Entities.Animals;
using kpoHomework.Domain.Entities.Things;

namespace kpoHomework.Domain.Organizations;

/// <summary>
/// Zoo class for zoo.
/// </summary>
public class Zoo(IVetClinic clinic) : IOrganization
{
    private int _numberCounter = 0;
    private readonly List<IInventory> _inventory = [];

    /// <summary>
    /// Adding animal to the zoo.
    /// </summary>
    /// <param name="animal">Animal that we're adding.</param>
    /// <returns>True, animal was added. False, otherwise. </returns>
    public bool AddAnimal(Animal animal)
    {
        if (!clinic.CheckHealth(animal)) {
            return false;
        }
        animal.Number = ++_numberCounter;
        _inventory.Add(animal);
        return true;
    }
    
    /// <summary>
    /// Adding thing to the zoo.
    /// </summary>
    /// <param name="thing">Thing that we're adding.</param>
    public void AddThing(Thing thing)
    {
        thing.Number = ++_numberCounter;
        _inventory.Add(thing);
    }

    /// <summary>
    /// Get total animals amount.
    /// </summary>
    /// <returns>Animals amount.</returns>
    public int GetAnimalCount()
    {
        return _inventory.OfType<Animal>().Count();
    } 
    
    /// <summary>
    /// Get total amount of food that animals eat per day (kg).
    /// </summary>
    /// <returns>Total amount of food (kg).</returns>
    public int GetFoodPerDay()
    {
        return _inventory.OfType<IAlive>().Sum(a => a.Food);
    }
    
    /// <summary>
    /// Get contact animals.
    /// </summary>
    /// <returns>Animals list.</returns>
    public List<Animal> GetAnimals()
    {
        return _inventory.OfType<Animal>().ToList();
    }
    
    /// <summary>
    /// Get contact animals.
    /// </summary>
    /// <returns>Animals list.</returns>
    public List<Animal> GetContactZooAnimals()
    {
        return _inventory.OfType<Animal>().Where(x => x.Kindness >= 5).ToList();
    }
    
    /// <summary>
    /// Get inventories.
    /// </summary>
    /// <returns>Inventory list.</returns>
    public List<IInventory> GetInventoryItems()
    {
        return _inventory;
    }
}