using Kalamarket.DataLayer.Eintitys.Address;
using Kalamarket.DataLayer.Eintitys.Banners;
using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.DataLayer.Eintitys.ContactUs;
using Kalamarket.DataLayer.Eintitys.Groups;
using Kalamarket.DataLayer.Eintitys.Order;
using Kalamarket.DataLayer.Eintitys.Permission;
using Kalamarket.DataLayer.Eintitys.Product;
using Kalamarket.DataLayer.Eintitys.ProductQuestions;
using Kalamarket.DataLayer.Eintitys.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Context
{
	public class KalamarketContext : DbContext
	{
		public KalamarketContext(DbContextOptions<KalamarketContext> options) : base(options)
		{

		}
		#region User
		public DbSet<User> Users { get; set; }
		public DbSet<Address> Addresses { get; set; }
		#endregion

		#region Product
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductComment> ProductComments { get; set; }
		public DbSet<ProductAnswer> ProductAnswers { get; set; }
		public DbSet<ProductQuestions> ProductQuestions { get; set; }
		public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
		public DbSet<ProductDiscount> ProductDiscounts { get; set; }
		#endregion

		#region Group
		public DbSet<Group> Groups { get; set; }
		#endregion

		#region Contact
		public DbSet<ContactUs> ContactUs { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		#endregion

		#region Order
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<UserOrder> UserOrders { get; set; }
		public DbSet<OrderStatus> OrderStatus { get; set; }
		#endregion

		#region Discount
		public DbSet<UserDiscounts> UserDiscounts { get; set; }
		public DbSet<Discount> Discounts { get; set; }
		#endregion

		#region Blogs
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<BlogComment> BlogComments { get; set; }
		#endregion

		#region Banners
		public DbSet<Banner> Banners { get; set; }
		#endregion

		#region MyRegion
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<RolePermission> RolePermissions { get; set; }

		#endregion


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>()
				.HasQueryFilter(u => !u.IsDelete);

			modelBuilder.Entity<Product>()
							.HasQueryFilter(p => !p.IsDeleted);
			
			modelBuilder.Entity<Role>()
							.HasQueryFilter(r => !r.IsDelete);
			
			modelBuilder.Entity<Group>()
							.HasQueryFilter(g => !g.IsDeleted);

			base.OnModelCreating(modelBuilder);
		}
	}
}
