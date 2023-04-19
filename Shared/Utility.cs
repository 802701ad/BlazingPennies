using System.Text.RegularExpressions;

namespace BlazingPennies.Shared
{
    public class Utility
    {
        /// <summary>
        /// This function takes a string as an input, extracts all the numbers from it and sums them up
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The sum of all the numbers in the input string</returns>
        /// <remarks>
        /// To escape a number that you don't want to add, use a backslash (\\) before it.
        /// </remarks>
        public static decimal SumNumbers(string? input)
        {
            if (input == null) return 0;
            // Use a regular expression to match all the decimal numbers in the input, except those preceded by a backslash
            MatchCollection matches = Regex.Matches(input, @"(?<!\\)-?\d+(?:\.\d+)?");

            // Initialize a variable to store the sum
            decimal sum = 0;

            // Loop through the matches and convert them to decimals
            foreach (Match match in matches)
            {
                decimal number = decimal.Parse(match.Value);

                // Add the number to the sum
                sum += number;
            }

            // Return the sum
            return sum;
        }
    }

}
