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
    /// Interaction logic for AddInstructionDialog.xaml
    /// </summary>
    public partial class InstructionDialog : Window
    {
        /// <summary>
        /// The actual text of the instruction.
        /// </summary>
        public string HelpText
        {
            get
            {
                return txtHelpText.Text;
            }
            set
            {
                txtHelpText.Text = value;
            }
        }

        /// <summary>
        /// The type of dialog to show. Either 'Add' or 'Update'.
        /// </summary>
        public RecipeDialogMode DialogMode { get; set; }

        public InstructionDialog()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // Data validation
            bool success = txtHelpText.Text.Length > 0;

            if (success)
            {
                // Proceed with the request.
                this.DialogResult = true;
                this.Close();
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
            if (this.DialogMode == RecipeDialogMode.Add)
            {
                // Set the button caption to 'Add'.
                btnAccept.Content = "Add";
            }

            if (this.DialogMode == RecipeDialogMode.Update)
            {
                // Set the button caption to 'Update'.
                btnAccept.Content = "Update";
            }
        }
    }
}
