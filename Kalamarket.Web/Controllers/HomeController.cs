using Kalamarket.Core.DTOs.Index;
using Kalamarket.Core.DTOs.Order;
using Kalamarket.Core.Service.Classes;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Product;
using Kalamarket.DataLayer.Eintitys.ProductQuestions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace MyMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminService _Adminsrvice;
        private readonly IProductService _productsrvice;
        private readonly IUserSrvice _usersrvice;
        private readonly IOrderSrvice _ordersrvice;
        public HomeController(IAdminService admin, IProductService productsrvice, IUserSrvice userSrvice,IOrderSrvice order)
        {
            _Adminsrvice = admin;
            _productsrvice = productsrvice;
            _usersrvice = userSrvice;
            _ordersrvice = order;

        }
        public IActionResult Index()
        {
            List<IndexGroupsViewModel> model = _productsrvice.indexGroupsViews();
            ViewBag.Banners = _productsrvice.GetBanners();
            return View(model);
        }
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var Product = _productsrvice.GetProductById(id);
            ViewData["GroupName"] = _productsrvice.GetGroupNameById(Product.GroupId);
            ViewData["SubGroupName"] = _productsrvice.GetSubGroupNameById((int)Product.SubGroup);
            ViewData["SelerName"] = _usersrvice.GetUserFullNameById(Product.SellerId);
            ViewBag.SellCount = _ordersrvice.GetProductOrderCount(id);

			return View(Product);
        }
        public IActionResult GetSubGroup(int id)
        {
            var Sub = _Adminsrvice.GetAllSubGroupForProduct(id);
            return Json(new SelectList(Sub, "Value", "Text"));
        }
        [Route("AboutUs")]
        public IActionResult AboutUs()
        {
            return View();
        }
        [Route("Error404")]
        public IActionResult Error404()
        {
            return View();
        }
        [Route("DeleteComment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            int ProductId = _productsrvice.DeleteComment(id);
            return Redirect($"/Details/{ProductId}");
        }
        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/EditorImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }

            var url = $"{"/EditorImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
        [HttpPost]  
        public IActionResult AddComment(ProductComment comment, List<string> advantages,List<string> devantages)
        {
            //ToDo Check the Product In User
            if (!string.IsNullOrEmpty(comment.CommentTitle) && !string.IsNullOrEmpty(comment.CommentBody))
            {
                int userId = _usersrvice.GetUserIdByUserName(User.Identity.Name);
                string gpont = string.Join("-", advantages);
                string bpont = string.Join("-", devantages);
                ProductComment productComment = new ProductComment()
                {
                    CommentTitle = comment.CommentTitle,
                    CommentBody = comment.CommentBody,
                    GoodPoints = gpont,
                    CommentDate = DateTime.Now,
                    ProductId = comment.ProductId,
                    UserId = userId,
                    BadPoiant = bpont,
                    IsRecommended = comment.IsRecommended
                };
                int productid = _productsrvice.AddComment(productComment);
                return Redirect($"/Details/{productid}");
            }
            else
            {
                ModelState.AddModelError("comment.CommentTitle", "لطفا مقادير اجبارى را وارد كنيد");
                return Redirect($"/Details/{comment.ProductId}");
            }

        }
        public IActionResult AddQoustion(ProductQuestions question)
        {
            if (!string.IsNullOrEmpty(question.QuestionsBody))
            {
                var qoue = new ProductQuestions()
                {
                    IsAnsewrd = false,
                    ProductId = question.ProductId,
                    QuestionDate = DateTime.Now,
                    UserId = _usersrvice.GetUserIdByUserName(User.Identity.Name),
                    QuestionsBody = question.QuestionsBody,
                };

                int productid = _productsrvice.AddQustion(qoue);
                return Redirect($"/Details/{productid}");
            }
            else
            {
                ModelState.AddModelError("question.QuestionsBody", "لطفا مقادير را وارد كنيد");
                return Redirect($"/Details/{question.ProductId}");
            }
        }
        [Route("DeleteQoustion/{id}")]
        public IActionResult DeleteQoustion(int id)
        {
            int productid = _productsrvice.DeleteQustion(id);

            return Redirect($"/Details/{productid}");
        }
		[Route("AddAnsewr/{productId}/{qoustionId}/{body}")]
		public IActionResult AddAnsewr(int productId,int qoustionId,string body)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }
            if(string.IsNullOrEmpty(body))
            {
                return Redirect("/");
            }
            int userId = _usersrvice.GetUserIdByUserName(User.Identity.Name);
            int SellerId = _productsrvice.GetProductSellerIdByProductId(productId);

            if(SellerId == null || SellerId != userId)
            {
				return BadRequest();
			}
            ProductAnswer answer = new ProductAnswer()
            {
                AnswerDate = DateTime.Now,
                ProductId = productId,
                QuestionId = qoustionId,
                UserId = userId,
                AnserBody = body
            };
            _productsrvice.IsAnserd(qoustionId);
            _productsrvice.AddAnsewr(answer);

			return Redirect($"/Details/{productId}");
		}
        [Route("DeleteAnsewr/{id}")]
        public IActionResult DeleteAnsewr(int id)
        {
            int productid = _productsrvice.DeleteAnsewr(id);

            return Redirect($"/Details/{productid}");
        }
        public IActionResult UseDiscount(string code,int orderId)
        {
            Discounts type = _ordersrvice.UseDiscounts(orderId, code);

            return Redirect("/Product/ShowCart?Type=" + type.ToString());
        }
        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            var Order = _ordersrvice.GetOrderById(id);
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                

                var payment = new ZarinpalSandbox.Payment((int)Order.OrderSum);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    foreach (var item in Order.OrderDetails)
                    {
                        var product = _productsrvice.GetProductById(item.ProductId);
                        product.SellCount += item.Count;
                        _productsrvice.UpdateProduct(product);
					}
                    ViewBag.code = res.RefId;
                    Order.IsFinaly = true;
                    _ordersrvice.UpdateOrder(Order);

                    #region User Order

                    _ordersrvice.AddUserOrder(Order.OrderId,User.Identity.Name);

                    #endregion

                    return View("SucsessCheckOut",Order);
                }

            }
                return View("NotSucsessCheckOut",Order);
            
            
        }
    }
}
