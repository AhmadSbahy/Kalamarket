using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Banners
{
	public class Banner
	{
        [Key]
        public int BannerId { get; set; }
        [Required]
        public string ImageName { get; set; }
		[Required]
		public string ImageUrl { get; set; }

    }
}
