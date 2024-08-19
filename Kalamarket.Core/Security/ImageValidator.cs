using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Security
{
	public static class ImageValidator
	{
		public static bool IsvalidImage(this IFormFile file)
		{
			try
			{
				var img = System.Drawing.Image.FromStream(file.OpenReadStream());
				return true;
			}
			catch 
			{

				return false;
			}
		}
	}
}
