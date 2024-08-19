using Kalamarket.Core.Convartor;
using Kalamarket.Core.DTOs.Admin.Index;
using Kalamarket.Core.DTOs.Admin.Product;
using Kalamarket.Core.DTOs.Admin.Users;
using Kalamarket.Core.Generator;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Banners;
using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.DataLayer.Eintitys.Groups;
using Kalamarket.DataLayer.Eintitys.Order;
using Kalamarket.DataLayer.Eintitys.Product;
using Kalamarket.DataLayer.Eintitys.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.Core.Convartor;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kalamarket.Core.Service.Classes
{
	public class AdminSrvice : IAdminService
	{
		private KalamarketContext _context;
		private IUserSrvice _userSrvice;

		public AdminSrvice(KalamarketContext context, IUserSrvice user)
		{
			_context = context;
			_userSrvice = user;
		}

		public int AddProduct(Product product)
		{
			_context.Products.Add(product);

			_context.SaveChanges();

			return product.ProductId;
		}

		public void AddProductImage(List<IFormFile> files, int productId)
		{
			foreach (var item in files)
			{
				if (item != null && item.IsvalidImage())
				{

					var img = new ProductImage()
					{
						ImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(item.FileName),
						ProductId = productId
					};
					_context.ProductImages.Add(img);
					string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Product/Image", img.ImageName);

					using (var stream = new FileStream(imagePath, FileMode.Create))
					{
						item.CopyTo(stream);
					}

					ImageConvertor imageConverter = new ImageConvertor();
					string ThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Product/Thumb", img.ImageName);

					imageConverter.Image_resize(imagePath, ThumbPath, 300);
					_context.SaveChanges();
				}
			}
		}

		public int AddUserFromAdmin(AddUserFromAdminViewModel user)
		{
			Random random = new Random();

			int activationCode = random.Next(10000, 100000);
			User adduser = new User()
			{
				ActivCode = activationCode,
				Email = user.Email,
				FullName = user.FullName,
				IsActive = true,
				IsDelete = false,
				Password = PasswordHelper.EncodePasswordMd5(user.Password),
				UserName = user.Username,
				PhoneNumber = user.Phonenmumber,
				RigisterDate = DateTime.Now,
			};
			_context.Users.Add(adduser);
			_context.SaveChanges();

			return adduser.UserId;
		}

		public int DeleteProductImage(int productId)
		{
			var Product = _context.Products.IgnoreQueryFilters().Include(p => p.Images).SingleOrDefault(p => p.ProductId == productId);
			if (Product != null && Product.Images != null)
			{
				foreach (var item in Product.Images)
				{
					string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Product/Image", item.ImageName);
					string ThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Product/Thumb", item.ImageName);
					if (File.Exists(imagePath) && File.Exists(ThumbPath))
					{
						File.Delete(imagePath);
						File.Delete(ThumbPath);
					}
				}
				foreach (var item in Product.Images)
				{
					_context.ProductImages.Remove(item);
				}
			}


			return productId;
		}


		public void DeleteUserFromAdmin(int userId)
		{
			var User = _userSrvice.GetUserById(userId);
			User.IsDelete = true;
			_context.Users.Update(User);
			_context.Users.Update(User);
			_context.SaveChanges();
		}

		public void DeletProduct(int productId)
		{
			var Product = GetProductById(productId);
			Product.IsDeleted = true;
			_context.Products.Update(Product);
			_context.SaveChanges();
		}

		public List<SelectListItem> GetAllGroupForProduct()
		{
			return _context.Groups.Where(p => p.GroupParentId == null)
				.Select(p => new SelectListItem()
				{
					Text = p.GroupTitle,
					Value = p.GroupId.ToString()
				}).ToList();
		}

		public Tuple<List<ShowProductViewModel>, int> GetAllProductForAdmin(int pageId =1, int take = 1,string name = "", string ordertype = "")
		{
			if (take == 1)
				take = 15;
			IQueryable<Product> products = _context.Products
				.Include(x=>x.ProductDiscounts)
				.Include(p => p.User)
				.Include(p => p.Images);

			if (!string.IsNullOrEmpty(name))
			{
				products = products.Where(p => p.ProductName.Contains(name));
			}
			if (!string.IsNullOrEmpty(ordertype))
			{
				switch (ordertype)
				{
					case "Expensive":
						products = products.OrderByDescending(p => p.ProductPrice);
						break;
					case "Cheap":
						products = products.OrderBy(p => p.ProductPrice);
						break;
					case "MostSell":
						products = products.OrderByDescending(p => p.SellCount);
						break;
					case "LessSell":
						products = products.OrderBy(p => p.SellCount);
						break;
				}
			}
			int skip = (pageId - 1) * take;

			var qoury = products
				.Select(p => new ShowProductViewModel()
				{
					ProductId = p.ProductId,
					Name = p.ProductName,
					ProductPrice = p.ProductPrice,
					SellCount = p.SellCount,
					SellerName = p.User.FullName,
					ImageName = p.Images[0].ImageName,
					ProductDiscount = p.ProductDiscounts
				}).Skip(skip).Take(take).ToList();

			int pageCount = products
                .Select(p => new ShowProductViewModel()
                {
                    ProductId = p.ProductId,
                    Name = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    SellCount = p.SellCount,
                    SellerName = p.User.FullName,
                    ImageName = p.Images[0].ImageName,
                }).Count() / take;

			return Tuple.Create(qoury, pageCount);
        }

		public List<SelectListItem> GetAllSubGroupForProduct(int parentId)
		{
			return _context.Groups.Where(p => p.GroupParentId == parentId)
				.Select(p => new SelectListItem()
				{
					Text = p.GroupTitle,
					Value = p.GroupId.ToString()
				}).ToList();
		}

		public Tuple<List<UserListViewModel>,int> GetAllUserForAdmin(int pageId = 1,string email = "", string number = "",string userName = "",int take=1)
		{
			if (take == 1)
				take = 20;
			IQueryable<User> users = _context.Users;

			if (!string.IsNullOrEmpty(email))
			{
				users = users.Where(p => p.Email.Contains(email));
			}
			if (!string.IsNullOrEmpty(number))
			{
				users = users.Where(p => p.PhoneNumber.Contains(number));
			}
			if(!string.IsNullOrEmpty(userName))
			{
				users = users.Where(p => p.UserName.Contains(userName));
			}
			int skip = (pageId - 1) * take;
			var qoury = users.Select(p => new UserListViewModel()
			{
				Id = p.UserId,
				Email = p.Email,
				IsActive = p.IsActive,
				IsDelete = p.IsDelete,
				PhoneNumber = p.PhoneNumber,
				UserName = p.UserName
			}).Skip(skip).Take(take).ToList();
			int pageCount = users.Select(p => new UserListViewModel()
			{
				Id = p.UserId,
				Email = p.Email,
				IsActive = p.IsActive,
				IsDelete = p.IsDelete,
				PhoneNumber = p.PhoneNumber,
				UserName = p.UserName
			}).Count()/take +1;

			return Tuple.Create(qoury, pageCount);
		}

		public string GetPasswordUserById(int id)
		{
			return _context.Users.Where(p => p.UserId == id).Select(p => p.Password).First();
		}

		public Product GetProductById(int productid)
		{
			return _context.Products.Find(productid);
		}

		public DeleteProductViewModel GetProductForDelete(int productId)
		{
			var Product = _context.Products.Include(p => p.User).Include(p => p.Images).Where(p => p.ProductId == productId)
				.AsEnumerable();


			return Product.Select(p => new DeleteProductViewModel()
			{
				Id = p.ProductId,
				Image = p.Images[0].ImageName,
				Name = p.ProductName,
				Price = p.ProductPrice,
				//Group = GetGroupNameById(p.GroupId),
				//Sub = GetSubGroupNameById(p.SubGroup)
			}).First();
		}



		public User GetUserByIdForAdmin(int userId)
		{
			return _context.Users.Find(userId);
		}

		public void UpdateUserFromAdmin(User user)
		{
			_context.Users.Update(user);
			_context.SaveChanges();
		}

		public string GetUserFullNameByProductId(int sellerId)
		{
			return _context.Users.Single(p => p.UserId == sellerId).FullName;
		}

		public string GetGroupNameById(int id)
		{
			return _context.Groups.Single(g => g.GroupId == id).GroupTitle;
		}

		public string GetSubGroupNameById(int? id)
		{
			if (id != null)
			{
				return _context.Groups.Single(g => g.GroupParentId == id).GroupTitle;
			}
			else
			{
				return null;
			}
		}

		public List<ProductImage> GetProducImagetName(int productId)
		{
			return _context.ProductImages.Where(p => p.ProductId == productId).ToList();
		}

		public void UpdateProduct(Product product)
		{

			var existingProduct = _context.Products.Find(product.ProductId);
			if (existingProduct != null)
			{
				_context.Entry(existingProduct).CurrentValues.SetValues(product);
				_context.SaveChanges();
			}
		}

		public List<Group> GetAllGroupsForAdmin()
		{
			return _context.Groups.ToList();
		}

		public void AddGroup(Group group)
		{
			group.IsDeleted = false;
			_context.Groups.Add(group);
			_context.SaveChanges();
		}

		public Group GetGroupById(int groupId)
		{
			return _context.Groups.Find(groupId);
		}

		public void UpdateGroup(Group group)
		{
			_context.Groups.Update(group);
			_context.SaveChanges();
		}

		public bool IsEmailOrUsernameExist(string email, string username)
		{
			return _context.Users.Any(u => u.Email == FIxedText.FixEmailText(email) || u.UserName == username);
		}

		public Tuple<List<Discount>,int> GetAllDiscounts(int pageId = 1,int take =1)
		{
			if (take == 1)
				take = 20;
			int skip = (pageId - 1) * take;

			var qoury = _context.Discounts.Skip(skip).Take(take).ToList();

			int pagecount = _context.Discounts
				.Count() / take ;

			return Tuple.Create(qoury,pagecount);
        }

		public bool IsCodeExsist(string code, int discountid = 0)
		{
			return _context.Discounts.Any(p => p.DiscountId != discountid && p.DiscountCode == code);
		}

		public void AddDiscount(Discount discount)
		{
			_context.Discounts.Add(discount);
			_context.SaveChanges();
		}

		public Discount GetDiscountById(int id)
		{
			return _context.Discounts.Find(id);
		}

		public void UpdateDiscount(Discount discount)
		{
			_context.Discounts.Update(discount);
			_context.SaveChanges();
		}

		public void DeleteBlogImage(Blog blog)
		{
			string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Blog", blog.ImageName);
			if (File.Exists(imagePath))
			{
				File.Delete(imagePath);
			}
		}

		public List<Banner> GetAllBanners()
		{
			return _context.Banners.ToList();
		}

        public Tuple<List<Blog>,int> GetBlogs(int pageId = 1, int take = 1)
        {
			if (take == 1)
				take = 10;

			int skip = (pageId - 1) * take;

			int pageCount = _context.Blogs.Count() / take +1;

			var qoury = _context.Blogs.Include(p=> p.User).Skip(skip).Take(take).ToList();

			return Tuple.Create(qoury, pageCount);
        }

		public AdminIndexViewModel GetIndexViewModel()
		{
			var AdminIndex = new AdminIndexViewModel();
			AdminIndex.TotalRevenue = _context.Orders.Where(p=> p.IsFinaly).Sum(p => p.OrderSum);
			AdminIndex.TotalCustomer = _context.Users.Count();
			AdminIndex.TotalOrders = _context.Orders.Count();
			return AdminIndex;
		}

		public void AddProductDiscount(ProductDiscount productDiscount)
		{
			_context.ProductDiscounts.Add(productDiscount);
			_context.SaveChanges();
		}

		public ProductDiscount GetProductDiscountById(int discountId)
		{
			return _context.ProductDiscounts.Find(discountId);
		}

		public void UpdateProductDiscount(ProductDiscount productDiscount)
		{
			_context.ProductDiscounts.Update(productDiscount);
			_context.SaveChanges();
		}

		public void DeleteProductDiscount(int productDiscountId)
		{
			var productDiscount = _context.ProductDiscounts.Find(productDiscountId);
			_context.ProductDiscounts.Remove(productDiscount);
			_context.SaveChanges();
		}
	}
}