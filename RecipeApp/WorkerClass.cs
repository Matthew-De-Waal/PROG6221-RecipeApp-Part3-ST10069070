using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace RecipeApp
{
    /// <summary>
    /// The main class that contains the core functionality of the program.
    /// </summary>
    public class WorkerClass
    {
        // Data fields
        private List<Recipe> recipeList = new List<Recipe>();
        private bool blueScreenEnabled = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// -------------------------------------------------------------------------
        public WorkerClass() { }

        /// <summary>
        /// This method displays all the recipes to the user.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void DisplayRecipes()
        {
            Console.WriteLine("List of recipes:");

            // Iterate through the recipeList to display each recipe name.
            for (int i = 0; i < recipeList.Count; i++)
                Console.WriteLine($"{i}: {recipeList[i].Name}");
        }

        /// <summary>
        /// Get the selected recipe index from the user.
        /// </summary>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        private int GetSelectedRecipe()
        {
            // Speak aloud using the SpeechEngine.
            Speak(SpeechEngine.SPEECH_PROVIDE_NAME_OR_INDEX, blueScreenEnabled);

            string selection = ConsoleIO.GetValidInput("Enter the recipe name or index value: ", ConsoleColor.Yellow, InputDataType.LettersAndNumbers, false);
            int recipeIndex;

            // Check if the string 'selection' cannot be converted to an integer.
            if (!int.TryParse(selection, out recipeIndex))
            {
                // Find the recipe and get its index value.
                for (int i = 0; i < recipeList.Count; i++)
                {
                    if (recipeList[i].Name == selection)
                    {
                        recipeIndex = i;
                        break;
                    }
                }
            }

            return recipeIndex;
        }

        /// <summary>
        /// Checks if a recipe exists already.
        /// </summary>
        /// <param name="recipeName"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        private bool RecipeExists(string recipeName)
        {
            bool recipeExists = false;

            for(int i = 0; i < recipeList.Count; i++)
            {
                if (recipeList[i].Name == recipeName)
                {
                    recipeExists = true;
                    break;
                }
            }

            return recipeExists;
        }

        /// <summary>
        /// This method sorts the recipeList in ascending order.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void SortRecipes()
        {
            Recipe[] recipes = SortAlgorithm.BubbleSortAscending(recipeList.ToArray());
            recipeList.Clear();
            recipeList.AddRange(recipes);
        }

        /// <summary>
        /// Displays all the unit of measurements that are available.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void DisplayUnitOfMeasurement()
        {
            // Display the unit of measurements to help the user.
            Console.WriteLine("\t------------------------------");
            Console.WriteLine("\tList of measurements:");

            // Display the Millitres option.
            ConsoleIO.PrintColorText("\tML", ConsoleColor.DarkYellow, false);
            Console.WriteLine(" - Millilitres");

            // Display the Litres option.
            ConsoleIO.PrintColorText("\tL", ConsoleColor.DarkYellow, false);
            Console.WriteLine(" - Litres");

            // Display the Grams option.
            ConsoleIO.PrintColorText("\tG", ConsoleColor.DarkYellow, false);
            Console.WriteLine(" - Grams");

            // Display the Kilograms option.
            ConsoleIO.PrintColorText("\tKG", ConsoleColor.DarkYellow, false);
            Console.WriteLine(" - Kilograms");

            // Display the Teaspoon option.
            ConsoleIO.PrintColorText("\tt", ConsoleColor.DarkYellow, false);
            Console.WriteLine(" - Teaspoon");

            // Display the Tablespoon option.
            ConsoleIO.PrintColorText("\tT", ConsoleColor.DarkYellow, false);
            Console.WriteLine(" - Tablespoon");
            Console.WriteLine();
        }

        /// <summary>
        /// Displays all the food categories that are available.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void DisplayFoodCategories()
        {
            // Display the food categories to the user.
            Console.WriteLine("\t------------------------------");
            Console.WriteLine("\tList of categories:");

            // Display the Vegetables option.
            ConsoleIO.PrintColorText("\t1", ConsoleColor.DarkYellow, false);
            Console.WriteLine(": Vegetables");

            // Display the Fruits option.
            ConsoleIO.PrintColorText("\t2", ConsoleColor.DarkYellow, false);
            Console.WriteLine(": Fruits");

            // Display the Grains option.
            ConsoleIO.PrintColorText("\t3", ConsoleColor.DarkYellow, false);
            Console.WriteLine(": Grains");

            // Display the Protein option.
            ConsoleIO.PrintColorText("\t4", ConsoleColor.DarkYellow, false);
            Console.WriteLine(": Protein");

            // Display the Dairy option.
            ConsoleIO.PrintColorText("\t5", ConsoleColor.DarkYellow, false);
            Console.WriteLine(": Dairy");

            // Display the Oil and Solid Fats option.
            ConsoleIO.PrintColorText("\t6", ConsoleColor.DarkYellow, false);
            Console.WriteLine(": Oil and Solid Fats");

            // Display the Added Sugars option.
            ConsoleIO.PrintColorText("\t7", ConsoleColor.DarkYellow, false);
            Console.WriteLine(": Added Sugars");

            // Display the Beverages option.
            ConsoleIO.PrintColorText("\t8", ConsoleColor.DarkYellow, false);
            Console.WriteLine(": Beverages");
            Console.WriteLine();
        }

        /// <summary>
        /// Converts a string to a UnitMeasurement.
        /// </summary>
        /// <param name="sMeasurement"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        private UnitMeasurement GetUnitMeasurement(string sMeasurement, ref bool success)
        {
            UnitMeasurement measurement = default;

            // Convert the measurement string to a UnitMeasurement enumeration.
            switch (sMeasurement)
            {
                case "ML":
                    // Assign the measurement to millilitres.
                    measurement = UnitMeasurement.Millilitres;
                    success = true;
                    break;

                case "L":
                    // Assign the measurement to litres.
                    measurement = UnitMeasurement.Litres;
                    success = true;
                    break;

                case "G":
                    // Assign the measurement to grams.
                    measurement = UnitMeasurement.Grams;
                    success = true;
                    break;

                case "KG":
                    // Assign the measurement to kilograms.
                    measurement = UnitMeasurement.Kilograms;
                    success = true;
                    break;

                case "t":
                    // Assign the measuremet to teapoons.
                    measurement = UnitMeasurement.Teaspoon;
                    success = true;
                    break;

                case "T":
                    // Assign the measurement to tablespoons.
                    measurement = UnitMeasurement.Tablespoon;
                    success = true;
                    break;

                default:
                    success = false;
                    break;
            }

            return measurement;
        }

        /// <summary>
        /// Speaks text aloud asynchronously, using the SpeechEngine class.
        /// </summary>
        /// <param name="s">The text to read aloud.</param>
        /// <param name="b">Indicates if the operation must happen.</param>
        /// -------------------------------------------------------------------------
        private void Speak(string s, bool b)
        {
            if (b) SpeechEngine.Speak(s);
        }

        /// <summary>
        /// Speaks text aloud synchronously, using the SpeechEngine class.
        /// </summary>
        /// <param name="s">The text to read aloud.</param>
        /// <param name="b">Indicates if the operation must happen.</param>
        /// -------------------------------------------------------------------------
        private void SpeakAndWait(string s, bool b)
        {
            if (b) SpeechEngine.Speak(s, false);
        }

        /// <summary>
        /// Uses Console.Beep method to play a success sound.
        /// </summary>
        /// <param name="b">Indicates if the sound must be played.</param>
        /// -------------------------------------------------------------------------
        public void PlaySuccessSound(bool b)
        {
            if (b)
            {
                Console.Beep(200, 500);
                Console.Beep(300, 500);
            }
        }

        /// <summary>
        /// Uses Console.Beep method to play an error sound.
        /// </summary>
        /// <param name="b">Indicates if the sound must be played.</param>
        /// -------------------------------------------------------------------------
        public void PlayErrorSound(bool b)
        {
            if(b) Console.Beep(500, 1000);
        }

        /// <summary>
        /// This is the method that runs the worker class.
        /// </summary>
        /// -------------------------------------------------------------------------
        public void Run()
        {
            // Initialization
            ConsoleIO.ErrorSoundEnabled = blueScreenEnabled;
            SpeechEngine.InitSynthesizer();

            // Assign the console title.
            Console.Title = "RecipeApp";

            // Print out the author details and instructions.
            ConsoleIO.PrintColorText("Welcome to RecipeApp.", ConsoleColor.DarkCyan, true);
            Console.WriteLine("Made by: Matthew De Waal");
            Console.WriteLine("Student Number: ST10069070");
            Console.WriteLine("----------------------------------------");

            // Speak aloud using the SpeechEngine.
            SpeakAndWait(SpeechEngine.SPEECH_INTRODUCTION, blueScreenEnabled);
            ProgramStartup();
        }

        /// <summary>
        /// Displays the instructions to launch the menu.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void ProgramStartup()
        {
            Console.WriteLine("\nPress M to launch the menu.");
            Console.WriteLine("Press any other key to exit the program.");

            // Speak aloud using the SpeechEngine.
            Speak(SpeechEngine.SPEECH_LAUNCH_MENU, blueScreenEnabled);

            // Obtain user input
            ConsoleKeyInfo keyInfo = ConsoleIO.GetKeyInput(ConsoleColor.Yellow);

            switch (keyInfo.Key)
            {
                case ConsoleKey.M:
                    // Launch the menu.
                    LaunchMenu();
                    break;
            }
        }

        /// <summary>
        /// Enables the blue screen for the console application.
        /// </summary>
        /// -------------------------------------------------------------------------
        public void EnableBlueScreen()
        {
            this.blueScreenEnabled = true;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
        }

        /// <summary>
        /// This method will launch the menu for the program.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void LaunchMenu()
        {
            // Display the menu.
            ConsoleIO.PrintColorText("\nMenu:", ConsoleColor.Green, true);
            Console.WriteLine("\tA - Add a new recipe.");
            Console.WriteLine("\tB - Scale a recipe's ingredients");
            Console.WriteLine("\tC - Reset recipe scale");
            Console.WriteLine("\tD - View a recipe");
            Console.WriteLine("\tE - Delete a recipe");
            Console.WriteLine("Press any other key to exit the program.");

            // Speak aloud using the SpeechEngine.
            Speak(SpeechEngine.SPEECH_MENU, blueScreenEnabled);

            ConsoleKeyInfo keyInfo = ConsoleIO.GetKeyInput(ConsoleColor.Yellow);
            bool exitApp = false;

            // Handle the input from the user.
            switch (keyInfo.Key)
            {
                case ConsoleKey.A:
                    // Navigate to the NewRecipe operation.
                    Operation_NewRecipe();
                    break;

                case ConsoleKey.B:
                    // Navigate to the ScaleRecipe operation.
                    Operation_ScaleRecipe();
                    break;

                case ConsoleKey.C:
                    // Navigate to the ResetRecipeScale operation.
                    Operation_ResetRecipeScale();
                    break;

                case ConsoleKey.D:
                    // Navigate to the ViewRecipe operation.
                    Operation_ViewRecipe();
                    break;

                case ConsoleKey.E:
                    // Navigate to the DeleteRecipe operation.
                    Operation_DeleteRecipe();
                    break;

                default:
                    // Exit the console application.
                    exitApp = true;
                    break;
            }

            if (!exitApp)
                ProgramStartup();
        }

        /// <summary>
        /// This method handles the operation of a new recipe.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void Operation_NewRecipe()
        {
            ConsoleIO.PrintColorText("\nOPERATION: Add New Recipe", ConsoleColor.Blue, true);
            Console.WriteLine("-----------------------------------------");

            // Speak aloud using the SpeechEngine.
            SpeakAndWait(SpeechEngine.SPEECH_OPERATION_NEW_RECIPE, blueScreenEnabled);
            Speak(SpeechEngine.SPEECH_RECIPE_NAME, blueScreenEnabled);

            Recipe recipe = new Recipe();
            // Obtain the recipe name from the user.
            recipe.Name = ConsoleIO.GetValidInput("Enter the name of the recipe: ", ConsoleColor.Yellow, InputDataType.LettersAndNumbers, false);

            if (!RecipeExists(recipe.Name))
            {
                // Speak aloud using the SpeechEngine.
                Speak(SpeechEngine.SPEECH_RECIPE_INGREDIENTS, blueScreenEnabled);
                // Obtain the number of ingredients.
                string sIngredients = ConsoleIO.GetValidInput("Enter the number of ingredients: ", ConsoleColor.Yellow, InputDataType.PositiveNumbers, false);
                int nIngredients = Convert.ToInt32(sIngredients);

                // Handles the process of obtaining the ingredients.
                Operation_NewRecipe_ObtainIngredients(recipe, nIngredients);
                // Handles the process of obtaining the steps.
                Operation_NewRecipe_ObtainSteps(recipe);

                recipe.MaxCalories = 300;
                recipe.MaximumCaloriesReached += Recipe_MaxCaloriesReached;
                // Perform data validation.
                recipe.Validate();

                recipeList.Add(recipe);
                // Sort the recipes in ascending order.
                SortRecipes();

                ConsoleIO.PrintColorText("\nSuccessfully added the new recipe.", ConsoleColor.DarkGreen, true);
                PlaySuccessSound(blueScreenEnabled);
            }
            else
            {
                ConsoleIO.PrintColorText("\nUnable to add the new recipe. The recipe already exists.", ConsoleColor.Red, true);
                PlayErrorSound(blueScreenEnabled);
            }
        }

        /// <summary>
        /// This method is part of 'Operation_NewRecipe'.
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="nIngredients"></param>
        /// -------------------------------------------------------------------------
        private void Operation_NewRecipe_ObtainIngredients(Recipe recipe, int nIngredients)
        {
            // Iterate until the user entered all the ingredients for the recipe.
            for (int i = 0; i < nIngredients; i++)
            {
                Console.WriteLine($"Ingredient {i}:");

                // Speak aloud using the SpeechEngine.
                Speak(SpeechEngine.SPEECH_INGREDIENT_NAME, blueScreenEnabled);
                // Get the ingredient name and quantity.
                string ingredientName = ConsoleIO.GetValidInput("\tName: ", ConsoleColor.Yellow, InputDataType.LettersAndNumbers, false);

                Speak(SpeechEngine.SPEECH_INGREDIENT_QUANTITY, blueScreenEnabled);
                string sQuantity = ConsoleIO.GetValidInput("\tQuantity: ", ConsoleColor.Yellow, InputDataType.Numbers, false);
                int ingredientQuantity = Convert.ToInt32(sQuantity);

                // Get the unit of measurement for the ingredient.
                DisplayUnitOfMeasurement();
                UnitMeasurement measurement = default;
                // This variable states whether the conversion of the UnitMeasurement to a string is successful.
                bool success = false;
                // Speak aloud using the SpeechEngine.
                Speak(SpeechEngine.SPEECH_MEASUREMENT_UNIT, blueScreenEnabled);

                // Loop until the conversion is successful.
                while (!success)
                {
                    // Obtain the unit of measurement as a string for the ingredient.
                    string sUnitOfMeasurement = ConsoleIO.GetValidInput("\tEnter the unit of measurement. Provide the abbreviation only: ", ConsoleColor.Yellow, InputDataType.Letters, false);
                    // Convert the unit measurement string to a UnitMeasurement enumeration and get the result of the conversion.
                    measurement = GetUnitMeasurement(sUnitOfMeasurement, ref success);

                    // If the conversion failed, proceed by this section.
                    if (!success)
                    {
                        // Display the error to the user.
                        ConsoleIO.PrintColorText("Error: ", ConsoleColor.Red, false);
                        Console.WriteLine("Invalid input provided. Expected a value from the measurement list.");
                        Console.Beep(500, 1000);
                    }
                }

                // Speak aloud using the SpeechEngine.
                Speak(SpeechEngine.SPEECH_PROVIDE_CALORIES, blueScreenEnabled);
                // Get the ingredient's calories.
                string sCalories = ConsoleIO.GetValidInput("\tEnter the number of calories: ", ConsoleColor.Yellow, InputDataType.Numbers, false);
                int calories = Convert.ToInt32(sCalories);

                // Speak aloud using the SpeechEngine.
                Speak(SpeechEngine.SPEECH_PROVIDE_FOOD_CATEGORY, blueScreenEnabled);
                // Get the food category of the ingredient.
                DisplayFoodCategories();
                string sFoodCategory = ConsoleIO.GetValidInput("\tEnter the food category. Provide the number only: ", ConsoleColor.Yellow, InputDataType.PositiveNumbers, false);
                int iFoodCategory = Convert.ToInt32(sFoodCategory);
                FoodCategory foodCategory = (FoodCategory)iFoodCategory;

                // Instantiate a new RecipeIngredient object and add it to the recipe.
                recipe.AddIngredient(new RecipeIngredient(ingredientName, ingredientQuantity, measurement, calories, foodCategory));
            }
        }

        /// <summary>
        /// This method is part of 'Operation_NewRecipe'.
        /// </summary>
        /// <param name="recipe"></param>
        /// -------------------------------------------------------------------------
        private void Operation_NewRecipe_ObtainSteps(Recipe recipe)
        {
            // Speak aloud using the SpeechEngine.
            Speak(SpeechEngine.SPEECH_PROVIDE_STEPS, blueScreenEnabled);
            // Obtain the steps from the user
            string strSteps = ConsoleIO.GetValidInput("Enter the number of steps: ", ConsoleColor.Yellow, InputDataType.PositiveNumbers, false);
            int nSteps = Convert.ToInt32(strSteps);

            // Loop until the user provided all the steps for the recipe.
            for (int i = 0; i < nSteps; i++)
            {
                // Speak aloud using the SpeechEngine.
                Speak(SpeechEngine.SPEECH_PROVIDE_DESCRIPTION, blueScreenEnabled);

                Console.WriteLine($"Step {i}:");
                Console.Write("\tDescription:\n\t");
                string description = ConsoleIO.GetInput(ConsoleColor.Yellow);

                // Instantiate a new RecipeStep object and add it to the recipe.
                recipe.AddInstruction(new RecipeStep(description));
            }
        }

        /// <summary>
        /// This method is associated with the MaximumCaloriesReached event.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="amount"></param>
        /// -------------------------------------------------------------------------
        private void Recipe_MaxCaloriesReached(Recipe r, int amount)
        {
            ConsoleIO.PrintColorText($"\nMaximum Calories Reached for the recipe '{r.Name}'", ConsoleColor.DarkYellow, true);
            Console.WriteLine($"Maximum Calory Limit: 300");
            Console.WriteLine($"Total number of calories: {amount}");

            // Speak aloud using the SpeechEngine.
            SpeakAndWait(SpeechEngine.SPEECH_MAX_CALORIES_REACHED, blueScreenEnabled);
        }

        /// <summary>
        /// This method handles the operation of scaling an existing recipe.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void Operation_ScaleRecipe()
        {
            if (recipeList.Count > 0)
            {
                ConsoleIO.PrintColorText("\nOPERATION: Scale a recipe's ingredients", ConsoleColor.Blue, true);

                // Speak aloud using the SpeechEngine.
                SpeakAndWait(SpeechEngine.SPEECH_OPERATION_SCALE_RECIPE, blueScreenEnabled);

                // Display all the recipes to the user.
                DisplayRecipes();
                // Obtain the selected recipe that the user selected.
                int recipeIndex = GetSelectedRecipe();

                if (recipeIndex > -1 && recipeIndex < recipeList.Count)
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("\t1 - Scale by 0.5");
                    Console.WriteLine("\t3 - Scale by 2");
                    Console.WriteLine("\t4 - Scale by 3");

                    // Speak aloud using the SpeechEngine.
                    Speak(SpeechEngine.SPEECH_CHOOSE_SCALE_FACTOR, blueScreenEnabled);

                    // Obtain the selected menu item.
                    string sMenuItem = ConsoleIO.GetValidInput("Choose the scale factor. Provide the number only: ", ConsoleColor.Yellow, InputDataType.PositiveDecimals, false);
                    int selectedMenuItem = Convert.ToInt32(sMenuItem);
                    float scaleFactor = 0.0f;
                    bool foundError = false;

                    // Set the scale factor based on the selected menu item.
                    switch (selectedMenuItem)
                    {
                        case 1:
                            // Halve the ingredients.
                            scaleFactor = 0.5f;
                            break;

                        case 3:
                            // Double the ingredients.
                            scaleFactor = 2.0f;
                            break;

                        case 4:
                            // Tripple the ingredients.
                            scaleFactor = 3.0f;
                            break;

                        default:
                            // Force an error if the selected menu item is out of range.
                            foundError = true;
                            break;
                    }

                    if (!foundError)
                    {
                        // The process was successful.
                        recipeList[recipeIndex].ScaleFactor = scaleFactor;
                        ConsoleIO.PrintColorText("\nSuccessfully updated the scale factor.", ConsoleColor.DarkGreen, true);
                        PlaySuccessSound(blueScreenEnabled);
                    }
                    else
                    {
                        // Display the error message to the user.
                        ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                        Console.WriteLine("Invalid option provided.");
                        PlayErrorSound(blueScreenEnabled);
                    }
                }
                else
                {
                    // Display the error message to the user.
                    ConsoleIO.PrintColorText("Error: ", ConsoleColor.Red, false);
                    Console.WriteLine("The index value is out of range.");
                    PlayErrorSound(blueScreenEnabled);
                }
            }
            else
            {
                ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                Console.WriteLine("No recipes found.");
                PlayErrorSound(blueScreenEnabled);
            }
        }

        /// <summary>
        /// This method handles the operation of resetting a recipe's scale.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void Operation_ResetRecipeScale()
        {
            if (recipeList.Count > 0)
            {
                ConsoleIO.PrintColorText("\nOPERATION: Reset recipe scale", ConsoleColor.Blue, true);

                // Speak aloud using the SpeechEngine.
                SpeakAndWait(SpeechEngine.SPEECH_OPERATION_RESET_RECIPE_SCALE, blueScreenEnabled);

                // Display the recipes to the user.
                DisplayRecipes();
                int recipeIndex = GetSelectedRecipe();

                // Check if the recipeIndex is within range.
                if (recipeIndex > -1 && recipeIndex < recipeList.Count)
                {
                    // Speak aloud using the SpeechEngine.
                    Speak(SpeechEngine.SPEECH_RESET_RECIPE_SCALE_QUESTION, blueScreenEnabled);
                    Console.WriteLine("Are you sure you want to reset the recipe scale? Enter (Y/N)");
                    ConsoleKeyInfo keyInfo = ConsoleIO.GetKeyInput(ConsoleColor.Yellow);

                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        // Reset the recipe scale.
                        recipeList[recipeIndex].ScaleFactor = 1.0f;
                        ConsoleIO.PrintColorText("\nSuccessfully resetted the recipe scale.", ConsoleColor.DarkGreen, true);
                        PlaySuccessSound(blueScreenEnabled);
                    }
                }
                else
                {
                    // Display the error message to the user.
                    ConsoleIO.PrintColorText("Error: ", ConsoleColor.Red, false);
                    Console.WriteLine("The index value is out of range.");
                    PlayErrorSound(blueScreenEnabled);
                }
            }
            else
            {
                ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                Console.WriteLine("No recipes found.");
                PlayErrorSound(blueScreenEnabled);
            }
        }

        /// <summary>
        /// This method handles the operation of viewing a recipe.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void Operation_ViewRecipe()
        {
            // Check if the user created at least one recipe.
            if (recipeList.Count > 0)
            {
                ConsoleIO.PrintColorText("\nOPERATION: View a recipe", ConsoleColor.Blue, true);
                Console.WriteLine("--------------------------------------------");

                // Speak aloud using the SpeechEngine.
                SpeakAndWait(SpeechEngine.SPEECH_OPERATION_VIEW_RECIPE, blueScreenEnabled);

                // Display all the recipes to the user.
                DisplayRecipes();
                int recipeIndex = GetSelectedRecipe();

                // Check if the recipeIndex is not -1.
                if(recipeIndex > -1 && recipeIndex < recipeList.Count)
                {
                    Recipe recipe = recipeList[recipeIndex];
                    ConsoleIO.PrintColorText("--------------------------------------------", ConsoleColor.DarkGreen, true);
                    Console.WriteLine(recipe.ToString());
                }
                else
                {
                    // Display the error message to the user.
                    ConsoleIO.PrintColorText("Error: ", ConsoleColor.Red, false);
                    Console.WriteLine("The index value is out of range.");
                    PlayErrorSound(blueScreenEnabled);
                }
            }
            else
            {
                // Display an error message to the user if no recipes were created.
                ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                Console.WriteLine("Unable to view the recipe. The recipe has not been created yet.");
                PlayErrorSound(blueScreenEnabled);
            }
        }

        /// <summary>
        /// This method handles the operation of deleting a recipe.
        /// </summary>
        /// -------------------------------------------------------------------------
        private void Operation_DeleteRecipe()
        {
            // Check if the user created at least one recipe.
            if (recipeList.Count() > 0)
            {
                ConsoleIO.PrintColorText("\nOPERATION: Delete recipe", ConsoleColor.Blue, true);
                Console.WriteLine("-----------------------------------------");

                // Speak aloud using the SpeechEngine.
                SpeakAndWait(SpeechEngine.SPEECH_OPERATION_DELETE_RECIPE, blueScreenEnabled);

                // Display all the recipes to the user.
                DisplayRecipes();
                int recipeIndex = GetSelectedRecipe();

                if (recipeIndex > -1 && recipeIndex < recipeList.Count)
                {
                    // Speak aloud using the SpeechEngine.
                    Speak(SpeechEngine.SPEECH_DELETE_RECIPE_QUESTION, blueScreenEnabled);
                    Console.WriteLine("Are you sure you want to delete the recipe. Enter (Y/N):");
                    ConsoleKeyInfo keyInfo = ConsoleIO.GetKeyInput(ConsoleColor.Yellow);

                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        // Delete the recipe from the list, from the given index.
                        recipeList.RemoveAt(recipeIndex);
                        // Sort the recipes in ascending order.
                        SortRecipes();

                        ConsoleIO.PrintColorText("\nSuccessfully deleted the recipe.", ConsoleColor.DarkGreen, true);
                        PlaySuccessSound(blueScreenEnabled);
                    }
                }
                else
                {
                    // Display the error message to the user.
                    ConsoleIO.PrintColorText("Error: ", ConsoleColor.Red, false);
                    Console.WriteLine("The index value is out of range.");
                    PlayErrorSound(blueScreenEnabled);
                }
            }
            else
            {
                // Display an error message to the user.
                ConsoleIO.PrintColorText("\nError: ", ConsoleColor.Red, false);
                Console.WriteLine("Unable to delete a recipe. No recipe(s) found.");
                PlayErrorSound(blueScreenEnabled);
            }
        }
    }
}
