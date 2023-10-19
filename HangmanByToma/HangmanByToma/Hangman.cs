using System;
class Program
{
    static string Underscore = "_";
    const int MaxAllowedIncorrectGuesses = 6;

    static void Main()
    {
        Console.CursorVisible = false;
        while (true)
        {
            string[] words = ReadWordsFromFile();
            string word = GetRandomWord(words);
            string wordToGuess = new string('_', word.Length);
            string guess = new string('_', word.Length);

            int incorrectGuessCount = 0;
            List<char> playerUsedLetters = new List<char>();

            DrawCurrentGameState(false, incorrectGuessCount, wordToGuess, playerUsedLetters, guess);
            PlayGame(word, wordToGuess, incorrectGuessCount, playerUsedLetters, guess);

            Console.Write("If you want to play again, press [Enter]. Else, type 'quit': ");
            string playerChoice = Console.ReadLine();
            if (playerChoice == "quit")
            {
                Console.Clear();
                Console.WriteLine("Thank you for playing! Hangman was closed.");
                break;
            }

            Console.Clear();
        }
    }

    static string[] ReadWordsFromFile()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
        const string WordFileName = "words.txt";
        string path = Path.Combine(projectDirectory, WordFileName);
        string[] words = File.ReadAllLines(path);
        return words;
    }

    static string GetRandomWord(string[] words)
    {
        Random random = new Random();
        string word = words[random.Next(words.Length)].ToLower();
        return word;
    }

    static void DrawCurrentGameState(bool inputIsInvalid, int incorrectGuess, string guessedWord, List<char> playerUsedLetters, string guess)
    {
        Console.Clear();
        Console.WriteLine(wrongGuessesFrames[incorrectGuess]);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Guess: ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{guess}");

        int wordLength = guessedWord.Length;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nYou have to guess {wordLength} symbols.");
        Console.WriteLine($"The following letters are used: {string.Join(", ", playerUsedLetters)}");
        Console.ForegroundColor = ConsoleColor.White;

        if (inputIsInvalid)
        {
            Console.WriteLine("You should type only a single character!");
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Your symbol: ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void PlayGame(string word, string wordToGuess, int incorrectGuessCount, List<char> playerUsedLetters, string guess)
    {
        while (true)
        {
            string playerInput = Console.ReadLine().ToLower();
            if (playerInput.Length != 1)
            {
                DrawCurrentGameState(true, incorrectGuessCount, wordToGuess, playerUsedLetters, guess);
                continue;
            }

            char playerLetter = playerInput[0];
            playerUsedLetters.Add(playerLetter);

            bool playerLetterIsContained = CheckIfSymbolIsContained(word, playerLetter);

            if (playerLetterIsContained)
            {
                wordToGuess = AddLetterToGuessWord(word, playerLetter, wordToGuess);
                guess = UpdateGuessString(word, guess, playerLetter);
            }
            else
            {
                incorrectGuessCount++;
            }

            DrawCurrentGameState(false, incorrectGuessCount, wordToGuess, playerUsedLetters, guess);

            if (CheckIfPlayerWins(wordToGuess))
            {
                Console.Clear();
                Console.WriteLine(WinScreenText2);
                Console.WriteLine($"The word you guessed is [{word}].");
                break;
            }

            if (CheckIfPlayerLoses(incorrectGuessCount))
            {
                Console.SetCursorPosition(0, 0);
                DrawDeathAnimation(deathAnimationFrames);
                Console.Clear();
                Console.WriteLine(LossScreenText2);
                Console.WriteLine($"The exact word is [{word}].");
                break;
            }
        }
    }

    static string UpdateGuessString(string word, string guess, char playerLetter)
    {
        char[] guessArray = guess.ToCharArray();

        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] == playerLetter)
            {
                guessArray[i] = playerLetter;
            }
        }

        return new string(guessArray);
    }

    static bool CheckIfSymbolIsContained(string word, char playerLetter)
    {
        return word.Contains(playerLetter);
    }

    static string AddLetterToGuessWord(string word, char playerLetter, string wordToGuess)
    {
        char[] wordToGuessCharArr = wordToGuess.ToCharArray();

        for (int i = 0; i < word.Length; i++)
        {
            if (playerLetter == word[i])
            {
                wordToGuessCharArr[i] = playerLetter;
            }
        }

        return new string(wordToGuessCharArr);
    }

    static bool CheckIfPlayerWins(string wordToGuess)
    {
        return !wordToGuess.Contains(Underscore);
    }

    static bool CheckIfPlayerLoses(int incorrectGuessCount)
    {
        return incorrectGuessCount == MaxAllowedIncorrectGuesses;
    }


    static string WinScreenText2 = @"
     
