// See https://aka.ms/new-console-template for more information

using Battleships;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = Di.RegisterDi();

var consoleUi = serviceProvider.GetService<ConsoleUi>();
if (consoleUi == null)
{
    Console.WriteLine("Fatal error");
}
else
{
    consoleUi.Run();
}