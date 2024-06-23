using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using RecipeApp;
using Microsoft.Win32;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Data fields.
        private List<ExtendedRecipe> recipeList = new List<ExtendedRecipe>();
        private List<int> recipeMenuList = new List<int>();
        private PieChart pieChart = new PieChart();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays all the recipes on the TreeView 'tvRecipes'.
        /// </summary>
        private void DisplayRecipes()
        {
            // Process tvRecipes TreeView
            tvRecipes.Items.Clear();

            // Iterate through the recipeList array.
            for (int i = 0; i < recipeList.Count; i++)
            {
                // Obtain the current recipe from the list.
                ExtendedRecipe recipe = recipeList[i];

                // Declare and instantiate a new TreeViewItem object.
                TreeViewItem parent = new TreeViewItem();
                TreeViewItem itemIngredients = new TreeViewItem();
                TreeViewItem itemInstructions = new TreeViewItem();

                // Assign the header information.
                parent.Header = recipe.Name;
                itemIngredients.Header = "Ingredients";
                itemInstructions.Header = "Instructions";

                // Obtain the ingredient and instruction count from the recipe.
                int ingredientCount = recipe.GetIngredientCount();
                int instructionCount = recipe.GetInstructionCount();

                // Obtain an array of ingredients from the recipe.
                RecipeIngredient[] ingredients = recipe.GetIngredients();

                // Iterate through the ingredient array.
                for (int j = 0; j < ingredientCount; j++)
                {
                    // Declare and instantiate a new TreeViewItem object.
                    TreeViewItem item = new TreeViewItem();

                    // Assign the details to the object.
                    item.Header = ingredients[j].Name;
                    item.Tag = $"RECIPE_INGREDIENT {i} {j}";
                    item.Background = new SolidColorBrush(Colors.White);
                    item.Foreground = new SolidColorBrush(Colors.Black);

                    // Add the object to the tree view.
                    itemIngredients.Items.Add(item);
                }

                // Iterate through the instruction array.
                for (int j = 0; j < instructionCount; j++)
                {
                    // Declare and instantiate a new TreeViewItem object.
                    TreeViewItem item = new TreeViewItem();

                    // Assign the details to the object.
                    item.Header = $"Instruction {j}";
                    item.Tag = $"RECIPE_INSTRUCTION {i} {j}";
                    item.Background = new SolidColorBrush(Colors.White);
                    item.Foreground = new SolidColorBrush(Colors.Black);

                    // Add the object to the tree view.
                    itemInstructions.Items.Add(item);
                }

                // Assign the details to the object.
                parent.Tag = $"RECIPE_PARENT {i}";
                parent.Background = new SolidColorBrush(Colors.White);
                parent.Foreground = new SolidColorBrush(Colors.Black);

                // Assign the details to the object.
                itemIngredients.Tag = $"RECIPE_INGREDIENTS {i}";
                itemIngredients.Background = new SolidColorBrush(Colors.White);
                itemIngredients.Foreground = new SolidColorBrush(Colors.Black);

                // Assign the details to the object.
                itemInstructions.Tag = $"RECIPE_INSTRUCTIONS {i}";
                itemInstructions.Background = new SolidColorBrush(Colors.White);
                itemInstructions.Foreground = new SolidColorBrush(Colors.Black);

                // Add itemIngredients and itemInstructions to the tree view item 'parent'.
                parent.Items.Add(itemIngredients);
                parent.Items.Add(itemInstructions);

                // Add the tree view item 'parent' to the tree view.
                tvRecipes.Items.Add(parent);
            }
        }

        /// <summary>
        /// Displays all the recipes from the recipe menu.
        /// </summary>
        private void DisplayMenuRecipes()
        {
            // Reset the lbRecipeMenu listbox.
            lbRecipeMenu.Items.Clear();
            // Add the item to the listbox that refers to the whole recipe menu.
            lbRecipeMenu.Items.Add("<PARENT>");

            // Iterate through the recipeMenuList.
            for (int i = 0; i < recipeMenuList.Count; i++)
            {
                // Obtain the recipeName.
                string recipeName = recipeList[recipeMenuList[i]].Name;
                // Add a new item to the lbRecipeMenu listbox.
                lbRecipeMenu.Items.Add(recipeName);
            }
        }

        /// <summary>
        /// Adds a new recipe to recipeList and tvRecipes.
        /// </summary>
        /// <param name="recipeName"></param>
        private void AddRecipe(string recipeName)
        {
            // Declare and instantiate a new ExtendedRecipe object.
            ExtendedRecipe recipe = new ExtendedRecipe();

            // Assign the details to the recipe.
            recipe.Name = recipeName;
            recipe.MaxCalories = 300;
            recipe.MaximumCaloriesReached += MaxCaloriesReached;

            // Add the recipe to the recipeList.
            recipeList.Add(recipe);

            // Sort the recipes in ascending order, using the Bubble Sort Algorithm.
            Recipe[] recipes = SortAlgorithm.BubbleSortAscending(recipeList.ToArray());
            // Clear the recipeList.
            recipeList.Clear();
            
            // Declare and instantiate a new array.
            ExtendedRecipe[] recipeArray = new ExtendedRecipe[recipes.Length];

            // Iterate through the recipeArray.
            for (int i = 0; i < recipeArray.Length; i++)
            {
                // Convert the recipe array to an ExtendedRecipe array.
                recipeArray[i] = (ExtendedRecipe)recipes[i];
            }

            // Add the extended recipe array to the recipeList.
            recipeList.AddRange(recipeArray);
            // Display all the recipes to the user.
            DisplayRecipes();
        }

        /// <summary>
        /// Displays a recipe to the user.
        /// </summary>
        /// <param name="recipe"></param>
        private void LoadRecipe(ExtendedRecipe recipe)
        {
            // Display the recipe content to the user.
            txtRecipeData.Text = recipe.ToString();
        }

        /// <summary>
        /// Updates the pie chart.
        /// </summary>
        /// <param name="recipeIndex"></param>
        private void UpdatePieChart(int recipeIndex)
        {
            // Declare and instantiate a new integer array.
            int[] categories = new int[8];

            // Check if the recipeIndex is '-1'
            if (recipeIndex == -1)
            {
                // Show the statistics of the whole recipe menu.
                UpdatePieChart_RecipeMenu(ref categories);
            }
            else
            {
                // Show the statistics of single recipe from the menu.
                UpdatePieChart_SingleRecipe(ref categories, recipeIndex);
            }

            // Check if the sum of the categories is greater than zero.
            if (categories.Sum() > 0)
            {
                // Instantiate a new integer array.
                this.pieChart.Percentages = new float[8]
                {
                // Category: Vegetables
                ((float)categories[0] / categories.Sum() * 100),
                // Category: Fruits
                ((float)categories[1] / categories.Sum() * 100),
                // Category: Grains
                ((float)categories[2] / categories.Sum() * 100),
                // Category: Protein
                ((float)categories[3] / categories.Sum() * 100),
                // Category: Dairy
                ((float)categories[4] / categories.Sum() * 100),
                // Category: Oil and Solid Fats
                ((float)categories[5] / categories.Sum() * 100),
                // Category: Added Sugars
                ((float)categories[6] / categories.Sum() * 100),
                // Category: Beverages
                ((float)categories[7] / categories.Sum() * 100),
                };

                // Assign the details to the food category labels.
                lblVegetables.Content = $"Vegetables: {(int)this.pieChart.Percentages[0]}%";
                lblFruits.Content = $"Fruits: {(int)this.pieChart.Percentages[1]}%";
                lblGrains.Content = $"Grains: {(int)this.pieChart.Percentages[2]}%";
                lblProtein.Content = $"Protein: {(int)this.pieChart.Percentages[3]}%";
                lblDairy.Content = $"Dairy: {(int)this.pieChart.Percentages[4]}%";
                lblOilSolidFats.Content = $"Oil and Solid Fats: {(int)this.pieChart.Percentages[5]}%";
                lblAddedSugars.Content = $"Added Sugars: {(int)this.pieChart.Percentages[6]}%";
                lblBeverages.Content = $"Beverages: {(int)this.pieChart.Percentages[7]}%";
            }
            else
            {
                // Instantiate a new integer array.
                this.pieChart.Percentages = new float[8];
            }

            // Draw the pie chart.
            this.pieChart.DrawChart();
        }

        /// <summary>
        /// Part of the UpdatePieChart method.
        /// </summary>
        /// <param name="categories"></param>
        private void UpdatePieChart_RecipeMenu(ref int[] categories)
        {
            // Iterate through the recipeMenuList.
            for (int i = 0; i < recipeMenuList.Count; i++)
            {
                // Obtain the ingredients from the recipe.
                RecipeIngredient[] ingredients = recipeList[recipeMenuList[i]].GetIngredients();

                // Iterate through the 'ingredients' array.
                for (int j = 0; j < ingredients.Length; j++)
                {
                    // Increase the category count from the given category.
                    switch (ingredients[j].Category)
                    {
                        case FoodCategory.Vegetables:
                            // The food category is 'Vegetables'.
                            categories[0]++;
                            break;

                        case FoodCategory.Fruits:
                            // The food category is 'Fruits'.
                            categories[1]++;
                            break;

                        case FoodCategory.Grains:
                            // The food category is 'Grains'.
                            categories[2]++;
                            break;

                        case FoodCategory.Protein:
                            // The food category is 'Protein'.
                            categories[3]++;
                            break;

                        case FoodCategory.Dairy:
                            // The food category is 'Dairy'.
                            categories[4]++;
                            break;

                        case FoodCategory.OilAndSolidFats:
                            // The food category is 'Oil and Solid Fats'.
                            categories[5]++;
                            break;

                        case FoodCategory.AddedSugars:
                            // The food category is 'Added Sugars'.
                            categories[6]++;
                            break;

                        case FoodCategory.Beverages:
                            // The food category is 'Beverages'.
                            categories[7]++;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Part of the UpdatePieChart method.
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="recipeIndex"></param>
        private void UpdatePieChart_SingleRecipe(ref int[] categories, int recipeIndex)
        {
            // Obtain the ingredients of the recipe.
            RecipeIngredient[] ingredients = recipeList[recipeIndex].GetIngredients();

            // Iterate through the 'ingredients' array.
            for (int j = 0; j < ingredients.Length; j++)
            {
                // Increase the category count from the given category.
                switch (ingredients[j].Category)
                {
                    case FoodCategory.Vegetables:
                        // The category is 'Vegetables'.
                        categories[0]++;
                        break;

                    case FoodCategory.Fruits:
                        // The category is 'Fruits'.
                        categories[1]++;
                        break;

                    case FoodCategory.Grains:
                        // The category is 'Grains'.
                        categories[2]++;
                        break;

                    case FoodCategory.Protein:
                        // The category is 'Protein'.
                        categories[3]++;
                        break;

                    case FoodCategory.Dairy:
                        // The category is 'Dairy'.
                        categories[4]++;
                        break;

                    case FoodCategory.OilAndSolidFats:
                        // The category is 'Oil and Solid Fats'.
                        categories[5]++;
                        break;

                    case FoodCategory.AddedSugars:
                        // The category is 'Added Sugars'.
                        categories[6]++;
                        break;

                    case FoodCategory.Beverages:
                        // The category is 'Beverages'.
                        categories[7]++;
                        break;
                }
            }
        }

        /// <summary>
        /// Converts a unit measurement to a string.
        /// </summary>
        /// <param name="measurement"></param>
        /// <returns></returns>
        private string GetUnitMeasurementString(UnitMeasurement measurement)
        {
            // Declare and instantiate a new string variable.
            string sUnitMeasurement = string.Empty;

            // Determine the 'sUnitMeasurement' value from the measurement parameter.
            switch (measurement)
            {
                case UnitMeasurement.Millilitres:
                    // The unit measurement is Millitres.
                    sUnitMeasurement = "ML";
                    break;

                case UnitMeasurement.Litres:
                    // The unit measurement is Litres.
                    sUnitMeasurement = "L";
                    break;

                case UnitMeasurement.Grams:
                    // The unit measurement is Grams.
                    sUnitMeasurement = "G";
                    break;

                case UnitMeasurement.Kilograms:
                    // The unit measurement is Kilograms.
                    sUnitMeasurement = "KG";
                    break;

                case UnitMeasurement.Teaspoon:
                    // The unit measurement is Teaspoon.
                    sUnitMeasurement = "t";
                    break;

                case UnitMeasurement.Tablespoon:
                    // The unit measurement is Tablespoon.
                    sUnitMeasurement = "T";
                    break;
            }

            return sUnitMeasurement;
        }

        /// <summary>
        /// Converts a food category to a string.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private string GetFoodCategoryString(FoodCategory category)
        {
            // Declare and initialize a string variable.
            string sFoodCategory = string.Empty;

            // Determine the 'sFoodCategory' from the category parameter.
            switch (category)
            {
                case FoodCategory.Vegetables:
                    // The food category is Vegetables.
                    sFoodCategory = "Vegetables";
                    break;

                case FoodCategory.Fruits:
                    // The food category is Fruits.
                    sFoodCategory = "Fruits";
                    break;

                case FoodCategory.Grains:
                    // The food category is Grains.
                    sFoodCategory = "Grains";
                    break;

                case FoodCategory.Protein:
                    // The food category is Protein.
                    sFoodCategory = "Protein";
                    break;

                case FoodCategory.Dairy:
                    // The food category is Dairy.
                    sFoodCategory = "Dairy";
                    break;

                case FoodCategory.OilAndSolidFats:
                    // The food category is Oil and Solid Fats.
                    sFoodCategory = "Oil & Solid Fats";
                    break;

                case FoodCategory.AddedSugars:
                    // The food category is Added Sugars.
                    sFoodCategory = "Added Sugars";
                    break;

                case FoodCategory.Beverages:
                    // The food category is Beverages.
                    sFoodCategory = "Beverages";
                    break;
            }

            return sFoodCategory;
        }

        /// <summary>
        /// This method is called when the maximum calories for a recipe is reached.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="amount"></param>
        private void MaxCaloriesReached(Recipe r, int amount)
        {
            MessageBox.Show($"Maximum Calories Reached for the recipe '{r.Name}'.\nMaximum Calory Limit: 300\nTotal number of calories: {amount}", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Occurs when the window is first shown to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Instantiate the pieChart object.
            this.pieChart = new PieChart(pieView,
                new Color[] {Colors.Red, Colors.Green, Colors.DarkCyan, Colors.Blue,
                             Colors.Orange, Colors.MediumAquamarine, Colors.Yellow, Colors.Magenta},
                new float[8], new Point(166, 136), 130);

            // Initialize the lbRecipeMenu component.
            lbRecipeMenu.Items.Add("<PARENT>");
        }

        /// <summary>
        /// Occurs when the user resizes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double windowWidth = gridMain.ActualWidth;
            // Set the new bounds (rectangle) of the gridRecipeMenu component.
            this.gridRecipeMenu.Margin = new Thickness((windowWidth / 2) - (gridRecipeMenu.ActualWidth / 2), 0, 0, 0);
        }

        /// <summary>
        /// Occurs when the user selects an item from the TreeView 'tvRecipes'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRecipes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Obtain the selected tree view item.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_PARENT"))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Get the recipe from the recipeList and assign it to the txtRecipeData textbox.
                ExtendedRecipe recipe = recipeList[recipeIndex];
                txtRecipeData.Text = recipe.ToString();
            }

            // Check if the selected item is a recipe ingredient.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_INGREDIENT "))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');

                // Obtain the recipeIndex and ingredientIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);
                int ingredientIndex = Convert.ToInt32(parts[2]);

                // Get the recipe and the ingredient of the recipe.
                ExtendedRecipe recipe = recipeList[recipeIndex];
                RecipeIngredient ingredient = recipe.GetIngredients()[ingredientIndex];

                // Assign the ingredient data to the txtRecipeData textboxx.
                txtRecipeData.Text = ingredient.ToString();
            }

            // Check if the selected item is a  recipe instruction.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_INSTRUCTION "))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');

                // Obtain the recipeIndex and ingredientIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);
                int instructionIndex = Convert.ToInt32(parts[2]);

                // Get the recipe and the instruction of the recipe.
                ExtendedRecipe recipe = recipeList[recipeIndex];
                RecipeStep instruction = recipe.GetInstructions()[instructionIndex];

                // Assign the instruction text to the txtRecipeData textbox.
                txtRecipeData.Text = instruction.HelpText;
            }

        }

        /// <summary>
        /// Occurs when the user selects an item from the ListBox 'lbRecipeMenu'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbRecipeMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the selected index of the lbRecipeMenu listbox is not '-1'.
            if (lbRecipeMenu.SelectedIndex != -1)
            {
                // Obtain the selected item from the lbRecipeMenu control.
                string selectedItem = lbRecipeMenu.SelectedItem.ToString();

                // Check if the selected item is '<PARENT>'.
                if (selectedItem == "<PARENT>")
                {
                    // Display the statistics of the whole recipe menu.
                    UpdatePieChart(-1);
                }
                else
                {
                    // Display the statistics of the selected recipe in the recipe menu.
                    int recipeIndex = lbRecipeMenu.SelectedIndex - 1;
                    UpdatePieChart(recipeMenuList[recipeIndex]);
                }
            }
        }

        /// <summary>
        /// New Recipe menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Declare and instantiate a CreateRecipeDialog object.
            CreateRecipeDialog createRecipeDlg = new CreateRecipeDialog();
            // Assign the owner of the createRecipeDlg to the main window.
            createRecipeDlg.Owner = this;
            // Get the result of the createRecipeDlg.
            bool? dlgResult = createRecipeDlg.ShowDialog();

            // Check if the result was a success.
            if(dlgResult == true)
            {
                // Add a new recipe.
                string recipeName = createRecipeDlg.RecipeName;
                AddRecipe(recipeName);
            }
        }

        /// <summary>
        /// Scale Recipe menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item from the TreeView control.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selectedItem is the recipe itself.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_PARENT"))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Declare and instantiate a ScaleRecipeDialog object.
                ScaleRecipeDialog scaleRecipeDlg = new ScaleRecipeDialog();
                // Set the owner of the scaleRecipeDlg to the main window.
                scaleRecipeDlg.Owner = this;
                // Set the scale factor from the selected recipe.
                scaleRecipeDlg.RecipeScale = recipeList[recipeIndex].ScaleFactor;
                // Get the result from the scaleRecipeDlg.
                bool? dlgResult = scaleRecipeDlg.ShowDialog();

                // Check if the result was a success.
                if (dlgResult == true)
                {
                    // Update the scale factor of the recipe.
                    recipeList[recipeIndex].ScaleFactor = scaleRecipeDlg.RecipeScale;
                    // Clear the txtRecipeData TextBox
                    txtRecipeData.Text = string.Empty;

                    // Show success message.
                    MessageBox.Show("Successfully updated the recipe scale.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                // Show error message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Clear Recipe menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_PARENT"))
            {
                // Split the tag of the recipe into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Clear the recipe. (Backend)
                recipeList[recipeIndex].Clear();
                // Clear the txtRecipeData TextBox
                txtRecipeData.Text = string.Empty;

                // Refresh the recipe. (Frontend)
                DisplayRecipes();
            }
            else
            {
                // Show error message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Delete Recipe menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself.
            if (selectedItem.Tag.ToString().Contains("RECIPE_PARENT"))
            {
                // Split the tag of the recipe into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Remove the recipe from the recipeList at a given index. (Backend)
                recipeList.RemoveAt(recipeIndex);
                // Remove the recipe from the TreeView at a given index. (Frontend)
                tvRecipes.Items.RemoveAt(recipeIndex);
                // Clear the txtRecipeData TextBox. (Frontend)
                txtRecipeData.Text = string.Empty;
            }
            else
            {
                // Show warning message.
                MessageBox.Show("Unable to delete the recipe. No recipe selected.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Export Recipe menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item of the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_PARENT"))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Declare and instantiate a SaveFileDialog object
                // that will be the user-interface for the user
                // to save their recipe.
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                // Assign the filter to text format only.
                saveFileDialog1.Filter = "Text File|*.txt";
                // Get the result from the saveFileDialog object.
                bool? dlgResult = saveFileDialog1.ShowDialog();

                // Check if the result was a success.
                if (dlgResult == true)
                {
                    // Obtain the content from the recipe.
                    string content = recipeList[recipeIndex].ToString();
                    // Convert the content to an array of bytes.
                    byte[] contentBytes = Encoding.UTF8.GetBytes(content);
                    // Save the bytes to a file that was chosen from the
                    // SaveFileDialog object.
                    File.WriteAllBytes(saveFileDialog1.FileName, contentBytes);
                }
            }
            else
            {
                // Show warning message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// 'Add To Menu' menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToMenu_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_PARENT"))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);
                // States whether the recipe is found in the recipeMenuList.
                bool containsRecipe = recipeMenuList.Contains(recipeIndex);

                // Checks if the recipe is not found in the recipeMenuList.
                if (!containsRecipe)
                {
                    // Add the recipe to the recipeMenuList.
                    recipeMenuList.Add(recipeIndex);
                    // Display the menu recipes.
                    DisplayMenuRecipes();
                    // Update the pie chart.
                    UpdatePieChart(lbRecipeMenu.SelectedIndex);
                }
                else
                {
                    // Show warning message.
                    MessageBox.Show("The selected recipe is already included in the menu.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Show error message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// 'Remove From Menu' menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveFromMenu_Click(object sender, RoutedEventArgs e)
        {
            // Check if the selected index is greater than zero (Excluding the '<PARENT>' item).
            if (lbRecipeMenu.SelectedIndex > 0)
            {
                // Get the recipeIndex from the recipeMenuList.
                int recipeIndex = recipeMenuList[lbRecipeMenu.SelectedIndex - 1];
                // Remove the recipe from the menu at a given index.
                recipeMenuList.Remove(recipeIndex);
                // Refresh the recipe menu.
                DisplayMenuRecipes();
                // Display the statistics of the selected recipe to the user.
                UpdatePieChart(recipeIndex);
            }
            else if(lbRecipeMenu.SelectedIndex == 0)
            {
                // Show error message.
                MessageBox.Show("Cannot remove the item '<PARENT>'.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Show error message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Exit Application menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Add Ingredient menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item of the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself or 'RECIPE_INGREDIENTS'.
            if (selectedItem != null &&
                (selectedItem.Tag.ToString().Contains("RECIPE_PARENT ") ||
                selectedItem.Tag.ToString().Contains("RECIPE_INGREDIENTS")))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Declare and instantiate an AddIngredientDialog object.
                IngredientDialog ingredientDlg = new IngredientDialog();
                // Assign the owner of the ingredientDlg to the main window.
                ingredientDlg.Owner = this;
                // Set the dialog mode to 'Add'.
                ingredientDlg.DialogMode = RecipeDialogMode.Add;
                // Obtain the result from the ingredientDlg.
                bool? dlgResult = ingredientDlg.ShowDialog();

                // Check if the result succeeded.
                if (dlgResult == true)
                {
                    // Obtain the properties from the new recipe ingredient.
                    string ingredientName = ingredientDlg.IngredientName;
                    int ingredientQuantity = ingredientDlg.IngredientQuantity;
                    UnitMeasurement ingredientMeasurement = ingredientDlg.IngredientMeasurement;
                    int ingredientCalories = ingredientDlg.IngredientCalories;
                    FoodCategory ingredientFoodCategory = ingredientDlg.IngredientFoodGroup;

                    // Add the recipe ingredient to the list. (Backend)
                    RecipeIngredient ingredient = new RecipeIngredient(ingredientName, ingredientQuantity, ingredientMeasurement, ingredientCalories, ingredientFoodCategory);
                    recipeList[recipeIndex].AddIngredient(ingredient);
                    recipeList[recipeIndex].Validate();
                    
                    // Refresh the recipes. (Frontend)
                    DisplayRecipes();
                    // Display the statistics of the whole recipe menu.
                    UpdatePieChart(-1);
                    // Clear the txtRecipeData TextBox.
                    txtRecipeData.Text = string.Empty;
                }
            }
            else
            {
                // Show warning message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Add Insruction menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddInstruction_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself or 'RECIPE_INSTRUCTIONS'.
            if (selectedItem != null &&
                (selectedItem.Tag.ToString().Contains("RECIPE_PARENT ") ||
                selectedItem.Tag.ToString().Contains("RECIPE_INSTRUCTIONS")))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Declare and instantiate a InstructionDialog object.
                InstructionDialog instructionDlg = new InstructionDialog();
                // Set the owner of the instructionDlg to the main window.
                instructionDlg.Owner = this;
                // Set the dialog mode of the instructionDlg to 'Add';
                instructionDlg.DialogMode = RecipeDialogMode.Add;
                // Get the result from the instructionDlg.
                bool? dlgResult = instructionDlg.ShowDialog();

                // Check if the result succeeded.
                if (dlgResult == true)
                {
                    // Get the help text from the dialog.
                    string helpText = instructionDlg.HelpText;

                    // Declare and instantiate a RecipeStep object.
                    RecipeStep step = new RecipeStep(helpText);
                    recipeList[recipeIndex].AddInstruction(step);

                    // Refresh the TreeView.
                    DisplayRecipes();
                    // Clear the txtRecipeData TextBox.
                    txtRecipeData.Text = string.Empty;
                }
            }
            else
            {
                // Show a warning message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Remove Ingredient menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself or a recipe ingredient.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_INGREDIENT "))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex and ingredientIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);
                int ingredientIndex = Convert.ToInt32(parts[2]);

                // Remove the ingredient from the recipeList. (Backend).
                recipeList[recipeIndex].RemoveIngredient(ingredientIndex);
                recipeList[recipeIndex].Validate();
                // Display the recipes. (Frontend).
                DisplayRecipes();
            }
            else
            {
                // Show error message.
                MessageBox.Show("Please select a recipe ingredient to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Remove Instruction menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveInstruction_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is a recipe instruction.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_INSTRUCTION "))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex and instructionIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);
                int instructionIndex = Convert.ToInt32(parts[2]);

                // Remove the instruction from the recipeList. (Backend)
                recipeList[recipeIndex].RemoveInstruction(instructionIndex);
                // Display the recipes to the user. (Frontend).
                DisplayRecipes();
            }
            else
            {
                // Show error message.
                MessageBox.Show("Please select a recipe instruction to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Clear Ingredients menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearIngredients_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself or 'RECIPE_INGREDIENTS'.
            if (selectedItem != null &&
                (selectedItem.Tag.ToString().Contains("RECIPE_PARENT ") ||
                selectedItem.Tag.ToString().Contains("RECIPE_INGREDIENTS")))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Get the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Clear the ingredients from the recipe. (Backend)
                recipeList[recipeIndex].ClearIngredients();
                recipeList[recipeIndex].Validate();
                // Refresh the TreeView. (Frontend)
                DisplayRecipes();
            }
            else
            {
                // Show a warning message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Clear Instructions menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearInstructions_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is the recipe itself or 'RECIPE_INSTRUCTIONS'.
            if (selectedItem != null &&
                (selectedItem.Tag.ToString().Contains("RECIPE_PARENT") ||
                selectedItem.Tag.ToString().Contains("RECIPE_INSTRUCTIONS")))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Get the recipeIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);

                // Clear the instructions from the recipe. (Backend)
                recipeList[recipeIndex].ClearInstructions();
                // Refresh the TreeView. (Frontend)
                DisplayRecipes();
            }
            else
            {
                // Show a warning message.
                MessageBox.Show("Please select a recipe to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Update Ingredients menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is a recipe ingredient.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_INGREDIENT "))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex and ingredientIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);
                int ingredientIndex = Convert.ToInt32(parts[2]);

                // Declare and instantiate an IngredientDialog object.
                IngredientDialog ingredientDlg = new IngredientDialog();
                // Set the owner of the ingredientDlg to the main window.
                ingredientDlg.Owner = this;
                // Set the dialog mode of the ingredientDlg to 'Update'.
                ingredientDlg.DialogMode = RecipeDialogMode.Update;

                // Get an array of recipe ingredients from the selected recipe.
                RecipeIngredient[] ingredients = recipeList[recipeIndex].GetIngredients();
                // Get the selected ingredient from the recipe.
                RecipeIngredient ingredient = ingredients[recipeIndex];

                // Assign the details of the ingredient to the ingredientDlg.
                ingredientDlg.IngredientName = ingredient.Name;
                ingredientDlg.IngredientQuantity = ingredient.Quantity;
                ingredientDlg.IngredientMeasurement = ingredient.Measurement;
                ingredientDlg.IngredientCalories = ingredient.Calories;
                ingredientDlg.IngredientFoodGroup = ingredient.Category;

                // Get the result of the ingredientDlg.
                bool? dlgResult = ingredientDlg.ShowDialog();

                // Check if the result was a success.
                if (dlgResult == true)
                {
                    // Obtain the properties from the ingredientDlg.
                    string ingredientName = ingredientDlg.IngredientName;
                    int ingredientQuantity = ingredientDlg.IngredientQuantity;
                    UnitMeasurement ingredientMeasurement = ingredientDlg.IngredientMeasurement;
                    int ingredientCalories = ingredientDlg.IngredientCalories;
                    FoodCategory ingredientCategory = ingredientDlg.IngredientFoodGroup;

                    // Update the ingredient with the properties of the ingredientDlg.
                    this.recipeList[recipeIndex].UpdateIngredient(ingredientIndex,
                        new RecipeIngredient(ingredientName, ingredientQuantity, ingredientMeasurement, ingredientCalories, ingredientCategory));
                    this.recipeList[recipeIndex].Validate();

                    // Refresh the TreeView. (Frontend)
                    DisplayRecipes();
                    // Display the statistics of the selected recipe.
                    UpdatePieChart(lbRecipeMenu.SelectedIndex);

                    // Show success message.
                    MessageBox.Show("Successfully updated the selected ingredient.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                // Show a warning message.
                MessageBox.Show("Please select a recipe ingredient to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Update Instruction menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInstruction_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the TreeView.
            TreeViewItem selectedItem = (TreeViewItem)tvRecipes.SelectedItem;

            // Check if the selected item is a recipe instruction.
            if (selectedItem != null && selectedItem.Tag.ToString().Contains("RECIPE_INSTRUCTION "))
            {
                // Split the tag of the selected item into a string array.
                string[] parts = selectedItem.Tag.ToString().Split(' ');
                // Obtain the recipeIndex and instructionIndex from the string array.
                int recipeIndex = Convert.ToInt32(parts[1]);
                int instructionIndex = Convert.ToInt32(parts[2]);

                // Declare and instantiate a InstructionDialog object.
                InstructionDialog instructionDlg = new InstructionDialog();
                // Set the owner of the instructionDlg to the main window.
                instructionDlg.Owner = this;
                // Set the dialog mode of the instructionDlg to 'Update'.
                instructionDlg.DialogMode = RecipeDialogMode.Update;

                // Obtain the instructions from the recipeList.
                RecipeStep[] instructions = recipeList[recipeIndex].GetInstructions();
                // Get the selected instruction.
                RecipeStep instruction = instructions[instructionIndex];

                // Assign the HelpText of the instructionDlg.
                instructionDlg.HelpText = instruction.HelpText;
                // Get the dialog result.
                bool? dlgResult = instructionDlg.ShowDialog();

                // Check if the result was a success.
                if (dlgResult == true)
                {
                    // Get the HelpText from the instructionDlg.
                    string helpText = instructionDlg.HelpText;
                    // Update the instruction.
                    this.recipeList[recipeIndex].UpdateInstruction(instructionIndex, new RecipeStep(helpText));
                    // Refresh the TreeView. (Frontend)
                    DisplayRecipes();

                    // Show success message.
                    MessageBox.Show("Successfully updated the selected instruction.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                // Show a warning message.
                MessageBox.Show("Please select a recipe instruction to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// About RecipeApp menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutRecipeApp_Click(object sender, RoutedEventArgs e)
        {
            // Declare and instantiate an AboutDialog object.
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.Owner = this;
            // Show the dialog to the user.
            aboutDialog.ShowDialog();
        }
    }
}