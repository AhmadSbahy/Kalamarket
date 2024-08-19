using Kalamarket.Core.DTOs.Blog;
using Kalamarket.DataLayer.Eintitys.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Interface
{
	public interface IBlogSrvice
	{
		Tuple<List<IndexBlogViewModel>,int> GetAllBlogsForIndex(int pagecount = 1, int take = 0);
		List<NewBlogViewModel> GetNewBlogForSideBar();
		Blog GetBlogById(int blogId);
		int AddBlog(Blog blog);
		int AddComment(BlogComment comment);
		void DeleteComment(BlogComment comment);
		BlogComment GetCommentById(int id);
	}
}
