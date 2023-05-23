using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

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
            //MatchCollection matches = Regex.Matches(input, @"(?<!\\)-?\d+(?:\.\d+)?");
            //letters must be separated from numbers by punctuation
            MatchCollection matches = Regex.Matches(input, @"(?<!\\)(?<!\w)-?\d+(?:\.\d+)?(?!\w)");

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

        public static string BackendUrl(string relativeUrl, Dictionary<string, string> additional_parameters=null)
        {
            if (additional_parameters==null) additional_parameters = new Dictionary<string, string>();
            string root = "https://pennypincher.x10.bz/pennydev/";
            // Create a UriBuilder object from the root and relativeUrl
            UriBuilder builder = new UriBuilder(new Uri(new Uri(root), relativeUrl));
            // Get the query string from the builder as a NameValueCollection
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            // Loop through the additional_parameters and add or update them in the query
            foreach (var pair in additional_parameters)
            {
                query[pair.Key] = pair.Value;
            }
            // Set the query back to the builder
            builder.Query = query.ToString();
            // Return the absolute URL from the builder
            return builder.ToString();
        }

        public static string FormatJson(string json)
        {
            try
            {
                JToken.Parse(json);
                return JValue.Parse(json).ToString(Formatting.Indented);
            }
            catch (JsonReaderException)
            {
                return "";
            }
        }
    }

    

}
