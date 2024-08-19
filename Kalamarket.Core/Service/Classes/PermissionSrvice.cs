using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Permission;
using Kalamarket.DataLayer.Eintitys.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Classes
{
	public class PermissionSrvice : IPermissionSrvice
	{
		private KalamarketContext _context;
		private IUserSrvice _userSrvice;

		public PermissionSrvice(KalamarketContext context, IUserSrvice user)
		{
			_context = context;
			_userSrvice = user;
		}

		public void AddRolePermission(List<int> permission, int roleId)
		{
			foreach (var item in permission)
			{
				_context.RolePermissions.Add(new RolePermission()
				{
					PermissionId = item,
					RoleId = roleId
				});
			}
			_context.SaveChanges();
		}

		public void AddUserRoles(List<int> rolesId, int userId)
		{
            foreach (var item in rolesId)
            {
				_context.UserRoles.Add(new UserRole()
				{
					UserId = userId,
					RoleId = item
				});
            }
			_context.SaveChanges();
        }

		public bool CheckPermission(int permissionId, string userName)
		{
			int userId = _userSrvice.GetUserIdByUserName(userName);
			
			List<int> UserRole = _context.UserRoles
				.Where(r=> r.UserId == userId)
				.Select(p=> p.RoleId)
				.ToList();

			if (UserRole == null)
				return false;

			List<int> RolePermission = _context.RolePermissions
				.Where(p=> p.PermissionId == permissionId)
				.Select(p=> p.RoleId)
				.ToList();

			return RolePermission.Any(e => UserRole.Contains(e));
		}

		public List<Permission> GetPermissions()
		{
			return _context.Permissions.ToList();
		}

		public List<int> GetRolePermission(int roleId)
		{
			return _context.RolePermissions
				.Where(p => p.RoleId == roleId)
				.Select(p => p.PermissionId)
				.ToList();
		}

		public List<Role> GetRoles()
		{
			return _context.Roles.ToList();
		}

		public List<int> GetUserRoles(string userName)
		{
			int userId = _userSrvice.GetUserIdByUserName(userName);

			return _context.UserRoles.Where(p => p.UserId == userId)
				.Select(p => p.RoleId)
				.ToList();
		}

		public bool IsUserInRole(string userName,int roleId)
		{
			var userRoles = GetUserRoles(userName);

			return userRoles.Contains(roleId);
		}	

		public void UpdateRolePermission(int roleId, List<int> permission)
		{
            foreach (var item in _context.RolePermissions.Where(p => p.RoleId == roleId))
            {
				_context.RolePermissions.Remove(item);
            }
			AddRolePermission(permission,roleId);
        }

		public void UpdateUserRole(string userName, List<int> roleIds)
		{
			int userId = _userSrvice.GetUserIdByUserName(userName);

            foreach (var item in _context.UserRoles.Where(p=> p.UserId == userId))
            {
				_context.UserRoles.Remove(item);
            }

			AddUserRoles(roleIds,userId);
        }
	}
}
