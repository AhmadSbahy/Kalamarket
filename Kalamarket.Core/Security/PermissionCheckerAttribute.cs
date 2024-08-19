using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Security
{
	public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		private IPermissionSrvice _permissionSrvice;
		private int _permissionId = 0;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
		{
			_permissionSrvice =
				(IPermissionSrvice)context.HttpContext.RequestServices.GetService(typeof(IPermissionSrvice));

			if (context.HttpContext.User.Identity.IsAuthenticated)
			{
				string userName = context.HttpContext.User.Identity.Name;
				if (!_permissionSrvice.CheckPermission(_permissionId, userName))
				{
					context.Result = new NotFoundResult();
				}
			}
			else
			{
				context.Result = new RedirectResult("/Login");
			}
		}
	}
}
