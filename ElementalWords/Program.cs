using ElementalWords.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IElementService, ElementService>()
    .BuildServiceProvider();

var elementService = serviceProvider.GetService<IElementService>();

if (elementService == null)
{
    Console.WriteLine("Failed to load ElementService.");
    return;
}

Console.WriteLine("Loading elements...");

var elementsLoaded = elementService.InitializeElements();

if (!elementsLoaded)
{
    Console.WriteLine("Failed to load elements.");
    return;
}

Console.WriteLine("Elements loaded.");

Console.WriteLine("This program will take a word and break it down into its elemental forms!");

var word = string.Empty;

while (true)
{
    Console.WriteLine("Please enter a word:");
    word = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(word) || !word.All(char.IsAsciiLetter) || word.Contains(' '))
    {
        Console.WriteLine("Please enter ONE valid word consiting ONLY of letters.");
        continue;
    }

    var result = elementService.ElementalForms(word);

    if (result.Count == 0)
    {
        Console.WriteLine("[]");
    }
    else
    {
        foreach (var elementResponse in result)
        {
            Console.WriteLine($"{string.Join(",", elementResponse.Elements.Select(e => $"[{e.Name} ({e.Symbol})]"))}");
        }
    }
}
