using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.User;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Permission;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Roles
{
	[PermissionChecker(11)]
	public class CreateModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;
        private IPermissionSrvice _permissionSrvice;
        public CreateModel(Kalamarket.DataLayer.Context.KalamarketContext context,IPermissionSrvice permission)
        {
            _context = context;
            _permissionSrvice = permission;
        }
        public List<Permission> Permissions { get; set; }
        public IActionResult OnGet()
        {
            Permissions = _permissionSrvice.GetPermissions();
            return Page();
        }

        [BindProperty]
        public Role Role { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(List<int> selectedperm)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Role.IsDelete = false;
            _context.Roles.Add(Role);
            await _context.SaveChangesAsync();
			_permissionSrvice.AddRolePermission(selectedperm, Role.RoleId);
			return RedirectToPage("./Index");
        }
    }
}
