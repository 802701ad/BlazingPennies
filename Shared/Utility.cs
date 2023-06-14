using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Components;

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
            input = input.Replace("$", "");
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

        private static string BackendUrlDict(IConfiguration _configuration, string relativeUrl, Dictionary<string, string> additional_parameters = null)
        {
            if (additional_parameters == null) additional_parameters = new Dictionary<string, string>();
            // Get the root variable from the configuration
            string root = _configuration["backend_root"];
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


        /// <summary>
        /// Returns a backend URL with the given relative URL and additional parameters
        /// </summary>
        /// <param name="relativeUrl">The relative URL to append to the root</param>
        /// <param name="additional_parameters">An anonymous type with the additional parameters as properties</param>
        /// <returns>A string representing the backend URL</returns>
        public static string BackendUrl(IConfiguration _configuration, string relativeUrl, object additional_parameters = null)
        {
            // If the additional_parameters is a dictionary, call the other version of the function
            if(additional_parameters is Dictionary<string, string>)
            {
                return BackendUrlDict(_configuration, relativeUrl, (Dictionary<string, string>)additional_parameters);
            }
            // Convert the anonymous type to a dictionary
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (additional_parameters != null)
            {
                foreach (var property in additional_parameters.GetType().GetProperties())
                {
                    dictionary[property.Name] = property.GetValue(additional_parameters)?.ToString();
                }
            }
            // Call the first version of the function with the dictionary
            return BackendUrl(_configuration, relativeUrl, dictionary);
        }


        //takes a navigationmanager, a configuration manager, and a path and performs a navigate to based on the frontend_path in appsettings.json
        public static void NavigateToPath(NavigationManager navigationManager, IConfiguration configuration, string path, bool forceload=false)
        {
            //perform the navigation based on combining the paths with a uri builder
            //UriBuilder builder = new UriBuilder(new Uri(new Uri(configuration["frontend_root"]), path));
            //navigationManager.NavigateTo(builder.ToString(), forceload);
            string new_path = (configuration["frontend_root"] + path).Replace("//","/");
            navigationManager.NavigateTo(new_path, forceload);
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
