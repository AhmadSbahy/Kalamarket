using Kalamarket.Core.DTOs.User;
using Kalamarket.Core.Generator;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalamarket.Core.Convartor;
using Kalamarket.Core.DTOs.UserPanel;
using Kalamarket.DataLayer.Eintitys.Address;
using Microsoft.EntityFrameworkCore;

namespace Kalamarket.Core.Service.Classes
{
    public class UserSrvice : IUserSrvice
    {
        private KalamarketContext _context;
        public UserSrvice(KalamarketContext context)
        {
            _context = context;
        }

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public Address GetAddressById(int? id)
        {
            if(id != null)
            {
                return _context.Addresses.SingleOrDefault(a => a.AddressId == id);
            }
            else
            {
                return null;
            }
        }

		public Tuple<List<FavoriteViewModel>, int> GetFavoritesByUsername(string username)
		{
            int userId = GetUserIdByUserName(username);
            var Products = _context.FavoriteProducts.Include(f=>f.User).Include(f=> f.Product).ThenInclude(f=> f.Images).Where(f => f.UserId == userId)
                .Select(f => new FavoriteViewModel()
                {
                    FavoriteId = f.FavoriteId,
                    ProductId = f.ProductId,
                    ProductImage = f.Product.Images[0].ImageName,
                    ProductName = f.Product.ProductName,
                    ProductPrice = f.Product.ProductPrice.ToString("#,0")
                }).ToList();
            int Counts = _context.FavoriteProducts.Where(f=> f.UserId == userId).Count();

            return Tuple.Create(Products, Counts);
		}

		public List<QoustionsViewModel> GetQoustionsByUsername(string username)
		{
			int userId = GetUserIdByUserName(username);

            return _context.ProductQuestions.Include(q=> q.Product).Where(q => q.UserId == userId)
                .Select(q => new QoustionsViewModel()
                {
                    IsAnsewrd = q.IsAnsewrd,
                    ProductId = q.ProductId,
                    ProductImage = q.Product.Images[0].ImageName,
                    ProductName = q.Product.ProductName,
                    QoustionsBody = q.QuestionsBody,
                    QoustionsId = q.QuestionId
                }).ToList();
		}

		public ShowAddressViewModel GetShowAddressByUsername(string username)
        {
            if (username == null)
            {
                // یا می‌توانید خطا مدیریتی اعمال کنید یا مقدار پیش‌فرض یا null برگردانید.
                return null;
            }

            int UserId = GetUserIdByUserName(username);

            if (UserId == 0)
            {
                // مدیریت خطا یا بازگشت به عنوان null یا مقدار پیش‌فرض
                return null;
            }

            return _context.Addresses.Where(a => a.UserId == UserId)
                .Include(p => p.User)
                .Select(p => new ShowAddressViewModel()
                {
                    AddressId = p.AddressId,
                    City = p.City,
                    FullName = p.User.FullName,
                    State = p.State,
                    PhoneNumber = p.User.PhoneNumber,
                    PostalCode = p.PostalCode
                }).FirstOrDefault(); // اینجا از First به جای FirstOrDefault استفاده کردم


        }

        public SideBarInformation GetSideBarInformationByUserName(string username)
		{
			var user = GetUserByUserName(username);
            SideBarInformation sideBar = new SideBarInformation()
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                RegisterDate = user.RigisterDate
            };
            return sideBar;
		}

		public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByLoginVM(LoginViewModle login)
        {
            string email = FIxedText.FixEmailText(login.Email);
            string password = PasswordHelper.EncodePasswordMd5(login.Password);

            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(p => p.UserName == username);
        }

        public ForgotPasswordViewModel GetUserForResetPass(string email)
        {
            var UserId = GetUserIdByEmail(email);
            return _context.Users.Where(p => p.UserId == UserId)
                .Select(p => new ForgotPasswordViewModel()
                {
                    Email = p.Email,
                    Code = p.ActivCode,
                    FullName = p.FullName
                }).First();
        }

        public string GetUserFullNameById(int id)
        {
                return _context.Users.Single(p => p.UserId == id).FullName;

        }

        public string GetUserFullNameByUserName(string username)
        {
            return _context.Users.Single(p => p.UserName == username).FullName;
        }

        public int GetUserIdByEmail(string email)
        {
            return _context.Users.Single(p => p.Email == email).UserId;
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(p => p.UserName == userName).UserId;
        }

        public UserInformationViewModel GetUserInformationByUserName(string username)
        {
            var user = GetUserByUserName(username);
            UserInformationViewModel userInformation = new UserInformationViewModel();
            userInformation.FullName = user.FullName;
            userInformation.PhoneNumber = user.PhoneNumber;
            userInformation.RegisterDate = user.RigisterDate;
            userInformation.Email = user.Email;
            return userInformation;
        }

        public bool IsAddressForUser(string username,int? AddressId)
        {
            int userid = GetUserIdByUserName(username);
            if(AddressId != null)
            {
                return _context.Addresses.Any(p => p.AddressId == AddressId && p.UserId == userid);
            }
           else
            {
                return false;
            }
        }

        public bool IsCodeInUser(string email, string code)
        {
            var user = GetUserByEmail(email);

            if(user != null && user.ActivCode.ToString() == code) 
            {
                Random random = new Random();

                int activationCode = random.Next(10000, 100000);

                user.IsActive = true;
                user.ActivCode = activationCode;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(p => p.Email == email);
        }

        public bool IsEmailExsit(string email)
        {
            return _context.Users.Any(p => p.Email == email);
        }

        public bool IsPasswordExsitByUsername(string username, string password)
        {
            int UserId = GetUserIdByUserName(username);

            string ENPass = PasswordHelper.EncodePasswordMd5(password);

            return _context.Users.Any(p=> p.UserId == UserId && p.Password == ENPass);
        }

        public bool IsUserHaveAddress(string username)
        {
            int UserId = GetUserIdByUserName(username);

            return _context.Addresses.Any(a => a.UserId == UserId);
        }

        public bool IsUserNameExsit(string userName)
        {
            return _context.Users.Any(p => p.UserName == userName);
        }

        public void RegisterUser(RegisterViewModle rigister)
        {
            var User = new User()
            {
                Email = FIxedText.FixEmailText(rigister.Email),
                IsDelete = false,
                ActivCode =rigister.Code ,
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(rigister.Password),
                UserName = rigister.UserName,
                RigisterDate = DateTime.Now,
                FullName = rigister.FullName,
                PhoneNumber = rigister.PhoneNumber
            };
            _context.Users.Add(User);
            _context.SaveChanges();
        }

        public void UpdateAddress(Address address)
        {
            _context.Addresses.Update(address);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

		public void UpdateUserPassByEmail(string email, string password)
		{
            var User = GetUserByEmail(email);
            if (User != null)
            {
                User.Password = PasswordHelper.EncodePasswordMd5(password);
                _context.Users.Update(User);
                _context.SaveChanges();
            }
		}
	}
}
