using System;

namespace Shared.Util;

public static class Print
{
    public static void ErrorMessage(string Print)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{Print}");
        Console.ResetColor();
    }

    public static void InfoMessage(string Print)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{Print}");
        Console.ResetColor();
    }

    public static void SuccessMessage(string Print)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{Print}");
        Console.ResetColor();
    }
}
