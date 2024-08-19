using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Blog
{
	public class Blog
	{
        [Key]
        public int BlogId { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        [MaxLength(600)]
        public string BlogTitle { get; set; }
		[Required]
		public string BlogDescription { get; set; }
        public int Views { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        public List<BlogComment>? BlogComments { get; set; }
    }
}
