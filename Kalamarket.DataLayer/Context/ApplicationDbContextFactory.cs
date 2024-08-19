using Kalamarket.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEbook.Datalayer.Context
{
	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<KalamarketContext>
	{
		public KalamarketContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<KalamarketContext>();
			optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER02;Database=Kalamarket_DB;Trusted_Connection=True;TrustServerCertificate=True; MultipleActiveResultSets=true;");

			return new KalamarketContext(optionsBuilder.Options);
		}
	}
}
