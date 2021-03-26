using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItemExchange.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Type")]
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual ExpenseType Type { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 1")]
        public float Amount { get; set; }
    }
}
