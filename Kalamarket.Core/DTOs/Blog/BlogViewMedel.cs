using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.DTOs.Blog
{
	public class IndexBlogViewModel
	{
		public int BlogId { get; set; }
		public int Views { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string SomeDescripton { get; set; }
        public DateTime CreateDate { get; set; }

    }
    public class NewBlogViewModel
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
