using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Speech.Synthesis;

namespace RecipeApp
{
    /// <summary>
    /// This class is used to handle the input and output of the console.
    /// It contains additional methods to System.Console class.
    /// </summary>
    public static class ConsoleIO
    {
        // Automatic Properties
        public static bool ErrorSoundEnabled { get; set; }

        /// <summary>
        /// This method prints out text to the console with color.
        /// </summary>
        /// <param name="s">The actual text to print.</param>
        /// <param name="color">The color of the text</param>
        /// <param name="bNewLine">Indicates if a new line character must be printed as well.</param>
        /// -------------------------------------------------------------------------
        public static void PrintColorText(string s, ConsoleColor color, bool bNewLine)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            if (bNewLine)
                Console.WriteLine(s);
            else
                Console.Write(s);

            Console.ForegroundColor = prevColor;
        }

        /// <summary>
        /// Reads from the console and checks if the input is valid based on the input data type.
        /// </summary>
        /// <param name="displayText">The message that will be displayed to the user.</param>
        /// <param name="color">The text color of the user input.</param>
        /// <param name="inputDataType">The data type to restrict the input value.</param>
        /// <param name="bNewLine">Indicates if a new line character must be printed after the display text.</param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        public static string GetValidInput(string displayText, ConsoleColor color, InputDataType inputDataType, bool bNewLine)
        {
            string result = "";

            // Indefinite loop. This loop will only stop once valid data has been received.
            while (true)
            {
                try
                {
                    // Display the message to the user.
                    if (bNewLine)
                        Console.WriteLine(displayText);
                    else
                        Console.Write(displayText);

                    // Obtain user input.
                    string input = ConsoleIO.GetInput(color);


                    // InputDataType: Letters
                    if (inputDataType == InputDataType.Letters)
                    {
                        // These are all the characters that are allowed as input.
                        const string VALID_SYMBOLS = "abcdefghijklmnopqrstuvwxyz ";
                        string lowercaseInput = input.ToLower();
                        bool errorFound = false;

                        for (int i = 0; i < input.Length; i++)
                        {
                            // If a character is not part of the VALID_SYMBOLS, then force an error.
                            if (!VALID_SYMBOLS.Contains(lowercaseInput[i]))
                                errorFound = true;
                        }

                        // If the user provided nothing as input, then force an error.
                        if (input.Length == 0)
                            errorFound = true;

                        if (errorFound)
                            throw new Exception("Invalid input provided. Expected: Alphabetical Letters");
                        else
                        {
                            result = input;
                            break; // Exit the indefinite loop.
                        }
                    }


                    // InputDataType: Numbers
                    if (inputDataType == InputDataType.Numbers)
                    {
                        // These are all the characters that are allowed as input.
                        const string VALID_SYMBOLS = "-0123456789";
                        bool errorFound = false;

                        for (int i = 0; i < input.Length; i++)
                        {
                            // If a character is not part of the VALID_SYMBOLS, then force an error.
                            if (!VALID_SYMBOLS.Contains(input[i]))
                                errorFound = true;
                        }

                        // If the user provided nothing as input, then force an error.
                        if (input.Length == 0)
                            errorFound = true;

                        if (errorFound)
                            throw new Exception("Invalid input provided. Expected: Integers");
                        else
                        {
                            result = input;
                            break; // Exit the indefinite loop.
                        }
                    }


                    // InputDataType: LettersAndNumbers
                    if (inputDataType == InputDataType.LettersAndNumbers)
                    {
                        // These are all the characters that are allowed as input.
                        const string VALID_SYMBOLS = "-0123456789abcdefghijklmnopqrstuvwxyz ";
                        string lowercaseInput = input.ToLower();
                        bool errorFound = false;

                        for (int i = 0; i < input.Length; i++)
                        {
                            // If a character is not part of the VALID_SYMBOLS, then force an error.
                            if (!VALID_SYMBOLS.Contains(lowercaseInput[i]))
                                errorFound = true;
                        }

                        // If the user provided nothing as input, then force an error.
                        if (input.Length == 0)
                            errorFound = true;

                        if (errorFound)
                            throw new Exception("Invalid input provided. Expected: Alphabetical Letters and Integers.");
                        else
                        {
                            result = input;
                            break; // Exit the indefinite loop.
                        }
                    }


                    // InputDataType: LettersAndPositiveNumbers
                    if (inputDataType == InputDataType.LettersAndPositiveNumbers)
                    {
                        // These are all the characters that are allowed as input.
                        const string VALID_SYMBOLS = "0123456789abcdefghijklmnopqrstuvwxyz ";
                        string lowercaseInput = input.ToLower();
                        bool errorFound = false;

                        for (int i = 0; i < input.Length; i++)
                        {
                            // If a character is not part of the VALID_SYMBOLS, then force an error.
                            if (!VALID_SYMBOLS.Contains(lowercaseInput[i]))
                                errorFound = true;
                        }

                        // If the user provided nothing as input, then force an error.
                        if (input.Length == 0)
                            errorFound = true;

                        if (errorFound)
                            throw new Exception("Invalid input provided. Expected: Alphabetical Letters and Positive Whole Numbers.");
                        else
                        {
                            result = input;
                            break; // Exit the indefinite loop.
                        }
                    }


                    // InputDataType: PositiveNumbers
                    if (inputDataType == InputDataType.PositiveNumbers)
                    {
                        // These are all the characters that are allowed as input.
                        const string VALID_SYMBOLS = "0123456789";
                        bool errorFound = false;

                        for (int i = 0; i < input.Length; i++)
                        {
                            // If a character is not part of the VALID_SYMBOLS, then force an error.
                            if (!VALID_SYMBOLS.Contains(input[i]))
                                errorFound = true;
                        }

                        // If the user provided nothing as input, then force an error.
                        if (input.Length == 0)
                            errorFound = true;

                        if (errorFound)
                            throw new Exception("Invalid input provided. Expected: Positive Whole Numbers");
                        else
                        {
                            result = input;
                            break; // Exit the indefinite loop.
                        }
                    }


                    // InputDataType: PositiveDecimals
                    if (inputDataType == InputDataType.PositiveDecimals)
                    {
                        // These are all the characters that are allowed as input.
                        const string VALID_SYMBOLS = "0123456789,.";
                        bool errorFound = false;

                        for (int i = 0; i < input.Length; i++)
                        {
                            // If a character is not part of the VALID_SYMBOLS, then force an error.
                            if (!VALID_SYMBOLS.Contains(input[i]))
                                errorFound = true;
                        }

                        // If the user provided nothing as input, then force an error.
                        if (input.Length == 0)
                            errorFound = true;

                        if (errorFound)
                            throw new Exception("Invalid input provided. Expected: Positive Decimal Numbers");
                        else
                        {
                            result = input;
                            break; // Exit the indefinite loop.
                        }
                    }
                }
                catch(Exception e)
                {
                    // Catch the exception that was thrown in the try-block and print
                    // the exception to the console.
                    PrintColorText("Error: ", ConsoleColor.Red, false);
                    Console.WriteLine(e.Message);

                    if (ErrorSoundEnabled)
                        Console.Beep(500, 700);
                }
            }

            return result;
        }

        /// <summary>
        /// This method gets the input from the user.
        /// </summary>
        /// <param name="color">The text color of the user's input.</param>
        /// <returns></returns>
        public static string GetInput(ConsoleColor color)
        {
            // Obtain the previous text color of the console.
            ConsoleColor prevColor = Console.ForegroundColor;
            // Assign the new text color to the console.
            Console.ForegroundColor = color;

            string input = Console.ReadLine();
            // Assign the previous text color to the console.
            Console.ForegroundColor = prevColor;

            return input;
        }

        /// <summary>
        /// This method gets the input from the user.
        /// </summary>
        /// <param name="color">The text color of the user's input.</param>
        /// <returns></returns>
        public static ConsoleKeyInfo GetKeyInput(ConsoleColor color)
        {
            // Obtain the previous text color of the console.
            ConsoleColor prevColor = Console.ForegroundColor;
            // Assign the new text color to the console.
            Console.ForegroundColor = color;

            ConsoleKeyInfo input = Console.ReadKey();
            // Assign the previous text color to the console.
            Console.ForegroundColor = prevColor;

            return input;
        }
    }
}

