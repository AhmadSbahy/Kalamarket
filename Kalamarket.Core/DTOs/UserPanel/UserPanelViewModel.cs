using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.DTOs.UserPanel
{
    public class UserInformationViewModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
    }
    public class SideBarInformation
    {
        public string FullName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class ShowAddressViewModel
    {
        public int AddressId { get; set; }
        public string FullName { get; set;}
        public string PhoneNumber { get; set;}
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }    
    }
    public class EditeUserInformationViewModel
    {
        public string? FullName { get; set;}
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage ="براي تغيير كلمه عبور بايد كلمه عبور جديد را وارد كنيد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "براي تغيير كلمه عبور بايد تكرار كلمه عبور جديد را وارد كنيد")]
        [Compare("NewPassword", ErrorMessage = "كلمه عبور با تكرار كلمه عبور مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string ReNewPassword { get; set; }
    }
    public class FavoriteViewModel
    {
        public int FavoriteId { get; set; } 
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage{ get; set; }
        public string ProductPrice { get; set; }
    }
    public class QoustionsViewModel
    {
		public int QoustionsId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; } 
		public string ProductImage { get; set; }
        public string QoustionsBody { get; set; }
        public bool IsAnsewrd { get; set; }
    }

}
