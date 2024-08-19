using Azure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.ContactUs
{
    public class ContactUs
    {
        [Key]
        public int ContactId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "لطفا موضوعى را انتخاب كنيد")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "لطفا عنوان پیام را وارد كنيد")]
        public string MassageTitle { get; set; } 
        [Required(ErrorMessage = "لطفا متن پیام را وارد كنيد")]
        public string Massagebody { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
    }
}
