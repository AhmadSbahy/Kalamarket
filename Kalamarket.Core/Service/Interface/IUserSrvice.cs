using Kalamarket.Core.DTOs.User;
using Kalamarket.Core.DTOs.UserPanel;
using Kalamarket.DataLayer.Eintitys.Address;
using Kalamarket.DataLayer.Eintitys.User;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Interface
{
    public interface IUserSrvice
    {
        #region Register
        void RegisterUser(RegisterViewModle rigister);
        bool IsUserNameExsit(string userName);
        bool IsEmailExsit(string email);
        bool IsCodeInUser(string email,string code);
        User GetUserByEmail(string email);
        #endregion

        #region Login
        User GetUserByLoginVM(LoginViewModle login);
        ForgotPasswordViewModel GetUserForResetPass(string email);
        #endregion

        #region User
        User GetUserByUserName(string username);
        User GetUserById(int id);
        int GetUserIdByUserName(string userName);
        string GetUserFullNameByUserName(string username);
        string GetUserFullNameById(int id);
        bool IsPasswordExsitByUsername(string username,string password);
        void UpdateUser(User user);
        int GetUserIdByEmail(string email);
        bool IsEmailExist(string email);
        void UpdateUserPassByEmail(string email,string password);
        #endregion

        #region UserPanel
        UserInformationViewModel GetUserInformationByUserName(string username);
		SideBarInformation GetSideBarInformationByUserName(string username);
		Tuple<List<FavoriteViewModel>,int> GetFavoritesByUsername(string username);
        List<QoustionsViewModel> GetQoustionsByUsername(string username);

		#endregion
		#region Address
		bool IsUserHaveAddress(string username);
        void AddAddress(Address address);
        ShowAddressViewModel GetShowAddressByUsername(string username);
        Address GetAddressById(int? id);
        bool IsAddressForUser(string username,int? AddressId);
        void UpdateAddress(Address address);
        #endregion
    }
}
