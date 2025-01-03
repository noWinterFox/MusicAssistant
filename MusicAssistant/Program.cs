namespace MusicAssistant;

class Program
{
    public static bool IsRunning = true;
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the MusicAssistantTool assistant tool! \n write the /help command for a list of commands and definitions");

        while (IsRunning)
        {
            string input = Console.ReadLine();

            if (input != null && input.Contains('/'))
            {
                try
                {
                    Commands command = CommandHelper.ParseCommand(input);
                    CommandHelper.ExecuteCommand(command);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
            }
            else if(input == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input cannot be empty");
                Console.ResetColor();
            }
        }
    }
}