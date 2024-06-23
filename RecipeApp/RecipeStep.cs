using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    /// <summary>
    /// This class represents a recipe step (instruction).
    /// </summary>
    public class RecipeStep
    {
        // Automatic Properties
        public string HelpText { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RecipeStep()
        {
            this.HelpText = string.Empty;
        }

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="helpText"></param>
        /// -------------------------------------------------------------------------
        public RecipeStep(string helpText)
        {
            this.HelpText = helpText;
        }
    }
}
