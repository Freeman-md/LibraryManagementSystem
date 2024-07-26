namespace LibraryManagementSystem;

public static class InputValidator
{
    public static string GetValidInput(string prompt, string errorMessage, string defaultValue = "")
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                return defaultValue;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
            return GetValidInput(prompt, errorMessage, defaultValue);
        }

        return input;
    }
}