┌───────────────────────────┐
│                           │
│  WW       WW ** NN   N    │
│  WW       WW ii NNN  N    │
│   WW  WW WW  ii N NN N    │
│    WWWWWWW   ii N  NNN    │
│     WW  W    ii N   NN    │
│                           │
│        Good job!          │
│  You guessed the word!    │
└───────────────────────────┘
";

    static string LossScreenText2 = @"
    ┌────────────────────────────────────┐
    │  LLL          OOOO    SSSS   SSSS  │
    │  LLL         OO  OO  SS  SS SS  SS │
    │  LLL        OO    OO SS     SS     │
    │  LLL        OO    OO  SSSS   SSSS  │
    │  LLL        OO    OO     SS     SS │
    │  LLLLLLLLLL  OO  OO  SS  SS SS  SS │
    │   LLLLLLLLL   OOOO    SSSS   SSSS  │
    │                                    │ 
    │        You were so close.          │
    │ Next time you will guess the word! │
    └────────────────────────────────────┘
";

    static string[] deathAnimationFrames = new string[]
    {
                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"      O   ║   " +'\n' +
                @"     /|\  ║   " +'\n' +
                @"     / \  ║   " +'\n' +
                @"     ███  ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"      O   ║   " +'\n' +
                @"     /|\  ║   " +'\n' +
                @"     / \  ║   " +'\n' +
                @"          ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"      o>  ║   " +'\n' +
                @"     /|   ║   " +'\n' +
                @"      >\  ║   " +'\n' +
                @"          ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"      O   ║   " +'\n' +
                @"     /|\  ║   " +'\n' +
                @"     / \  ║   " +'\n' +
                @"          ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"     <o   ║   " +'\n' +
                @"      |\  ║   " +'\n' +
                @"     /<   ║   " +'\n' +
                @"          ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"      O   ║   " +'\n' +
                @"     /|\  ║   " +'\n' +
                @"     / \  ║   " +'\n' +
                @"          ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"      o>  ║   " +'\n' +
                @"     /|   ║   " +'\n' +
                @"      >\  ║   " +'\n' +
                @"          ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"      o>  ║   " +'\n' +
                @"     /|   ║   " +'\n' +
                @"      >\  ║   " +'\n' +
                @"          ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " +'\n' +
                @"      |   ║   " +'\n' +
                @"      O   ║   " +'\n' +
                @"     /|\  ║   " +'\n' +
                @"     / \  ║   " +'\n' +
                @"          ║   " +'\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"     <o   ║   " + '\n' +
                @"      |\  ║   " + '\n' +
                @"     /<   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"     <o   ║   " + '\n' +
                @"      |\  ║   " + '\n' +
                @"     /<   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"     <o   ║   " + '\n' +
                @"      |\  ║   " + '\n' +
                @"     /<   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"     /|\  ║   " + '\n' +
                @"     / \  ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      o   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      o   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      o   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      o   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      o   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      o   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      |   ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      /   ║   " + '\n' +
                @"      \   ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      |__ ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"      .   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      \__ ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @"     ____ ║   " + '\n' +
                @"    ══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @"      .   ║   " + '\n' +
                @"    __    ║   " + '\n' +
                @"   /══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"      .   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"    _ '   ║   " + '\n' +
                @"  _/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @"    _     ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @"      .   ║   " + '\n' +
                @"          ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"      .   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @"      _   ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @"      .   ║   " + '\n' +
                @"          ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"      .   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @"      _   ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      .   ║   " + '\n' +
                @"          ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗   " + '\n' +
                @"      |   ║   " + '\n' +
                @"      O   ║   " + '\n' +
                @"          ║   " + '\n' +
                @"          ║   " + '\n' +
                @"      '   ║   " + '\n' +
                @" __/══════╩═══",

                @"      ╔═══╗ " + '\n' +
                @"      |   ║ " + '\n' +
                @"      O   ║ " + '\n' +
                @"          ║ " + '\n' +
                @"          ║ " + '\n' +
                @"        _ ║ " + '\n' +
                @" __/══════╩═══"
    };

    static string[] wrongGuessesFrames = new string[]
    {
    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"       \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══"
    };

    static void DrawDeathAnimation(string[] deathAnimation)
    {
        Console.SetCursorPosition(0, 0);
        foreach (string frame in deathAnimation)
        {
            Console.Clear();
            Console.WriteLine(frame);
            Thread.Sleep(200);
            Console.SetCursorPosition(0, 0);
        }
    }
}
