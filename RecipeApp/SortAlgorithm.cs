using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    /// <summary>
    /// This class contains the functionality to sort Recipe objects.
    /// </summary>
    public static class SortAlgorithm
    {
        /// <summary>
        /// Sorts the array of recipes in ascending order, using the Bubble Sort Algorithm.
        /// </summary>
        /// <param name="recipes"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        public static Recipe[] BubbleSortAscending(Recipe[] recipes)
        {
            // The output array that will be in ascending order.
            byte[][] items = new byte[recipes.Length][];

            for(int i = 0; i < recipes.Length; i++)
                // Convert each string (name of each recipe) to an array of bytes, using UTF8 Encoding.
                items[i] = Encoding.UTF8.GetBytes(recipes[i].Name.ToLower());

            // Receive the sorted array from the function 'BubbleSortAscending'.
            byte[][] sortedItems = BubbleSortAscending(items);
            // This array will contain the Recipe objects in ascending order.
            Recipe[] output = new Recipe[recipes.Length];

            // Iterate through the sortedItems array to finalize the output array.
            for(int i = 0; i < output.Length; i++)
            {
                string s = new string(Encoding.UTF8.GetChars(sortedItems[i]));
                output[i] = recipes[FindRecipe(recipes, s)];
            }

            return output;
        }

        /// <summary>
        /// Finds the recipe index of the given recipe name.
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        private static int FindRecipe(Recipe[] recipes, string s)
        {
            // The variable to hold the index value of the recipe.
            int recipeIndex = -1;

            // Iterate through the recipes array to find the recipe name and get its index value.
            for(int i = 0; i < recipes.Length; i++)
            {
                if (recipes[i].Name.ToLower() == s.ToLower())
                {
                    recipeIndex = i;
                    break;
                }
            }

            return recipeIndex;
        }

        /// <summary>
        /// Determines the largest array by checking the values of each array.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        private static int FindLargestArray(byte[] b1, byte[] b2)
        {
            int result = -1;
            // Get the smallest length from 'b1' and 'b2'.
            int length = b1.Length <= b2.Length ? b1.Length : b2.Length;

            // Find the array that has the largest values.
            for(int i = 0; i < length; i++)
            {
                if (b1[i] > b2[i])
                {
                    // The first array is larger than the second array.
                    result = 0;
                    break;
                }

                if (b1[i] < b2[i])
                {
                    // The second array is larger than the first array.
                    result = 1;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Sorts the 2D byte array in ascending order, using the Bubble Sort Algorithm.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        private static byte[][] BubbleSortAscending(byte[][] input)
        {
            // This variable will hold the output result.
            byte[][] result = (byte[][])input.Clone();

            // Outer loop of the Bubble Sort Algorithm.
            for(int a = 0; a < (result.Length - 1); a++)
            {
                // Inner loop of the Bubble Sort Algorithm.
                for(int b = 0; b < (result.Length - 1); b++)
                {
                    // This variable will state which array is the largest.
                    int p = FindLargestArray(result[b], result[b + 1]);

                    // If p = -1: Both of the arrays are the same size.
                    // If p = 0: The first array is larger than the second array.
                    // If p = 1: The second array is larger than the first array.
                    if(p == 0)
                    {
                        // Proceed if the largest array is the first array.
                        // Swap the first array with the second array.
                        byte[] temp = result[b];
                        result[b] = result[b + 1];
                        result[b + 1] = temp;
                    }
                }
            }

            return result;
        }
    }
}
