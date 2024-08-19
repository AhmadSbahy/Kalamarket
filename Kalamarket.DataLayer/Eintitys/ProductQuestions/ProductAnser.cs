using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.ProductQuestions
{
    public class ProductAnswer
    {
        [Key]
        public int AnserId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string AnserBody { get; set; }
        [Required]
        public DateTime AnswerDate { get; set; }
        [ForeignKey("UserId")]
        public User.User? User { get; set; }
        [ForeignKey("QuestionId")]
        public ProductQuestions? ProductQuestions { get; set; }
        [ForeignKey("ProductId")]
        public Product.Product? Product { get; set; }
    }
}
