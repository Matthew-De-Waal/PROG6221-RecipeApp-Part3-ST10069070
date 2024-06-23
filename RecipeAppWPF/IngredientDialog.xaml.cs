using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
using System.Windows.Shapes;
using RecipeApp;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for AddIngredientDialog.xaml
    /// </summary>
    public partial class IngredientDialog : Window
    {
        /// <summary>
        /// THe name of the ingredient
        /// </summary>
        public string IngredientName
        {
            get
            {
                return txtIngredientName.Text;
            }
            set
            {
                txtIngredientName.Text = value;
            }
        }

        /// <summary>
        /// The quantity of the ingredient.
        /// </summary>
        public int IngredientQuantity
        {
            get
            {
                int quantity = 0;
                int.TryParse(txtQuantity.Text, out quantity);

                return quantity;
            }
            set
            {
                txtQuantity.Text = value.ToString(); 
            }
        }

        /// <summary>
        /// The unit measurement for the ingredient.
        /// </summary>
        public UnitMeasurement IngredientMeasurement
        {
            get
            {
                return (UnitMeasurement)cbUnitMeasurement.SelectedIndex + 1;
            }
            set
            {
                cbUnitMeasurement.SelectedIndex = (int)value - 1;
            }
        }

        /// <summary>
        /// The total number of calories for the ingredient.
        /// </summary>
        public int IngredientCalories
        {
            get
            {
                int calories = 0;
                int.TryParse(txtCalories.Text, out calories);

                return calories;
            }
            set
            {
                txtCalories.Text = value.ToString();
            }
        }

        /// <summary>
        /// The food category of the ingredient.
        /// </summary>
        public FoodCategory IngredientFoodGroup
        {
            get
            {
                return (FoodCategory)cbFoodGroup.SelectedIndex + 1;
            }
            set
            {
                cbFoodGroup.SelectedIndex = (int)value - 1;
            }
        }

        /// <summary>
        /// The type of dialog to show. Either 'Add' or 'Update'.
        /// </summary>
        public RecipeDialogMode DialogMode { get; set; }

        public IngredientDialog()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // Data validation
            bool success = txtIngredientName.Text.Length > 0 &&
                            txtQuantity.Text.Length > 0 &&
                            txtCalories.Text.Length > 0 &&
                            cbUnitMeasurement.SelectedIndex > -1 &&
                            cbFoodGroup.SelectedIndex > -1;

            if (success)
            {
                // Proceed with the request.
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                // Show a warning message.
                MessageBox.Show("Please complete the form to continue.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Cancel the request.
            this.DialogResult = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.DialogMode == RecipeDialogMode.Add)
            {
                // Set the button caption to 'Add'.
                btnAccept.Content = "Add";
            }

            if(this.DialogMode == RecipeDialogMode.Update)
            {
                // Set the button caption to 'Update'.
                btnAccept.Content = "Update";
            }    
        }
    }
}
