using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Convartor
{
	public static class JsonResponseStatus
	{
		public static JsonResult Ok()
		{
			return new JsonResult(new
			{
				Status = "ok"
			});
		}
		public static JsonResult Ok(object returnData)
		{
			return new JsonResult(new
			{
				Status = "ok",
				data = returnData
			});
		}
		public static JsonResult NotFound()
		{
			return new JsonResult(new
			{
				Status = "notFound",
			});
		}
		public static JsonResult NotFound(object returnData)
		{
			return new JsonResult(new
			{
				Status = "notFound",
				data = returnData
			});
		}
		public static JsonResult UnAuthorize()
		{
			return new JsonResult(new
			{
				Status = "unAuthorize",
			});
		}
		public static JsonResult UnAuthorize(object returnData)
		{
			return new JsonResult(new
			{
				Status = "unAuthorize",
				data = returnData,
			});
		}
		public static JsonResult Error()
		{
			return new JsonResult(new
			{
				Status = "error",
			});
		}
		public static JsonResult Error(object returnData)
		{
			return new JsonResult(new
			{
				Status = "error",
				data = returnData
			});
		}
	}
}