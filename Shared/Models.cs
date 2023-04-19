using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;


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

        [StringLength(12000, ErrorMessage = "Name is too long.")]
        public string? comment { get; set; }
    }

    public class Transaction
    {
        public Transaction()
        {
            // Initialize details to an empty list
            details = new List<TransactionDetail>();

            date = DateTime.Now;
        }

        [Required]
        public string user_id { get; set; }
        [Required]
        public string id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [Required]
        public string account_id { get; set; }
        [Required]
        public string name { get; set; }
        public string comment { get; set; }
        [Required]
        public string is_active { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal amount { get; set; }
        public List<TransactionDetail> details { get; set; }
    }

    public class TransactionDetail
    {
        [Required(ErrorMessage = "The transaction ID is required.")]
        public string transaction_id { get; set; }

        [Required(ErrorMessage = "The account ID is required.")]
        public string account_id { get; set; }

        [Required(ErrorMessage = "The fund ID is required.")]
        public string fund_id { get; set; }

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
