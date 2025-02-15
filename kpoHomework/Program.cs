using kpoHomework.Domain.Abstractions;
using kpoHomework.Domain.Organizations;
using kpoHomework.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace kpoHomework;

class Program
{
    /// <summary>
    /// Program start
    /// </summary>
    static void Main()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IVetClinic, VetClinic>();
        services.AddSingleton<Zoo>();

        services.AddSingleton<IConsoleService, ConsoleService>();

        var serviceProvider = services.BuildServiceProvider();

        var consoleService = serviceProvider.GetRequiredService<IConsoleService>();
        consoleService.Run();
    }
}