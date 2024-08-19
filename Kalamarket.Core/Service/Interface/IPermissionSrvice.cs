using Kalamarket.DataLayer.Eintitys.Permission;
using Kalamarket.DataLayer.Eintitys.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Interface
{
	public interface IPermissionSrvice
	{
		#region Role
		List<Role> GetRoles();
		void AddUserRoles(List<int> rolesId, int userId);
		void UpdateUserRole(string userName,List<int> roleIds);
		List<int> GetUserRoles(string userName);
		bool IsUserInRole(string userName,int roleId);
		#endregion

		#region Permission
		List<Permission> GetPermissions();
		void AddRolePermission(List<int> permission, int roleId);
		List<int> GetRolePermission(int roleId);
		void UpdateRolePermission(int roleId, List<int> permission);
		bool CheckPermission(int permissionId,string userName);
		#endregion
	}
}
