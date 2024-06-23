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
using System.Windows.Shapes;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for CreateRecipeDialog.xaml
    /// </summary>
    public partial class CreateRecipeDialog : Window
    {
        /// <summary>
        /// Automatic Properties
        /// </summary>
        public string RecipeName
        {
            get
            {
                return txtRecipeName.Text;
            }
        }

        public CreateRecipeDialog()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Cancel the request.
            this.DialogResult = false;
            this.Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // Data validation
            bool success = txtRecipeName.Text.Length > 0;

            if (success)
            {
                // Proceed with the request.
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                // Show a warning message.
                MessageBox.Show("Please enter a name for the recipe.", "RecipeApp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
