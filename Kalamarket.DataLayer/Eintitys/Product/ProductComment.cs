using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Product
{
    public class ProductComment
    {
        [Key]
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        [MaxLength(300)]
        [Required]
        public string CommentTitle { get; set; }
        [MaxLength(900)]
        [Required]
        public string CommentBody { get; set; }
        [MaxLength(900)]
        public string? GoodPoints { get; set; }
        [MaxLength(900)]
        public string? BadPoiant { get; set; }
        [Required]
        public DateTime CommentDate { get; set; }
        [Required]
        public bool IsRecommended { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [ForeignKey("UserId")]
        public User.User? User { get; set; }
    }
}
