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
    /// Interaction logic for ScaleRecipeDialog.xaml
    /// </summary>
    public partial class ScaleRecipeDialog : Window
    {
        /// <summary>
        /// Automatic properties
        /// </summary>
        public float RecipeScale
        {
            get
            {
                switch(srRecipeScale.Value)
                {
                    case 0:
                        return 0.5f;

                    case 1:
                        return 1.0f;

                    case 2:
                        return 2.0f;

                    case 3:
                        return 3.0f;

                    default:
                        return 1.0f;
                }
            }
            set
            {
                switch(value)
                {
                    case 0.5f:
                        srRecipeScale.Value = 0;
                        break;

                    case 1.0f:
                        srRecipeScale.Value = 1;
                        break;

                    case 2.0f:
                        srRecipeScale.Value = 2;
                        break;

                    case 3.0f:
                        srRecipeScale.Value = 3;
                        break;
                }
            }
        }

        public ScaleRecipeDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Cancel the request.
            this.DialogResult = false;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Proceed with the request.
            this.DialogResult = true;
            this.Close();
        }
    }
}
