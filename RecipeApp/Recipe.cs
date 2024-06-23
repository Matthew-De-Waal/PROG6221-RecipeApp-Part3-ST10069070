using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RecipeApp
{
    // Event Handler to handle events with the Recipe class.
    public delegate void RecipeEventHandler(Recipe r, int amount);

    /// <summary>
    /// This class represents a recipe that will hold ingredients and instructions.
    /// </summary>
    public class Recipe
    {
        // Automatic Properties
        public string Name { get; set; }
        public float ScaleFactor { get; set; }
        public int MaxCalories { get; set; }
        
        // Data fields
        protected List<RecipeIngredient> ingredients = new List<RecipeIngredient>();
        protected List<RecipeStep> instructions = new List<RecipeStep>();

        // Events
        public event RecipeEventHandler MaximumCaloriesReached;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// -------------------------------------------------------------------------
        public Recipe()
        {
            this.ScaleFactor = 1.0f;
        }

        /// <summary>
        /// Validates any data that must be checked.
        /// </summary>
        public virtual void Validate()
        {
            int totalCalories = CalculateTotalCalories();

            // Raise the MaximumCaloriesReached event if the totalCalories is greater than the MaxCalories property.
            if(totalCalories > MaxCalories)
                MaximumCaloriesReached(this, totalCalories);
        }

        /// <summary>
        /// Adds a new ingredient to the recipe.
        /// </summary>
        /// <param name="ingredient"></param>
        public void AddIngredient(RecipeIngredient ingredient)
        {
            this.ingredients.Add(ingredient);
        }

        /// <summary>
        /// Adds a new instruction to the recipe.
        /// </summary>
        /// <param name="step"></param>
        public void AddInstruction(RecipeStep step)
        {
            this.instructions.Add(step);
        }

        /// <summary>
        /// Returns the recipe object as a string.
        /// </summary>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        public override string ToString()
        {
            // Calculate the total calories for the recipe.
            int totalCalories = CalculateTotalCalories();

            // Build the output string, using a StringBuilder.
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Recipe Name: {Name}");
            sb.AppendLine("----------------------------");
            sb.AppendLine($"Number of ingredients: \t{ingredients.Count}");
            sb.AppendLine($"Number of steps: \t{instructions.Count}");
            sb.AppendLine($"Total number of calories: \t{totalCalories * ScaleFactor}");
            sb.AppendLine();

            sb.AppendLine("Ingredients:");

            // Iterate through the Ingredients array to display each ingredient.
            for (int i = 0; i < ingredients.Count; i++)
                sb.AppendLine($"\t{ingredients[i].ToString(ScaleFactor)}");

            sb.AppendLine("----------------------------");
            sb.AppendLine();
            sb.AppendLine("Instructions (Steps):");

            // Iterate through the Instructions array to display each instruction.
            for (int i = 0; i < instructions.Count; i++)
                sb.AppendLine($"\tStep {i}: {instructions[i].HelpText}");

            sb.AppendLine("----------------------------");

            // Print out any notifications.
            if (totalCalories > MaxCalories)
            {
                sb.AppendLine();
                sb.AppendLine("Maximum Calories Reached");
                sb.AppendLine("Calory Limit: 300");
                sb.AppendLine($"Total number of calories in this recipe: {totalCalories}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Clears the recipe to its original state.
        /// </summary>
        public void Clear()
        {
            // Set the properties to its default values.
            this.ingredients.Clear();
            this.instructions.Clear();
            this.ScaleFactor = 1.0f;
        }

        /// <summary>
        /// Calculates and returns the total calories of the recipe.
        /// </summary>
        /// <returns></returns>
        private int CalculateTotalCalories()
        {
            int totalCalories = 0;

            // Check if the number of ingredients is not zero.
            if(ingredients.Count != 0)
            {
                // Iterate through the Ingredients array to sum the calories in each ingredient.
                for(int i = 0; i < ingredients.Count; i++)
                {
                    totalCalories += ingredients[i].Calories;
                }
            }

            return totalCalories;
        }
    }
}
