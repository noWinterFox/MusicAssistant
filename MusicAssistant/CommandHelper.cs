namespace MusicAssistant;

public abstract class CommandHelper
{
    // command parse
    public static Commands ParseCommand(string command)
    {
        command = command.Remove(0, 1);
        
        if (Enum.TryParse(command, true, out Commands parsedCommand))
        {
            if (Enum.IsDefined(typeof(Commands), parsedCommand))
            {
                return parsedCommand;
            }
        }

        throw new ArgumentException("Invalid command, type /help to check the command list");
    }

    // command execution
    public static void ExecuteCommand(Commands command)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        switch (command)
        {
            case Commands.Help:
                ListCommands();
                break;
            
            case Commands.Scale:
                Scale.InitiateScale();
                break;
        }
        
        Console.ResetColor();
    }
    
    // commands
    private static void ListCommands()
    {
        Console.WriteLine("/help - lists all of the tools commands");
        Console.WriteLine("/scale - generates a scale based on a key and mode");
    }
}