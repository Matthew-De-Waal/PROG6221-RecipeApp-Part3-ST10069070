using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeApp;

namespace RecipeAppWPF
{
    public class ExtendedRecipe : Recipe
    {
        // Data fields
        private bool notifyUser = false;
        private bool notifyUser2 = false;

        /// <summary>
        /// Automatic Properties
        /// </summary>
        public bool EnableMultipleNotifications { get; set; } = false;

        /// <summary>
        /// This method gets the ingredient count of the recipe.
        /// </summary>
        /// <returns></returns>
        public int GetIngredientCount()
        {
            return base.ingredients.Count;
        }

        /// <summary>
        /// This method gets the instruction count of the recipe.
        /// </summary>
        /// <returns></returns>
        public int GetInstructionCount()
        {
            return base.instructions.Count;
        }

        /// <summary>
        /// This method removes an ingredient by the given index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveIngredient(int index)
        {
            base.ingredients.RemoveAt(index);
        }

        /// <summary>
        /// This method removes an instruction by the given index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveInstruction(int index)
        {
            base.instructions.RemoveAt(index);
        }

        /// <summary>
        /// Clears all the ingredients in the recipe.
        /// </summary>
        public void ClearIngredients()
        {
            base.ingredients.Clear();
        }

        /// <summary>
        /// Clears all the instructions in the recipe.
        /// </summary>
        public void ClearInstructions()
        {
            base.instructions.Clear();
        }

        /// <summary>
        /// Returns an array of ingredients from the recipe.
        /// </summary>
        /// <returns></returns>
        public RecipeIngredient[] GetIngredients()
        {
            return base.ingredients.ToArray();
        }

        /// <summary>
        /// Returns an array of instructions from the recipe.
        /// </summary>
        /// <returns></returns>
        public RecipeStep[] GetInstructions()
        {
            return base.instructions.ToArray();
        }

        /// <summary>
        /// Gets the total calory count from the recipe.
        /// </summary>
        /// <returns></returns>
        public int GetTotalCalories()
        {
            int totalCalories = 0;

            for(int i = 0; i  < base.ingredients.Count; i++)
            {
                totalCalories += (int)(base.ingredients[i].Calories * base.ScaleFactor);
            }

            return totalCalories;
        }

        /// <summary>
        /// Updates an ingredient within the recipe.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newIngredient"></param>
        public void UpdateIngredient(int index, RecipeIngredient newIngredient)
        {
            this.ingredients[index] = newIngredient;
        }

        /// <summary>
        /// Updates an instruction within the recipe.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newStep"></param>
        public void UpdateInstruction(int index, RecipeStep newStep)
        {
            this.instructions[index] = newStep;
        }

        /// <summary>
        /// This method performs data validation.
        /// The main purpose of this overrided method is to notify the user only once
        /// when the total number of calories exceeds the limit or notify the user many
        /// times when the total number of calories exceeds the limit.
        /// </summary>
        public override void Validate()
        {
            if(EnableMultipleNotifications)
                // Perform data validation.
                base.Validate();
            else
            {
                // Get the total number of calories from the recipe
                int totalCalories = GetTotalCalories();

                // Check if the total number of calories is less than the MaxCalories;
                if (totalCalories < MaxCalories)
                    notifyUser2 = true;

                // Check if the total number of calories exceeds the MaxCalories and if notifyUser2 is true.
                if (totalCalories > MaxCalories && notifyUser2)
                    notifyUser = true;

                // Check if notifyUser is true
                if(notifyUser)
                {
                    // Perform data validation.
                    base.Validate();

                    // Reset the values.
                    notifyUser = false;
                    notifyUser2 = false;
                }
            }
        }
    }
}
