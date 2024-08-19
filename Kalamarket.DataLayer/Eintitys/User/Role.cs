using Kalamarket.DataLayer.Eintitys.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.User
{
	public class Role
	{
		[Key]
		public int RoleId { get; set; }
		[Required]
		[MaxLength(600)]
		[Display(Name ="عنوان نقش")]
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }	

        public List<UserRole>? UserRoles { get; set; }
		public List<RolePermission>? RolePermissions { get; set; }
	}
}
