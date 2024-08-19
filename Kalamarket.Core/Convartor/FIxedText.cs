using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Convartor
{
    public class FIxedText
    {
        public static string FixEmailText(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
