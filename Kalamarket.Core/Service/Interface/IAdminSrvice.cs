using Kalamarket.Core.DTOs.Admin.Index;
using Kalamarket.Core.DTOs.Admin.Product;
using Kalamarket.Core.DTOs.Admin.Users;
using Kalamarket.DataLayer.Eintitys.Banners;
using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.DataLayer.Eintitys.Groups;
using Kalamarket.DataLayer.Eintitys.Order;
using Kalamarket.DataLayer.Eintitys.Product;
using Kalamarket.DataLayer.Eintitys.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Interface
{
	public interface IAdminService
	{
        #region User
        Tuple<List<UserListViewModel>, int> GetAllUserForAdmin(int pageId = 1, string email = "", string number = "", string userName = "", int take = 1);

		int AddUserFromAdmin(AddUserFromAdminViewModel user);
        User GetUserByIdForAdmin(int userId);
        void UpdateUserFromAdmin(User user);
        string GetPasswordUserById(int id);
        void DeleteUserFromAdmin(int userId);
        bool IsEmailOrUsernameExist(string email,string username);
        #endregion

        #region Product
        Product GetProductById(int productid);
        List<ProductImage> GetProducImagetName(int productId);
        Tuple<List<ShowProductViewModel>, int> GetAllProductForAdmin(int pageId = 1, int take = 1, string name = "", string ordertype = "");
        List<SelectListItem> GetAllGroupForProduct();
        List<SelectListItem> GetAllSubGroupForProduct(int parentId);
        int AddProduct(Product product);
        void AddProductImage(List<IFormFile> files, int productId);
        void DeletProduct(int productId);
        DeleteProductViewModel GetProductForDelete(int productId);
        int DeleteProductImage(int productId);
        string GetUserFullNameByProductId(int SellerId);
        void UpdateProduct(Product product);
        #endregion

        #region Group
        List<Group> GetAllGroupsForAdmin();
        void AddGroup(Group group);
        string GetGroupNameById(int id);
        string GetSubGroupNameById(int? id);
        Group GetGroupById(int groupId);
        void UpdateGroup(Group group);

        #endregion

        #region Discount
        Tuple<List<Discount>, int> GetAllDiscounts(int pageId = 1, int take = 1);
        bool IsCodeExsist(string code,int discountid = 0);
        void AddDiscount(Discount discount);
        Discount GetDiscountById(int id);
		void UpdateDiscount(Discount discount);
		void AddProductDiscount(ProductDiscount productDiscount);
		ProductDiscount GetProductDiscountById(int discountId);
		void UpdateProductDiscount(ProductDiscount productDiscount);
        void DeleteProductDiscount(int productDiscountId);
		#endregion

		#region Blog
        void DeleteBlogImage(Blog blog);
        Tuple<List<Blog>, int> GetBlogs(int pageId = 1,int take= 1);
        #endregion

        #region Banner
        List<Banner> GetAllBanners();
        #endregion

        #region Index
        AdminIndexViewModel GetIndexViewModel();
		#endregion
	}
}
