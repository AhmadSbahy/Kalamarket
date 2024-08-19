using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.DTOs.Admin.Users
{
	public class UserListViewModel
	{
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class AddUserFromAdminViewModel
    {
        
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phonenmumber { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

    }
}
