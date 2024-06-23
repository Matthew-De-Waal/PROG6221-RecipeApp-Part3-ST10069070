using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    public enum InputDataType
    {
        Letters = 1,                        // Only alphabetical letters are allowed as input.
        Numbers = 2,                        // Only integers are allowed as input.
        LettersAndNumbers = 3,              // Only alphabetical letters and integers are allowed as input.
        LettersAndPositiveNumbers = 4,      // Only alphabetical letters and positive whole numbers are allowed as input.
        PositiveNumbers = 5,                // Only positive whole numbers are allowed as input.
        PositiveDecimals = 6                // Only positive decimal numbers are allowed as input.
    }
}
