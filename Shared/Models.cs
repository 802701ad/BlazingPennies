using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BlazingPennies.Shared.Models
{
    public class Account
    {
        public string? user_id { get; set; }
        public string? id { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Name is too long.")]
        public string? name { get; set; }

        [StringLength(12000, ErrorMessage = "Name is too long.")]
        public string? comment { get; set; }

        public decimal? balance;
    }

    public class Fund
    {
        public string? user_id { get; set; }
        public string? id { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Name is too long.")]
        public string? name { get; set; }

        [Range(0,1)]
        public int is_active { get; set; }
        public bool isActive { get { return is_active == 1; } set { is_active = value ? 1 : 0; } }

        [StringLength(12000, ErrorMessage = "Name is too long.")]
        public string? comment { get; set; }
    }

        public partial class Transaction
    {
        public Transaction()
        {
            // Initialize details to an empty list
            details = new List<TransactionDetail>();

            date = DateTime.Now;
            seq = DateTime.Now.Ticks.ToString().PadLeft(20, '0');
            isActive = true;
        }

        [Required]
        // Use the JsonPropertyName attribute to specify the JSON property name
        public string user_id { get; set; } = "";

        [Required]
        public string id { get; set; } = "";

        [Required]
        //[JsonConverter(typeof(CustomDateConverter))]
        public DateTime date { get; set; }

        public string seq { get; set; }

        [Required]
        public string account_id { get; set; } = "";

        [Required]        
        public string name { get; set; }

        
        public string comment { get; set; }
        
        //[JsonIgnore]
        public string is_active {
            get { return isActive ? "1" : "0"; }
            set { isActive = (value is "-1" or "1"|| value.ToLower() is "y" or "t" or "true" or "yes"); }
        }

        [JsonIgnore]
        //[JsonConverter(typeof(CustomBoolConverter))]
        //[JsonPropertyName("is_active")]  
        public bool isActive;

        [Required]
        [Range(0, double.MaxValue)]
        // Use the JsonConverter attribute to specify a custom converter for amount
        public decimal amount { get; set; }

        // Use the JsonIgnore attribute to exclude details from serialization/deserialization
        //[JsonIgnore]
        public List<TransactionDetail> details { get; set; }

    }
    #region
    public class CustomDateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Try to parse the date as ISO 8601 format with time zone offset
            if (DateTimeOffset.TryParse(reader.GetString(), out DateTimeOffset date))
            {
                return date.DateTime;
            }
            // If that fails, try to parse the date as ISO 8601 format without time zone offset
            else if (DateTime.TryParse(reader.GetString(), out DateTime date2))
            {
                return date2;
            }
            // If that fails, throw an exception
            else
            {
                throw new JsonException("Invalid date format.");
            }

        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Write the date as ISO 8601 format with time zone offset
            writer.WriteStringValue(value.ToString("o"));
        }
    }

    // Define a custom converter for is_active that can handle different values
    public class CustomBoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Get the value as a string
            string value = reader.GetString();

            // Check if the value is one of the accepted values for true
            if (value == "-1" || value == "1" || value.ToLower() == "y" || value.ToLower() == "t" || value.ToLower() == "true" || value.ToLower() == "yes")
            {
                return true;
            }
            // Check if the value is one of the accepted values for false
            else if (value == "0" || value.ToLower() == "n" || value.ToLower() == "f" || value.ToLower() == "false" || value.ToLower() == "no")
            {
                return false;
            }
            // If not, throw an exception
            else
            {
                throw new JsonException("Invalid bool format.");
            }

        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            // Write the bool as a string ("Y" or "N")
            writer.WriteStringValue(value ? "1" : "0");
        }
    }

   
    

#endregion

public class TransactionDetail
    {
        [Required(ErrorMessage = "The transaction ID is required.")]
        public string transaction_id { get; set; }

        [Required(ErrorMessage = "The account ID is required.")]
        public string account_id { get; set; }

        [Required(ErrorMessage = "The fund ID is required.")]
        public string fund_id { get; set; }

        public bool _fund_is_active=true;
        public string fund_is_active
        {
            get { return _fund_is_active ? "1" : "0"; }
            set { _fund_is_active = (value is "-1" or "1" || value.ToLower() is "y" or "t" or "true" or "yes"); }
        }
        public string fund_title { get; set; }

        [StringLength(500, ErrorMessage = "The description is too long.")]
        public string description { get; set; }

        [Required(ErrorMessage = "The value is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The value must be greater than or equal to zero.")]
        public decimal value { get; set; }
    }

    public class User
    {
        private string? _userId;
        private string? _pass;
        private string? _user;
        private string? _email;
        private string? _firstName;
        private string? _lastName;

        public string USER_ID
        {
            get { return _userId ?? ""; }
            set { _userId = value; }
        }

        public string PASS
        {
            get { return _pass ?? ""; }
            set { _pass = value; }
        }

        public string user
        {
            get { return _user ?? ""; }
            set { _user = value; }
        }

        public string EMAIL
        {
            get { return _email ?? ""; }
            set { _email = value; }
        }

        public string FIRST_NAME
        {
            get { return _firstName ?? ""; }
            set { _firstName = value; }
        }

        public string LAST_NAME
        {
            get { return _lastName ?? ""; }
            set { _lastName = value; }
        }
    }
}
//{ "user_id": "1", "id": "1", "name": "A", "comment": "comment" }
