using Kalamarket.Core.DTOs.Blog;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Blog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Classes
{
	public class BlogSrvice : IBlogSrvice
	{
		private KalamarketContext _context;
		private IUserSrvice _userSrvice;

		public BlogSrvice(KalamarketContext context, IUserSrvice user)
		{
			_context = context;
			_userSrvice = user;
		}

        public int AddBlog(Blog blog)
        {
			_context.Add(blog);
			_context.SaveChanges();
			return blog.BlogId;
        }

        public int AddComment(BlogComment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
            return comment.BlogId;
        }

		public void DeleteComment(BlogComment comment)
		{
			_context.Remove(comment);
			_context.SaveChanges();
		}

		public Tuple<List<IndexBlogViewModel>, int> GetAllBlogsForIndex(int pagecount = 1,int take = 0)
		{
			if (take == 0)
				take = 6;

			int skip = (pagecount - 1) * take;

			int PageCount = _context.Blogs
				.AsEnumerable()
				.Select(b => new IndexBlogViewModel()
				{
					BlogId = b.BlogId,
					CreateDate = b.CreateDate,
					Title = b.BlogTitle,
					Image = b.ImageName,
					Views = b.Views,
					SomeDescripton = b.BlogDescription.Length > 300 ? b.BlogDescription.Substring(0, 300) : b.BlogDescription
				})
				.OrderByDescending(p => p.Views)
				.Count() / take;

			var blogs = _context.Blogs
				.AsEnumerable()
				.Select(b => new IndexBlogViewModel()
				{
					BlogId = b.BlogId,
					CreateDate = b.CreateDate,
					Title = b.BlogTitle,
					Image = b.ImageName,
					Views = b.Views,
					SomeDescripton = b.BlogDescription.Length > 300 ? b.BlogDescription.Substring(0, 300) : b.BlogDescription
				})
				.OrderByDescending(p=> p.Views)
				.Skip(skip).Take(take).ToList();


			return Tuple.Create(blogs, PageCount);

        }

        public Blog GetBlogById(int blogId)
        {
			return _context.Blogs.Include(p=> p.BlogComments).ThenInclude(p=> p.User).SingleOrDefault(p=> p.BlogId == blogId);
        }

		public BlogComment GetCommentById(int id)
		{
			return _context.BlogComments.Find(id);
		}

		public List<NewBlogViewModel> GetNewBlogForSideBar()
        {
			return _context.Blogs.Select(b => new NewBlogViewModel()
			{
				BlogId = b.BlogId,
				DateTime = b.CreateDate,
				ImageName = b.ImageName,
				Title = b.BlogTitle
			}).OrderByDescending(p=> p.DateTime).Take(15).ToList();
        }
    }
}
