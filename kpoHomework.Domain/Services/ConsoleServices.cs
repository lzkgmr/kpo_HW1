using kpoHomework.Domain.Abstractions;
using kpoHomework.Domain.Entities.Animals;
using kpoHomework.Domain.Entities.Things;
using kpoHomework.Domain.Organizations;

namespace kpoHomework.Domain.Services;

/// <summary>
/// Console Service for Zoo.
/// </summary>
public class ConsoleService(Zoo zoo) : IConsoleService
{
    /// <summary>
    /// Start service.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            PrintMenu();
            Console.Write("Выбрали: ");
            var input = Console.ReadLine();
            Console.Clear();
            switch (input)
            {
                case "1":
                    AddAnimal();
                    break;
                case "2":
                    AddThing();
                    break;
                case "3":
                    Console.WriteLine($"Количество еды в день: {zoo.GetFoodPerDay()}");
                    break;
                case "4":
                    Console.WriteLine($"Количество животных: {zoo.GetAnimalCount()}");
                    break;
                case "5":
                    PrintAnimals();
                    break;
                case "6":
                    PrintContactAnimals();
                    break;
                case "7":
                    PrintInventory();
                    break;
                case "8":
                    Console.WriteLine("Выход из программы...");
                    return;
                default:
                    Console.WriteLine("Некорректный ввод, попробуйте снова.\n");
                    break;
            }
        }
    }

    /// <summary>
    /// Printing menu into console.
    /// </summary>
    private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Выберите действие:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1. Добавить животное в зоопарк");
            Console.WriteLine("2. Добавить вещь в зоопарк");
            Console.WriteLine("3. Количество еды в день");
            Console.WriteLine("4. Количество животных");
            Console.WriteLine("5. Животные в зоопарке");
            Console.WriteLine("6. Животные в контактном зоопарке");
            Console.WriteLine("7. Инвентарь зоопарка");
            Console.WriteLine("8. Выйти из программы");
            Console.ResetColor();
        }

    /// <summary>
    /// Adding animals to the zoo.
    /// </summary>
    private void AddAnimal()
    {
        Console.Clear();
        Console.WriteLine("Выберите животное для добавления (1 - 4): ");
        Console.Write("1. Обезьяна\n2. Заяц\n3. Тигр\n4. Волк\n");
        if (!TryParseInt(out int choice, 1, 4))
        {
            Console.WriteLine("Некорректный ввод. Введите число от 1 до 4.\n");
            return;
        }
        
        Console.Clear();
        Console.Write("Введите имя: ");
        if (!TryParseString(out string name))
        {
            Console.WriteLine("Некорректный ввод. Нужно ввести непустое имя.");
            return;
        }
        
        Console.Write("Введите суточное потребление еды: ");
        if (!TryParseUint(out int food))
        {
            Console.WriteLine("Некорректный ввод. Потребление еды должно быть положительным числом.");
            return;
        }
        
        Console.Write("Введите информацию о результате осмотра ветеринаром (1 - Здоров. 2 - Болен) : ");
        if (!TryParseCase(out bool isHealthy))
        {
            Console.WriteLine("Некорректный ввод. Введите либо 1, либо 2.");
            return;
        }

        if (!isHealthy)
        {
            Console.WriteLine("Это животное болеет. Его нельзя поселить в зоопарк.");
            return;
        }
        
        Console.Write("Введите уровень доброты (1-10): ");
        if (!TryParseInt(out int kindness, 1, 10))
        {
            Console.WriteLine("Некорректный ввод. Доброта должна быть числом от 1 до 10.");
            return;
        }
        
        Animal animal;
        switch (choice)
        {
            case 1:
                animal = new Monkey(name, food, isHealthy, kindness);
                break;
            case 2:
                animal = new Rabbit(name, food, isHealthy, kindness);
                break;
            case 3:
                animal = new Tiger(name, food, isHealthy, kindness);
                break;
            default:
                animal = new Wolf(name, food, isHealthy, kindness);
                break;
        }

        zoo.AddAnimal(animal);
        Console.WriteLine("Животное добавлено успешно!");
    }

    /// <summary>
    /// Adding thing.
    /// </summary>
    private void AddThing()
    {
        Console.Clear();
        Console.WriteLine("Выберите тип вещи для добавления (1 - 2): ");
        Console.Write("1. Стол\n2. Компьютер\n");
        if (!TryParseInt(out int choice, 1, 2))
        {
            Console.WriteLine("Некорректный ввод. Введите число от 1 до 2.\n");
            return;
        }
        
        Thing thing = choice == 1 ? new Table() : new Computer();
        zoo.AddThing(thing);
        Console.WriteLine("Вещь успешно добавлена!.");
    }

    /// <summary>
    /// Printing animals into console.
    /// </summary>
    private void PrintAnimals()
    {
        var items = zoo.GetAnimals();
        if (items.Count == 0)
        {
            Console.WriteLine("В зоопарке пока нет животных.");
            return;
        }
            
        Console.WriteLine("Животные в зоопарке:");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
    
    /// <summary>
    /// Printing contact animals into console.
    /// </summary>
    private void PrintContactAnimals()
    {
        var items = zoo.GetContactZooAnimals();
        
        if (items.Count == 0)
        {
            Console.WriteLine("В контактном зоопарке пока нет животных.");
            return;
        }
            
        Console.WriteLine("Животные в контактном зоопарке:");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
    
    /// <summary>
    /// Printing inventory list into console.
    /// </summary>
    private void PrintInventory()
    {
        var items = zoo.GetInventoryItems();
        
        if (items.Count == 0)
        {
            Console.WriteLine("Зоопарк пуст.");
            return;
        }
            
        Console.WriteLine("Содержимое зоопарка:");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// Parsing input to non empty string.
    /// </summary>
    /// <param name="input">User input</param>
    /// <returns>True if input is correct, false otherwise.</returns>
    private bool TryParseString(out string input)
    {
        string? s = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(s))
        {
            input = s;
            return true;
        }
        input = "";
        return false;
    }
    
    /// <summary>
    /// Parsing user input to int.
    /// </summary>
    /// <param name="result">User input</param>
    /// <param name="min">Min available number.</param>
    /// <param name="max">Max available number.</param>
    /// <returns>True if input is correct, false otherwise.</returns>
    private bool TryParseInt(out int result, int min, int max)
    {
        string? input = Console.ReadLine();
        if (int.TryParse(input, out result) && result >= min && result <= max)
        {
            return true;
        }
        result = 0;
        return false;
    }
    
    /// <summary>
    /// Parsing user input to uint.
    /// </summary>
    /// <param name="result">User input</param>
    /// <returns>True if input is correct, false otherwise.</returns>
    private bool TryParseUint(out int result)
    {
        string? readLine = Console.ReadLine();
        if (int.TryParse(readLine, out result) && result > 0)
        {
            return true;
        }
        result = 0;
        return false;
    }
    
    /// <summary>
    /// Parsing user input to boolean.
    /// </summary>
    /// <param name="result">User input</param>
    /// <returns>True if input correct, false otherwise.</returns>
    private bool TryParseCase(out bool result)
    {
        string? readLine = Console.ReadLine();
        switch (readLine)
        {
            case "1":
                result =  true;
                return true;
            case "2":
                result =  false;
                return true;
            default:
                result = false;
                return false;
        }
    }
}