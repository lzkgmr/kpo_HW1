using kpoHomework.Domain.Abstractions;
using kpoHomework.Domain.Entities.Animals;
using kpoHomework.Domain.Entities.Things;
using kpoHomework.Domain.Organizations;

namespace kpoHomework.Tests;

using Moq;
using Xunit;

/// <summary>
/// Tests for Zoo class.
/// </summary>
public class ZooTest
{
    private readonly Zoo _zoo;

    /// <summary>
    /// Constructor for tests.
    /// </summary>
    public ZooTest()
    {
        Mock<IVetClinic> vetClinicMock = new();
        vetClinicMock.Setup(vc => vc.CheckHealth(It.IsAny<Animal>())).Returns(true);
        _zoo = new Zoo(vetClinicMock.Object);
    }

    [Fact]
    public void AddAnimal_HealthyAnimal_ReturnsTrueAndAddsToInventory()
    {
        Animal animal = new Tiger("rrr", 50, true, 0);

        var result = _zoo.AddAnimal(animal);

        Assert.True(result);
        Assert.Equal(1, _zoo.GetAnimalCount());
    }

    [Fact]
    public void AddThing_AddsThingToInventory()
    {
        Thing thing = new Computer();

        _zoo.AddThing(thing);

        Assert.Contains(thing, _zoo.GetInventoryItems());
    }

    [Fact]
    public void GetAnimalCount_ReturnsCorrectNumber()
    {
        _zoo.AddAnimal(new Rabbit("Aizek", 1, true, 4));
        _zoo.AddAnimal(new Wolf("Auf", 30, true, 0));

        Assert.Equal(2, _zoo.GetAnimalCount());
    }

    [Fact]
    public void GetTotalFoodPerDay_ReturnsCorrectSum()
    {
        _zoo.AddAnimal(new Rabbit("Aizek", 1, true, 4));
        _zoo.AddAnimal(new Wolf("Auf", 30, true, 0));
        _zoo.AddAnimal(new Monkey("ChiChiChi", 1, true, 4));
        _zoo.AddAnimal(new Tiger("rrr", 50, true, 0));

        Assert.Equal(82, _zoo.GetFoodPerDay());
    }
    
    [Fact]
    public void Zoo_Constructor_InitializesInventory()
    {
        var clinic = new Mock<IVetClinic>().Object;
        
        var zoo = new Zoo(clinic);

        Assert.NotNull(zoo.GetInventoryItems());
        Assert.Empty(zoo.GetInventoryItems());
    }

    [Fact]
    public void GetContactZooAnimals_ReturnsOnlyContactAnimals()
    {
        var clinic = new Mock<IVetClinic>();
        clinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
        var zoo = new Zoo(clinic.Object);

        var contactHerbo = new Monkey("ChiChiChi", 1, true, 6); 
        var nonContactHerbo = new Rabbit("Aizek", 1, true, 4);
        var nonContactPredator = new Tiger("rrr", 50, true, 0);

        zoo.AddAnimal(contactHerbo);
        zoo.AddAnimal(nonContactHerbo);
        zoo.AddAnimal(nonContactPredator);
        
        var result = zoo.GetContactZooAnimals();
        
        Assert.Single(result);
        Assert.Contains(contactHerbo, result);
    }

    [Fact]
    public void GetInventoryItems_ReturnsAllItems()
    {
        Animal animal = new Tiger("rrr", 50, true, 0);
        Thing thing = new Computer();
        
        _zoo.AddAnimal(animal);
        _zoo.AddThing(thing);

        var result = _zoo.GetInventoryItems();

        Assert.Contains(animal, result);
        Assert.Contains(thing, result);
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public void Thing_ToString_ReturnsCorrectFormat()
    {
        Thing computer = new Computer();
        computer.Number = 7;
        
        Assert.Equal("Computer: Number: 7", computer.ToString());
    }

    [Fact]
    public void Animal_ToString_ReturnsCorrectFormat()
    {
        Animal monkey = new Monkey("ChiChiChi", 1, true, 4);
        monkey.Number = 2;
        
        Assert.Equal($"Monkey: Name: ChiChiChi, Food: 1, IsHealthy: True, Number: 2, Kindness: 4", monkey.ToString());
    }
    
    [Fact]
    public void AddAnimal_UnhealthyAnimal_ReturnsFalseAndDoesNotAddToInventory()
    {
        var clinic = new Mock<IVetClinic>();
        clinic.Setup(vc => vc.CheckHealth(It.IsAny<Animal>())).Returns(false);
        var zoo = new Zoo(clinic.Object);
        var animal = new Tiger("rrr", 50, true, 0);
        
        var result = zoo.AddAnimal(animal);

        Assert.False(result);
        Assert.Equal(0, zoo.GetAnimalCount()); 
    }
    
    [Fact]
    public void AddAnimalAndThing_AssignsUniqueNumbers()
    {
        var clinic = new Mock<IVetClinic>();
        clinic.Setup(vc => vc.CheckHealth(It.IsAny<Animal>())).Returns(true);
        var zoo = new Zoo(clinic.Object);

        var animal1 = new Tiger("rrr", 50, true, 0);
        var animal2 = new Rabbit("Aizek", 1, true, 4);
        var thing = new Computer();
        
        zoo.AddAnimal(animal1);
        zoo.AddAnimal(animal2);
        zoo.AddThing(thing);
        
        Assert.Equal(1, animal1.Number); 
        Assert.Equal(2, animal2.Number);
        Assert.Equal(3, thing.Number); 
    }
    
    [Fact]
    public void GetContactZooAnimals_NoContactAnimals_ReturnsEmptyList()
    {
        var clinic = new Mock<IVetClinic>();
        clinic.Setup(vc => vc.CheckHealth(It.IsAny<Animal>())).Returns(true);
        var zoo = new Zoo(clinic.Object);

        var nonContactHerbo = new Rabbit("Aizek", 1, true, 4);
        var nonContactPredator = new Tiger("rrr", 50, true, 0);

        zoo.AddAnimal(nonContactHerbo);
        zoo.AddAnimal(nonContactPredator);
        
        var result = zoo.GetContactZooAnimals();
        
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetTotalFoodPerDay_NoAnimals_ReturnsZero()
    {
        var clinic = new Mock<IVetClinic>().Object;
        var zoo = new Zoo(clinic);

        var totalFood = zoo.GetFoodPerDay();
        
        Assert.Equal(0, totalFood);
    }
    
    [Fact]
    public void GetAnimalCount_NoAnimals_ReturnsZero()
    {
        var clinic = new Mock<IVetClinic>().Object;
        var zoo = new Zoo(clinic);
        
        var count = zoo.GetAnimalCount();
        
        Assert.Equal(0, count);
    }
    
    [Fact]
    public void AddThing_AddsMultipleThingsToInventory()
    {
        var clinic = new Mock<IVetClinic>().Object;
        var zoo = new Zoo(clinic);

        var thing1 = new Computer();
        var thing2 = new Table();
        
        zoo.AddThing(thing1);
        zoo.AddThing(thing2);
        
        Assert.Contains(thing1, zoo.GetInventoryItems());
        Assert.Contains(thing2, zoo.GetInventoryItems()); 
        Assert.Equal(2, zoo.GetInventoryItems().Count); 
    }
}