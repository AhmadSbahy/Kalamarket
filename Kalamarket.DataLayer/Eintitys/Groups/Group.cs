using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Groups
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string GroupTitle { get; set; }
        [Display(Name = "گروه اصلی")]
        public int? GroupParentId { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDeleted { get; set; }

        [ForeignKey("GroupParentId")]
        public List<Group>? Groups { get; set; }

        public List<Product.Product>? Products { get; set; }
    }
}
