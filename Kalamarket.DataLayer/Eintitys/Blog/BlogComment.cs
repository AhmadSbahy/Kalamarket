using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Blog
{
    public class BlogComment
    {
        [Key]
        public int BC_Id { get; set; }
        public int BlogId { get; set; }
        public int UserId { get; set; }
        [Required]
        [MaxLength(990)]
        public string CommentBody { get; set; }
        public DateTime CreateDate { get; set; }

        public User.User User { get; set; }
        public Blog Blog { get; set; }
    }
}
