using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RecipeApp;

namespace RecipeApp.UnitTests
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalculateTotalCaloriesTest()
        {
            // Declare and instantiate a new Recipe object.
            Recipe recipe = new Recipe();
            recipe.MaxCalories = 300;

            // Instantiate new RecipeIngredient objects.
            recipe.AddIngredient(new RecipeIngredient("Ingredient 1", 0, default, 200, default));
            recipe.AddIngredient(new RecipeIngredient("Ingredient 2", 0, default, 100, default));
            recipe.AddIngredient(new RecipeIngredient("Ingredient 3", 0, default, 10, default));

            recipe.MaximumCaloriesReached += MaxCaloriesReached;
            // Perform data validation.
            recipe.Validate();
        }

        private void MaxCaloriesReached(Recipe r, int i)
        {
            Assert.AreEqual(true, i > 300);
        }
    }
}
