using Kalamarket.Core.DTOs.Index;
using Kalamarket.Core.DTOs.Product;
using Kalamarket.DataLayer.Eintitys.Banners;
using Kalamarket.DataLayer.Eintitys.ContactUs;
using Kalamarket.DataLayer.Eintitys.Groups;
using Kalamarket.DataLayer.Eintitys.Product;
using Kalamarket.DataLayer.Eintitys.ProductQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Interface
{
	public interface IProductService
	{
		#region Group
		List<IndexGroupSearch> GroupsSearches();
		List<Group> GetAllGroups();
		string GetGroupNameById(int groupid);
		string GetSubGroupNameById(int subid);
		#endregion

		#region Product
		List<IndexGroupsViewModel> indexGroupsViews();
		Product GetProductById(int productid);
		List<ProductCommentViewModel> GetProductComments(int productId);
		int GetProductSellerIdByProductId(int productId);
		void AddAnsewr(ProductAnswer answer);
		void IsAnserd(int qoustionId);
		void UpdateProduct(Product product);
		List<MyProductsViewModel> GetMyProducts(string userName);
		bool HasUserPurchasedProduct(string userName, int productId);
		#endregion

		#region Comment
		int AddComment(ProductComment comment);
		ProductComment GetCommentById(int commentId);
		int DeleteComment(int commentId);
        #endregion

        #region Qustion
		List<QoustionViewModel> GetQoustions(int productId);
		AnserViewModel GetQoustionsAnsewrById(int questionId);
		int AddQustion(ProductQuestions questions);
		int DeleteQustion(int qoustionid);
		List<ProductAnswer> GetAllAnsewrByQustionId(int qoustionid);
        int DeleteAnsewr(int id);

		List<Subject> GetSubjects();
		void AddContact(ContactUs contact);
		#endregion

		#region Archive
		Tuple<List<ShowProductListViewModel>, int> GetProducts(int pageId = 1, string filter = "",string getType="All",int minPrice=0,int maxPrice =0,List<int> selectedGroups=null,int take=0);
		#endregion
		
		#region Favorite
		void AddFavorite(FavoriteProduct favorite);
		void RemoveFavorite(int userId, int productId);
		bool IsFavoriteInUser(int userId,int productId);
		int GetFavoriteId(int userId,int productId);
        bool IsUserInThisProduct(int userid, int productid);
		#endregion

		#region Banner
		List<Banner> GetBanners();
		#endregion

		#region Order
		IndexOrderViewModel GetIndexOrder(string userName);
		#endregion
	}
}
