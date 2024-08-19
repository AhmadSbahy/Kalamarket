using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.User;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Permission;
using Kalamarket.Core.Service.Classes;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Roles
{
	[PermissionChecker(12)]
	public class EditModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;
        private readonly IPermissionSrvice _permission;
        public EditModel(Kalamarket.DataLayer.Context.KalamarketContext context,IPermissionSrvice permission)
        {
            _context = context;
            _permission = permission;   
        }

        [BindProperty]
        public Role Role { get; set; }

		public List<Permission> Permissions { get; set; }
		

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _context.Roles.FirstOrDefaultAsync(m => m.RoleId == id);

            if (Role == null)
            {
				return NotFound();
			}

			Permissions = _permission.GetPermissions();

			ViewData["selected"] = _permission.GetRolePermission((int)id);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(List<int> selectedperm)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Roles.Update(Role);
            _permission.UpdateRolePermission(Role.RoleId, selectedperm);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(Role.RoleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
    }
}
