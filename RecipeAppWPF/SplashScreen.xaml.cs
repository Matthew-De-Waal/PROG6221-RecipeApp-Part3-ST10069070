using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Declare and instantiate a Task object.
            Task t = new Task(RunApp);
            // Start the start asynchronously.
            t.Start();
            // Wait for the asynchronous task to finish.
            await t;

            // Declare and instantiate a MainWindow object.
            MainWindow w = new MainWindow();
            // Show the main window to the user.
            w.Show();
            // Close the splash screen.
            this.Close();
        }

        private void RunApp()
        {
            // Delay the execution by three seconds.
            Thread.Sleep(3000);
        }
    }
}
