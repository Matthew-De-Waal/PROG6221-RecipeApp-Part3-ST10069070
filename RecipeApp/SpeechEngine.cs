using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    /// <summary>
    /// This class allows for Text-to-Speech with the program.
    /// </summary>
    public static class SpeechEngine
    {
        private static SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        // Global constants
        public const string SPEECH_INTRODUCTION = "Welcome to RecipeApp.";
        public const string SPEECH_LAUNCH_MENU = "Press M to launch the menu.";
        public const string SPEECH_MENU = "Press A to add a new recipe. Press B to scale a recipe's ingredients. Press C to reset a recipe's scale. Press D to view a recipe. Press E to delete a recipe.";
        public const string SPEECH_OPERATION_NEW_RECIPE = "Operation New Recipe.";
        public const string SPEECH_RECIPE_NAME = "Please provide a name for the recipe.";
        public const string SPEECH_RECIPE_INGREDIENTS = "Please provide the number of ingredients for the recipe.";
        public const string SPEECH_INGREDIENT_NAME = "Please provide the name of the ingredient.";
        public const string SPEECH_INGREDIENT_QUANTITY = "Please provide the ingredient quantity.";
        public const string SPEECH_MEASUREMENT_UNIT = "Please provide a unit measurement.";
        public const string SPEECH_PROVIDE_CALORIES = "Please provide the calories of the ingredient.";
        public const string SPEECH_PROVIDE_FOOD_CATEGORY = "Please provide the food category of the ingredient.";
        public const string SPEECH_PROVIDE_STEPS = "Please provide the number of steps.";
        public const string SPEECH_MAX_CALORIES_REACHED = "The maximum calory limit has been reached.";
        public const string SPEECH_OPERATION_SCALE_RECIPE = "Operation Scale Recipe.";
        public const string SPEECH_CHOOSE_SCALE_FACTOR = "Choose a scale factor for the recipe.";
        public const string SPEECH_OPERATION_RESET_RECIPE_SCALE = "Operation Reset Recipe Scale.";
        public const string SPEECH_RESET_RECIPE_SCALE_QUESTION = "Are you sure you want to reset the recipe scale? Enter yes or no.";
        public const string SPEECH_OPERATION_VIEW_RECIPE = "Operation View Recipe.";
        public const string SPEECH_OPERATION_DELETE_RECIPE = "Operation Delete Recipe.";
        public const string SPEECH_DELETE_RECIPE_QUESTION = "Are you sure you want to delete the recipe? Enter yes or no.";
        public const string SPEECH_PROVIDE_NAME_OR_INDEX = "Enter the name or index value of the recipe.";
        public const string SPEECH_PROVIDE_DESCRIPTION = "Please provide the description of the step.";

        /// <summary>
        /// Initializes the synthesizer.
        /// </summary>
        /// -------------------------------------------------------------------------
        public static void InitSynthesizer()
        {
            synthesizer.SelectVoice("Microsoft David Desktop");
        }

        /// <summary>
        /// Performs the speech from the string 's'.
        /// </summary>
        /// <param name="s">The actual text that will be spoken.</param>
        /// -------------------------------------------------------------------------
        public static void Speak(string s)
        {
            // Cancel previous speeches.
            synthesizer.SpeakAsyncCancelAll();
            // Speak asynchronously.
            synthesizer.SpeakAsync(s);
        }

        /// <summary>
        /// Performs the speech from the string 's'
        /// </summary>
        /// <param name="s">The actual text that will be spoken.</param>
        /// <param name="b">Indicates if the operation must happen asynchronously.</param>
        /// -------------------------------------------------------------------------
        public static void Speak(string s, bool b)
        {
            // Cancel previous speeches.
            synthesizer.SpeakAsyncCancelAll();

            if (b)
                // Speak asynchronously.
                synthesizer.SpeakAsync(s);
            else
                // Speak synchronously.
                synthesizer.Speak(s);
        }
    }
}
