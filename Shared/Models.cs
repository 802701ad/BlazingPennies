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
        public string user_id { get; set; }
        public string id { get; set; }
        public string date { get; set; }
        public string account_id { get; set; }
        public string name { get; set; }
        public string comment { get; set; }
        public string is_active { get; set; }
        public decimal amount { get; set; }
        public List<TransactionDetail> details { get; set; }
    }

    public class TransactionDetail
    {
        public string account_id { get; set; }
        public string fund_id { get; set; }
        public string description { get; set; }
        public decimal value { get; set; }
    }

}
//{ "user_id": "1", "id": "1", "name": "A", "comment": "comment" }
