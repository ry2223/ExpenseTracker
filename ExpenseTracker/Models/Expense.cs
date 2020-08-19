using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        [DisplayName("Item name")]
        public string ExpenseName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, 9999999999999999.99)]
        [DisplayName("Amount spent")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [DisplayName("Expense date")]
        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        [DisplayName("Item category")]
        public string Category { get; set; }
    }
}