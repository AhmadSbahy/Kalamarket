using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.ProductQuestions
{
    public class ProductQuestions
    {
        [Key]
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string QuestionsBody { get; set; }
        [Required]
        public DateTime QuestionDate { get; set; }
        public bool IsAnsewrd { get; set; } 

        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        [ForeignKey("ProductId")]
        public Product.Product? Product { get; set; }
        public ProductAnswer? ProductAnswers { get; set; }
    }
}
