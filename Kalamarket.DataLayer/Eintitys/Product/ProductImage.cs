using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Product
{
    public class ProductImage
    {
        [Key]
        public int IamgeId { get; set; }
        [Display(Name = "تصوير محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string ImageName { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }   
}
