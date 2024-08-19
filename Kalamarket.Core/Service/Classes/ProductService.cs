using Kalamarket.Core.DTOs.Index;
using Kalamarket.Core.DTOs.Product;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Banners;
using Kalamarket.DataLayer.Eintitys.ContactUs;
using Kalamarket.DataLayer.Eintitys.Groups;
using Kalamarket.DataLayer.Eintitys.Product;
using Kalamarket.DataLayer.Eintitys.ProductQuestions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Classes
{
    public class ProductSrvice : IProductService
    {
        private KalamarketContext _context;
        private IUserSrvice _userSrvice;

        public ProductSrvice(KalamarketContext context, IUserSrvice user)
        {
            _context = context;
            _userSrvice = user;
        }

        public List<Group> GetAllGroups()
        {
            return _context.Groups.ToList();
        }

        public List<IndexGroupSearch> GroupsSearches()
        {
            return _context.Groups.Select(g => new IndexGroupSearch()
            {
                GroupId = g.GroupId,
                GroupName = g.GroupTitle
            }).ToList();
        }

        public List<IndexGroupsViewModel> indexGroupsViews()
        {
            var groups = _context.Groups.Where(g => g.GroupParentId == null).ToList();

            List<IndexGroupsViewModel> result = new List<IndexGroupsViewModel>();

            foreach (var group in groups)
            {
                var products = _context.Products
	                .Include(p=>p.ProductDiscounts)
	                .Where(p => p.GroupId == group.GroupId)
                    .Select(p => new ProductBoxViewModel
                    {
                        FirstImage = p.Images.FirstOrDefault().ImageName,
                        Id = p.ProductId,
                        Price = p.ProductPrice.ToString("#,0"),
                        ProductName = p.ProductName,
                        GroupId = p.GroupId,
                        ProductId = p.ProductId,
                        ProductDiscount = p.ProductDiscounts
                    }).ToList();

                result.Add(new IndexGroupsViewModel
                {
                    GroupName = group.GroupTitle,
                    Products = products
                });
            }

            return result;
        }
        public Product GetProductById(int productid)
        {
            return _context.Products.Include(p => p.Images)
                .Include(p => p.Group)
                .Include(p => p.User)
                .Include(p=> p.ProductDiscounts)
                .Include(x=>x.ProductDiscounts)
                .SingleOrDefault(p => p.ProductId == productid);
        }

        public string GetGroupNameById(int groupid)
        {
            return _context.Groups.Where(p => p.GroupParentId == null && p.GroupId == groupid).First().GroupTitle;
        }

        public string GetSubGroupNameById(int subid)
        {
            return _context.Groups.Where(p => p.GroupParentId != null && p.GroupId == subid).First().GroupTitle;
        }

        public List<ProductCommentViewModel> GetProductComments(int productId)
        {
            var comments = _context.ProductComments
                    .Where(p => p.ProductId == productId)
                    .OrderByDescending(p => p.CommentDate)
                    .Select(p => new ProductCommentViewModel
                    {
                        Title = p.CommentTitle,
                        Body = p.CommentBody,
                        CommentId = p.CommentId,
                        IsRecomended = p.IsRecommended,
                        Name = _userSrvice.GetUserFullNameById(p.UserId),
                        BadPoints = p.BadPoiant,
                        GoodPoints = p.GoodPoints,
                        CreateDate = p.CommentDate
                    })
                    .ToList();

            return comments;


        }

        public int AddComment(ProductComment comment)
        {
            _context.ProductComments.Add(comment);
            _context.SaveChanges();
            return comment.ProductId;
        }

        public ProductComment GetCommentById(int commentId)
        {
            return _context.ProductComments.Find(commentId);
        }

        public int DeleteComment(int commentId)
        {
            var Comment = GetCommentById(commentId);
            _context.ProductComments.Remove(Comment);
            _context.SaveChanges();
            return Comment.ProductId;
        }

        public List<QoustionViewModel> GetQoustions(int productId)
        {
            return _context.ProductQuestions.Include(u => u.User).Where(q => q.ProductId == productId)
                .OrderByDescending(q => q.QuestionDate)
                .Select(q => new QoustionViewModel()
                {
                    Body = q.QuestionsBody,
                    Date = q.QuestionDate,
                    Id = q.QuestionId,
                    Name = q.User.FullName,
                    IsAnswerd = q.IsAnsewrd
                })
                .ToList();
        }

        public AnserViewModel GetQoustionsAnsewrById(int questionId)
        {
            return _context.ProductAnswers.Include(p => p.User).Where(p => p.QuestionId == questionId)
                .OrderByDescending(p => p.AnswerDate)
                .Select(p => new AnserViewModel()
                {
                    Body = p.AnserBody,
                    Date = p.AnswerDate,
                    Id = p.AnserId,
                    Name = p.User.FullName
                }).FirstOrDefault();

        }

        public int AddQustion(ProductQuestions questions)
        {
            _context.ProductQuestions.Add(questions);
            _context.SaveChanges();
            return questions.ProductId;
        }

        public int DeleteQustion(int qoustionid)
        {
            var qoustinAnsewrs = GetAllAnsewrByQustionId(qoustionid);

            if (qoustinAnsewrs != null)
            {
                foreach (var item in qoustinAnsewrs)
                {
                    _context.ProductAnswers.Remove(item);
                }
            }

            var qoustion = _context.ProductQuestions.Find(qoustionid);
            _context.ProductQuestions.Remove(qoustion);
            _context.SaveChanges();

            return qoustion.ProductId;
        }

        public List<ProductAnswer> GetAllAnsewrByQustionId(int qoustionid)
        {
            return _context.ProductAnswers.Where(p => p.QuestionId == qoustionid)
                .ToList();
        }

        public int DeleteAnsewr(int id)
        {
            var anser = _context.ProductAnswers.Include(p => p.ProductQuestions).Where(p => p.AnserId == id).FirstOrDefault();

            _context.ProductAnswers.Remove(anser);
            anser.ProductQuestions.IsAnsewrd = false;

            _context.SaveChanges();
            return anser.ProductId;
        }

        public Tuple<List<ShowProductListViewModel>, int> GetProducts(int pageId = 1, string filter = "", string getType = "All", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
                take = 12;

            IQueryable<Product> products = _context.Products;

            if (!string.IsNullOrEmpty(filter))
            {
                products = products.Where(p => p.ProductName.Contains(filter) || p.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "All":
                    break;
                case "Cheap":
                    products = products.OrderBy(p => p.ProductPrice);
                    break;
                case "Expinsive":
                    products = products.OrderByDescending(p => p.ProductPrice);
                    break;
                case "BeastSell":
                    products = products.OrderByDescending(p => p.SellCount);
                    break;
            }
            if(minPrice != 0)
            {
                products = products.Where(p=> p.ProductPrice <= minPrice);
            }
            if(maxPrice != 0)
            {
                products = products.Where(p=> p.ProductPrice >= maxPrice);
            }
			if (selectedGroups != null && selectedGroups.Any())
			{
				List<int> groupIds = new List<int>(); // ایجاد یک لیست برای ذخیره Id ها

				foreach (int Id in selectedGroups)
				{
					// اضافه کردن Id ها به لیست
					groupIds.Add(Id);
				}

				// استفاده از دستور Where برای انتخاب محصولات مربوط به Id های انتخاب شده
				products = products.Where(p => groupIds.Contains(p.GroupId) || groupIds.Contains((int)p.SubGroup));
			}
			int skip = (pageId - 1) * take;

            int PageCount = products.Include(p => p.Images)
                .AsEnumerable()
                .Select(p => new ShowProductListViewModel()
                {
                    ImageName = p.Images[0].ImageName,
                    Price = p.ProductPrice,
                    ProductId = p.ProductId,
                    ProductName = p.ProductName
                }).Count() / take;

            var qurey = products
	            .Include(p=>p.ProductDiscounts)
	            .Include(p => p.Images)
                .AsEnumerable()
                .Select(p => new ShowProductListViewModel()
                {
                    ImageName = p.Images[0].ImageName,
                    Price = p.ProductPrice,
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDiscount = p.ProductDiscounts
                }).Skip(skip).Take(take).ToList();

            return Tuple.Create(qurey,PageCount);
        }

        public void AddFavorite(FavoriteProduct favorite)
        {
            _context.FavoriteProducts.Add(favorite);
            _context.SaveChanges();
        }

        public bool IsUserInThisProduct(int userid, int productid)
        {
            return _context.FavoriteProducts.Any(p => p.UserId == userid && p.ProductId == productid);
        }

        public void RemoveFavorite(int userId,int productId)
        {
            int FavoriteId = GetFavoriteId(userId,productId);
            var fav = _context.FavoriteProducts.Find(FavoriteId);
            _context.FavoriteProducts.Remove(fav);
            _context.SaveChanges();
        }

        public bool IsFavoriteInUser(int userId, int productId)
        {
            return _context.FavoriteProducts.Any(p=> p.UserId == userId && p.ProductId == productId);
        }

        public int GetFavoriteId(int userId, int productId)
        {
            return _context.FavoriteProducts.Where(p => p.ProductId == productId && p.UserId == userId).First().FavoriteId;
        }

        public List<Subject> GetSubjects()
        {
            return _context.Subjects.ToList();
        }

        public void AddContact(ContactUs contact)
        {
            _context.Add(contact);
            _context.SaveChanges();
        }

        public List<Banner> GetBanners()
        {
            return _context.Banners.ToList();
        }

		public int GetProductSellerIdByProductId(int productId)
		{
            return _context.Products.SingleOrDefault(p => p.ProductId == productId).SellerId;
		}

		public void AddAnsewr(ProductAnswer answer)
		{
            _context.ProductAnswers.Add(answer);
            _context.SaveChanges();
		}

		public void IsAnserd(int qoustionId)
		{
            var qoustion = _context.ProductQuestions.Find(qoustionId);
            if(qoustion != null)
            {
				qoustion.IsAnsewrd = true;
                _context.ProductQuestions.Update(qoustion);
                _context.SaveChanges();
            }
		}

		public IndexOrderViewModel GetIndexOrder(string userName)
		{
			int userId = _userSrvice.GetUserIdByUserName(userName);
			var Order = _context.Orders.FirstOrDefault(p => !p.IsFinaly && p.UserId == userId);
			var IndexOrderViewModel = new IndexOrderViewModel();

			if (Order == null)
			{
				IndexOrderViewModel.Price = 0;
				IndexOrderViewModel.Count = 0;
			}
			else
			{
				IndexOrderViewModel.Price = (int)Order.OrderSum;
				IndexOrderViewModel.Count = Order.OrderDetails?.Count() ?? 0;
			}

			return IndexOrderViewModel;
		}

		public void UpdateProduct(Product product)
		{
			_context.Products.Update(product);
            _context.SaveChanges();
		}

		public List<MyProductsViewModel> GetMyProducts(string userName)
		{
			int userId = _userSrvice.GetUserIdByUserName(userName);

			return _context.Products.Where(p => p.SellerId == userId)
				.Select(x => new MyProductsViewModel()
				{
					Price = x.ProductPrice,
					ImageName = x.Images[0].ImageName,
					ProductId = x.ProductId,
					ProductName = x.ProductName,
				}).ToList();
		}

		public bool HasUserPurchasedProduct(string userName, int productId)
		{
			int userId = _userSrvice.GetUserIdByUserName(userName);
			var hasPurchased = _context.UserOrders
				.Include(uo => uo.Order)
				.ThenInclude(o => o.OrderDetails)
				.Any(uo => uo.UserId == userId &&
				           uo.Order.OrderDetails.Any(od => od.ProductId == productId));

			return hasPurchased;
		}
	}
}