namespace LibraryManagementSystem;

public static class InputValidator
{
    public static string GetValidInput(string prompt, string errorMessage)
    {
        string? input;

        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
            }
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }
}
