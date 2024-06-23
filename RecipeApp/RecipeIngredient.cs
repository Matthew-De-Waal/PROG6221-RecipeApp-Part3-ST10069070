using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    /// <summary>
    /// This class represents an ingredient for a recipe.
    /// </summary>
    public class RecipeIngredient
    {
        // Automatic Properties
        public string Name { get; set; }
        public int Quantity { get; set; }
        public UnitMeasurement Measurement { get; set; }
        public int Calories { get; set; }
        public FoodCategory Category { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// -------------------------------------------------------------------------
        public RecipeIngredient()
        {
            this.Name = string.Empty;
            this.Quantity = 0;
            this.Measurement = default;
        }

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <param name="measurement"></param>
        /// -------------------------------------------------------------------------
        public RecipeIngredient(string name, int quantity, UnitMeasurement measurement)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Measurement = measurement;
        }

        /// <summary>
        /// Advanced master constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <param name="measurement"></param>
        /// <param name="calories"></param>
        /// <param name="category"></param>
        /// -------------------------------------------------------------------------
        public RecipeIngredient(string name, int quantity, UnitMeasurement measurement, int calories, FoodCategory category)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Measurement = measurement;
            this.Calories = calories;
            this.Category = category;
        }

        /// <summary>
        /// Returns the ingredient as a string.
        /// </summary>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        public override string ToString()
        {
            string sMeasurement = string.Empty;
            string sCategory = string.Empty;

            // Convert the UnitMeasurement enumeration to a string.
            switch(Measurement)
            {
                case UnitMeasurement.Millilitres:
                    // Assign the measurement to Millitres.
                    sMeasurement = "ML";
                    break;

                case UnitMeasurement.Litres:
                    // Assign the measurement to Litres.
                    sMeasurement = "L";
                    break;

                case UnitMeasurement.Grams:
                    // Assign the measurement to Grams.
                    sMeasurement = "G";
                    break;

                case UnitMeasurement.Kilograms:
                    // Assign the measurement to Kilograms.
                    sMeasurement = "KG";
                    break;

                case UnitMeasurement.Teaspoon:
                    // Assign the measurement to Teaspoon.
                    sMeasurement = "Teaspoon";
                    break;

                case UnitMeasurement.Tablespoon:
                    // Assign the measurement to Tablespoon.
                    sMeasurement = "Tablespoon";
                    break;
            }

            // Convert the Category enumeration to a string
            switch(Category)
            {
                case FoodCategory.Vegetables:
                    // Assign the category to Vegetables.
                    sCategory = "Vegetables";
                    break;

                case FoodCategory.Fruits:
                    // Assign the category to Fruits.
                    sCategory = "Fruits";
                    break;

                case FoodCategory.Grains:
                    // Assign the category to Grains.
                    sCategory = "Grains";
                    break;

                case FoodCategory.Protein:
                    // Assign the category to Protein.
                    sCategory = "Protein";
                    break;

                case FoodCategory.Dairy:
                    // Assign the category to Dairy.
                    sCategory = "Dairy";
                    break;

                case FoodCategory.OilAndSolidFats:
                    // Assign the category to Oil & Solid Fats.
                    sCategory = "Oil & Solid Fats";
                    break;

                case FoodCategory.AddedSugars:
                    // Assign the category to Added Sugars.
                    sCategory = "Added Sugars";
                    break;

                case FoodCategory.Beverages:
                    // Assign the category to Beverages.
                    sCategory = "Beverages";
                    break;
            }

            return $"{Quantity} {sMeasurement} of {Name}; Calories: {Calories}; Category: {sCategory}";
        }

        /// <summary>
        /// Returns a string of the ingredient where the measurement is scaled.
        /// </summary>
        /// <param name="scaleValue"></param>
        /// <returns></returns>
        /// -------------------------------------------------------------------------
        public string ToString(float scaleFactor)
        {
            string sMeasurement = string.Empty;
            string sCategory = string.Empty;

            // Convert the UnitMeasurement enumeration to a string.
            switch (Measurement)
            {
                case UnitMeasurement.Millilitres:
                    // Assign the category to Vegetables.
                    sMeasurement = "ML";
                    break;

                case UnitMeasurement.Litres:
                    // Assign the measurement to Litres.
                    sMeasurement = "L";
                    break;

                case UnitMeasurement.Grams:
                    // Assign the measurement to Grams.
                    sMeasurement = "G";
                    break;

                case UnitMeasurement.Kilograms:
                    // Assign the measurement to Kilograms.
                    sMeasurement = "KG";
                    break;

                case UnitMeasurement.Teaspoon:
                    // Assign the measurement to Teaspoon.
                    sMeasurement = "Teaspoon";
                    break;

                case UnitMeasurement.Tablespoon:
                    // Assign the measurement to Tablespoon.
                    sMeasurement = "Tablespoon";
                    break;
            }

            // Convert the Category enumeration to a string.
            switch (Category)
            {
                case FoodCategory.Vegetables:
                    // Assign the category to Vegetables.
                    sCategory = "Vegetables";
                    break;

                case FoodCategory.Fruits:
                    // Assign the category to Fruits.
                    sCategory = "Fruits";
                    break;

                case FoodCategory.Grains:
                    // Assign the category to Grains.
                    sCategory = "Grains";
                    break;

                case FoodCategory.Protein:
                    // Assign the category to Protein.
                    sCategory = "Protein";
                    break;

                case FoodCategory.Dairy:
                    // Assign the category to Dairy.
                    sCategory = "Dairy";
                    break;

                case FoodCategory.OilAndSolidFats:
                    // Assign the category to Oil & Solid Fats.
                    sCategory = "Oil & Solid Fats";
                    break;

                case FoodCategory.AddedSugars:
                    // Assign the category to Added Sugars.
                    sCategory = "Added Sugars";
                    break;

                case FoodCategory.Beverages:
                    // Assign the category to Beverages.
                    sCategory = "Beverages";
                    break;
            }

            // Add the plural 's' if the Quantity is not equal to one.
            if (Quantity != 1)
                sMeasurement += "s";

            return $"{Quantity * scaleFactor} {sMeasurement} of {Name}; Calories: {Calories * scaleFactor}; Category: {sCategory}";
        }
    }
}
