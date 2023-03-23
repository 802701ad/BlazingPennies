using System.ComponentModel.DataAnnotations;


namespace BlazTest.Shared.Models
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

        [StringLength(12000, ErrorMessage = "Name is too long.")]
        public string? comment { get; set; }
    }

    public class Transaction
    {
        [Required]
        public string user_id { get; set; }
        [Required]
        public string id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string date { get; set; }
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

        [StringLength(500, ErrorMessage = "The description is too long.")]
        public string description { get; set; }

        [Required(ErrorMessage = "The value is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The value must be greater than or equal to zero.")]
        public decimal value { get; set; }
    }

    public class User
    {
        public string USER_ID { get; set; }
        public string PASS { get; set; }
        public string user { get; set; }
        public string EMAIL { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }
}
//{ "user_id": "1", "id": "1", "name": "A", "comment": "comment" }
