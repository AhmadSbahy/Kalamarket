using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Address
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Display(Name = "ادرس كاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public int UserId { get; set; }
        [Display(Name = "نام تحویل گیرنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(300, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string RecipientName { get; set; }
        [Display(Name = "شماره تلفن ثابت")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(300, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string Landlinenumber { get; set; }
        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(300, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string State { get; set; }
        [Display(Name = "پلاک")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(300, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string Plate { get; set; }
        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(300, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string City { get; set; }
        [Display(Name = "واحد")]
        [MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string? Floar { get; set; }
        [Display(Name = "کد پستی")]
        [MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string? PostalCode { get; set; }
        [Display(Name = "ادرس")]
        public string FullAddress { get; set; }
        
        [Display(Name = "توضيحات ادرس")]
        public string? AddressDescripton { get; set; }

        [ForeignKey("UserId")]
        public User.User? User { get; set; }
    }
}
