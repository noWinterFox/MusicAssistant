namespace MusicAssistant;

// comments by chatGPT

public abstract class Scale
{
    // List to store the notes of the scale
    private static readonly List<Note> NoteScale = [];

    // Intervals for major and minor scales (whole and half steps)
    private static readonly int[] MajorInterval = [2, 2, 1, 2, 2, 2, 1];
    private static readonly int[] MinorInterval = [2, 1, 2, 2, 1, 2, 2];

    // Diatonic interval patterns for major and minor scales
    private static readonly int[] MajorDiatonics = [1, 2, 2, 1, 1, 2, 3]; // Major diatonic chords: 1 - Major, 2 - Minor, 3 - Diminished
    private static readonly int[] MinorDiatonics = [2, 3, 1, 2, 2, 1, 1];

    // Mode intervals for different musical modes
    private static readonly Dictionary<Mode, int[]?> ModeIntervals = new()
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

    // Mapping for accidentals (sharp/flat) to standardized note names
    private static readonly Dictionary<string, string?> NoteMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "A#", "ASharpBFlat" }, { "Bb", "ASharpBFlat" },
        { "C#", "CSharpDFlat" }, { "Db", "CSharpDFlat" },
        { "D#", "DSharpEFlat" }, { "Eb", "DSharpEFlat" },
        { "F#", "FSharpGFlat" }, { "Gb", "FSharpGFlat" },
        { "G#", "GSharpAFlat" }, { "Ab", "GSharpAFlat" }
    };

    // Diatonic chords for each mode
    public static readonly Dictionary<Mode, int[]> DiatonicChords = new() // not implemented yet
    {
        { Mode.Lydian, [1, 1, 2, 3, 1, 2, 2] },
        { Mode.Ionian, MajorDiatonics },
        { Mode.Major, MajorDiatonics },
        { Mode.Mixolydian, [1, 2, 3, 1, 2, 2, 1] },
        { Mode.Dorian, [2, 2, 1, 1, 2, 3, 1] },
        { Mode.Aeolian, MinorDiatonics },
        { Mode.Minor, MinorDiatonics },
        { Mode.Phrygian, [2, 1, 1, 2, 3, 1, 2] },
        { Mode.Locrian, [3, 1, 2, 2, 1, 1, 2] }
    };

    // Method to initiate the scale generation based on user input
    public static void InitiateScale()
    {
        // Prompt user for key and mode input
        Console.WriteLine();
        Console.WriteLine("Input a key and mode (e.g. \"C Major\")");

        Console.ResetColor();
        string input = Console.ReadLine()!;

        // Split the input into key and mode
        if (input.Split().Length != 2)
        {
            throw new ArgumentException("Invalid format, please use the format \"C Major\"");
        }

        string inputKey = input.Split()[0].Trim();
        string inputMode = input.Split()[1].Trim();

        // Parse the key and mode
        Note key = ParseNote(inputKey);
        Mode mode = ParseMode(inputMode);

        // Generate the scale for the given key and mode
        GenerateScale(key, mode);
    }

    // Parse the note input and validate against known notes (case-insensitive)
    private static Note ParseNote(string note)
    {
        // Check if the note exists in the NoteMap and parse accordingly
        if (NoteMap.TryGetValue(note, out string? enumName))
        {
            if (Enum.TryParse(enumName, true, out Note parsedNote) && Enum.IsDefined(typeof(Note), parsedNote))
            {
                return parsedNote;
            }
        }
        else
        {
            // Try to parse the note directly if not an accidental
            if (Enum.TryParse(note, true, out Note parsedNote) && Enum.IsDefined(typeof(Note), parsedNote))
            {
                return parsedNote;
            }
        }

        throw new ArgumentException($"\"{note}\" is not a valid key");
    }

    // Parse the mode input and validate against known modes
    private static Mode ParseMode(string mode)
    {
        if (Enum.TryParse(mode, true, out Mode parsedMode) && Enum.IsDefined(typeof(Mode), parsedMode))
        {
            return parsedMode;
        }

        throw new ArgumentException($"\"{mode}\" is not a valid mode");
    }

    // Generate the scale based on the given key and mode
    private static void GenerateScale(Note key, Mode mode)
    {
        // Clear any previous scale
        NoteScale.Clear();

        // Check if the mode intervals are defined
        if (ModeIntervals.TryGetValue(mode, out int[]? intervals))
        {
            Note currentNote = key;

            // Generate the scale by iterating through the intervals
            foreach (var t in intervals!)
            {
                NoteScale.Add(currentNote);
                currentNote = (Note)(((int)currentNote + t) % Enum.GetValues(typeof(Note)).Length); // Wrap around the notes
            }

            // Output the scale
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"The {key} {mode} scale: ");

            // Print the notes of the scale
            foreach (var note in NoteScale)
            {
                Console.Write($"{note} ");
            }

            Console.WriteLine();
        }
        else
        {
            throw new ArgumentException($"Mode \"{mode}\" not found");
        }
    }
}