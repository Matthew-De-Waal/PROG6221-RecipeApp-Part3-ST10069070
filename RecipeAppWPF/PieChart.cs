using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RecipeAppWPF
{
    public class PieChart
    {
        /// <summary>
        /// Automatic Properties
        /// </summary>
        public Canvas Canvas { get; set; }
        public Color[] Colors { get; set; }
        public float[] Percentages { get; set; }
        public Point Origin { get; set; }
        private int Radius { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PieChart() { }

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="canvas">The visual control that will be used to draw the pie chart.</param>
        public PieChart(Canvas canvas)
        {
            Canvas = canvas;
        }

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="canvas">The visual control that will be used to draw the pie chart.</param>
        /// <param name="colors">The array of colors for the different categories.</param>
        /// <param name="percentages">The array of percentages for the different categories.</param>
        /// <param name="origin">The 2D point of where the circle begins.</param>
        /// <param name="radius">The radius of the cirlce.</param>
        public PieChart(Canvas canvas, Color[] colors, float[] percentages, Point origin, int radius)
        {
            Canvas = canvas;
            Colors = colors;
            Percentages = percentages;
            Origin = origin;
            Radius = radius;
        }

        /// <summary>
        /// Checks if the given array are all zero values.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private bool isEmptyArray(int[] array)
        {
            bool isEmpty = true;

            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                    isEmpty = false;
            }

            return isEmpty;
        }

        /// <summary>
        /// Draws the pie chart.
        /// </summary>
        public void DrawChart()
        {
            // Clear the canvas
            Canvas.Children.Clear();

            // Declare an integer array.
            int[] angles = new int[Percentages.Length];
            
            // Calculate the angle values and store it in the angles array.
            for(int i = 0; i < angles.Length; i++)
            {
                angles[i] = (int)(Percentages[i] / 100 * 360);
            }

            // Check if the angle array is not empty.
            if (!isEmptyArray(angles))
            {
                // This variable keeps track of the drawing position.
                int position = 0;

                // Outer loop to handle the drawing process.
                for (int i = 0; i < angles.Length; i++)
                {
                    // Inner loop to handle the drawing process.
                    for (int j = 0; j <= angles[i]; j++)
                    {
                        // Trigonomeric functions 'Sin' and 'Cos' are used to calculate the
                        // x and y cooridinates of a single line to be drawn.
                        int x = (int)(Math.Cos((double)((Math.PI / 180) * (j + position))) * Radius);
                        int y = (int)(Math.Sin((double)((Math.PI / 180) * (j + position))) * Radius);

                        // This object represents a single line of a circle.
                        Line line = new Line();
                        line.X1 = Origin.X;
                        line.Y1 = Origin.Y;
                        line.X2 = Origin.X + x;
                        line.Y2 = Origin.Y - y;
                        line.Fill = new SolidColorBrush(Colors[i]);
                        line.Stroke = new SolidColorBrush(Colors[i]);
                        line.StrokeThickness = 3;

                        // Add the line to the canvas.
                        Canvas.Children.Add(line);
                    }

                    // Update the drawing position.
                    position += angles[i];
                }
            }
        }
    }
}
