namespace MusicAssistant;

public class Scale
{
    private static List<Note> NoteScale = new();
    
    // dictionary definition
    private static readonly int[] MajorInterval = [2, 2, 1, 2, 2, 2, 1];
    private static readonly int[] MinorInterval = [2, 1, 2, 2, 1, 2, 2];
    
    private static readonly Dictionary<Mode, int[]> ModeIntervals = new()
    {
        { Mode.Lydian, [2, 2, 2, 1, 2, 2, 1] },
        { Mode.Ionian, MajorInterval },
        { Mode.Major, MajorInterval },
        { Mode.Mixolydian, [2, 2, 1, 2, 2, 1, 2] },
        { Mode.Dorian, [2, 1, 2, 2, 2, 1, 2] },
        { Mode.Aeolian, MinorInterval },
        { Mode.Minor, MinorInterval },
        { Mode.Phrygian, [1, 2, 2, 2, 1, 2, 2] },
        { Mode.Locrian, [1, 2, 2, 1, 2, 2, 2] }
    };

    private static readonly Dictionary<string, string> NoteMap = new()
    {
        { "A#", "ASharpBFlat" },
        { "Bb", "ASharpBFlat" },
        
        { "C#", "CSharpDFlat" },
        { "Db", "CSharpDFlat" },
        
        { "D#", "DSharpEFlat" },
        { "Eb", "DSharpEFlat" },
        
        { "F#", "FSharpGFlat" },
        { "Gb", "FSharpGFlat" },
        
        { "G#", "GSharpAFlat" },
        { "Ab", "GSharpAFlat" },
        
        
        { "a#", "ASharpBFlat" },
        { "bb", "ASharpBFlat" },
        
        { "c#", "CSharpDFlat" },
        { "db", "CSharpDFlat" },
        
        { "d#", "DSharpEFlat" },
        { "eb", "DSharpEFlat" },
        
        { "f#", "FSharpGFlat" },
        { "gb", "FSharpGFlat" },
        
        { "g#", "GSharpAFlat" },
        { "ab", "GSharpAFlat" }
    };
    // scale initiation
    public static void InitiateScale()
    {
        Console.WriteLine();
        Console.WriteLine("Input a key and mode (e.g. \"C Major\")");
        
        Console.ResetColor();
        string input = Console.ReadLine();

        // split the input
        if (input.Split().Length != 2)
        {
            throw new ArgumentException("invalid format, please use the format \"C Major\"");
        }
        
        string inputKey = input.Split()[0].Trim();
        string inputMode = input.Split()[1].Trim();

        Note key = ParseNote(inputKey);
        Mode mode = ParseMode(inputMode);
        
        // generate the scale
        GenerateScale(key, mode);
    }
    
    // input validation && parsing
    private static Note ParseNote(string note)
    {
        if (NoteMap.TryGetValue(note, out string enumName))
        {
            // Try to parse the mapped enum name
            if (Enum.TryParse(enumName, true, out Note parsedNote))
            {
                // Check if the parsed value is a defined enum value
                if (Enum.IsDefined(typeof(Note), parsedNote))
                {
                    return parsedNote;
                }
            }
        }
        else
        {
            // Try to parse the input directly if it's not an accidental
            if (Enum.TryParse(note, true, out Note parsedNote))
            {
                // Check if the parsed value is a defined enum value
                if (Enum.IsDefined(typeof(Note), parsedNote))
                {
                    return parsedNote;
                }
            }
        }

        throw new ArgumentException($"\"{note}\" is not a valid key");
    }
    
    private static Mode ParseMode(string mode)
    {
            if (Enum.TryParse(mode, true, out Mode parsedMode))
            {
                if (Enum.IsDefined(typeof(Mode), parsedMode))
                {
                    return parsedMode;
                }
            }
            
            throw new ArgumentException($"\"{mode}\" is not a valid mode");
    }
    
    // scale generation
    private static void GenerateScale(Note key, Mode mode)
    {
        NoteScale.Clear(); // prepares scale list
        
        if (ModeIntervals.TryGetValue(mode, out int[] intervals))
        {
            Note currentNote = key;
            
            for (int i = 0; i < intervals.Length; i++)
            {
                NoteScale.Add(currentNote);
                currentNote = (Note) (((int)currentNote + intervals[i]) % Enum.GetValues(typeof(Note)).Length); // courtesy of chatGPT, ain't no way I'm thinking of this on my own
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"the {key} {mode} scale: ");
            
            int count = 0;
            
            foreach (var note in NoteScale)
            {
                Console.Write($"{note} ");
            }
            
            Console.WriteLine();
        }
        else
        {
            throw new ArgumentException($"mode \"{mode}\" not found");
        }
    }
}