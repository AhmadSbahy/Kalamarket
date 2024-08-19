using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kalamarket.Web.Controllers
{
	public class BlogController : Controller
	{
		private readonly IBlogSrvice _blogSrvice;
		private readonly IUserSrvice _userSrvice;
        public BlogController(IBlogSrvice blogSrvice,IUserSrvice user)
        {
            _blogSrvice = blogSrvice;
			_userSrvice = user;
        }
        public IActionResult Index(int pageId = 1,int take=0)
		{
            ViewBag.PageId = pageId;
            return View(_blogSrvice.GetAllBlogsForIndex(pageId,take));
		}
		[Route("SingleBlog/{id}")]
		public IActionResult SingleBlog(int id)
		{
			return View(_blogSrvice.GetBlogById(id));
		}
		[Authorize]
		public IActionResult AddComment(BlogComment comment)
		{
			int UserId = _userSrvice.GetUserIdByUserName(User.Identity.Name);
			BlogComment addcom = new BlogComment()
			{
				CommentBody = comment.CommentBody,
				CreateDate = DateTime.Now,
				UserId = UserId,
				BlogId = comment.BlogId
			};
			int blogId = _blogSrvice.AddComment(addcom);
			
			return Redirect($"/SingleBlog/{blogId}");
		}
		[Authorize]
		[Route("DeleteComment/{comId}/{blogId}")]
		public IActionResult DeleteComment(int comId,int blogId)
		{
			int UserId = _userSrvice.GetUserIdByUserName(User.Identity.Name);
			var comment = _blogSrvice.GetCommentById(comId);
			if (comment.UserId == UserId)
			{
				_blogSrvice.DeleteComment(comment);
				return Redirect($"/SingleBlog/{blogId}");
			}
			else
			{
				return BadRequest();
			}	
		}
	}
}
