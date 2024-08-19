using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Permission
{
	public class Permission
	{
		[Key]
		public int PermissionId { get; set; }
		[Display(Name = "عنوان دسترسى")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		[MaxLength(400, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
		public string PermissionTitle { get; set; }
		
		public int? ParentId { get; set; }
		

		[ForeignKey("ParentId")]
		public List<Permission>? Permissions { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }

    }
}
