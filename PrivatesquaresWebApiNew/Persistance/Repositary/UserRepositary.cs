﻿
using PrivatesquaresWebApiNew.Models;
using PrivatesquaresWebApiNew.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivatesquaresWebApiNew.Persistance.Repositary
{
    public class UserRepositary : GenericRepository<EWT_PSQNEWEntities>, IUserRepositary
    {
        public ForgetPassword_Result ForgetPassword(LoginModel objModel)
        {
            return Context.ForgetPassword(objModel.EmailId).FirstOrDefault();
        }

        public IEnumerable<GetAllInterest_Result> GetAllInterest()
        {
            return Context.GetAllInterest().ToList();
        }

        public IEnumerable<InterestCategories_Result> GetAllInterestCategory()
        {
            return Context.InterestCategories().ToList();
        }

        public IEnumerable<GetUserBusiness_Result> GetBusiness(BusinessModel objModel)
        {
            return Context.GetUserBusiness(objModel.UserId).ToList();
        }

        public IEnumerable<GetBusinessDetail_Result> GetBusinessDetail(BusinessModel objModel)
        {
            return Context.GetBusinessDetail(objModel.Id).ToList();
        }

        public IEnumerable<GetCity_Result> GetCity(DropDownModel objModel)
        {
            return Context.GetCity(objModel.StateId).ToList();
        }

        public IEnumerable<GetCountry_Result> GetCountry()
        {
            return Context.GetCountry().ToList();
        }

        public IEnumerable<GetNetwork_Result> GetNetwork(NetworkModel networkModel)
        {
            return Context.GetNetwork(networkModel.LogInUserId).ToList();
        }

        public IEnumerable<GetProduct_Result> GetProduct(ProductModel objModel)
        {
            return Context.GetProduct(objModel.UserId).ToList();
        }

        public IEnumerable<GetProductCategory_Result> GetProductCategory()
        {
            return Context.GetProductCategory().ToList();
        }

        public IEnumerable<GetProductDetail_Result> GetProductDetail(ProductModel objModel)
        {
            return Context.GetProductDetail(objModel.Id).ToList();
        }

        public IEnumerable<GetProfession_Result> GetProfession()
        {
            return Context.GetProfession().ToList();
        }

        public IEnumerable<GetState_Result> GetState(DropDownModel objModel)
        {
            return Context.GetState(objModel.CountryId).ToList();
        }

        public IEnumerable<GetUserBusiness_Result> GetUserbusiness(BusinessModel objModel)
        {
            return Context.GetUserBusiness(objModel.UserId).ToList();
        }

        public IEnumerable<GetUserInterest_Result> GetUserInterest(UserInterestModel objModel)
        {
            return Context.GetUserInterest(objModel.UserId).ToList();
        }

        public IEnumerable<GetUserProfile_Result> GetUserProfile(UserProfileModel objModel)
        {
            return Context.GetUserProfile(objModel.UserId).ToList();
        }

        public LoginAuthenticate_Result LoginAuthenticte(UserRegisterModel ObjModel)
        {
            var RESULT = Context.LoginAuthenticate(ObjModel.Mobile, ObjModel.Otp);
            return RESULT.FirstOrDefault();
        }

        public AuthenticateLogin_Result LoginUser(LoginModel objModel)
        {
            return Context.AuthenticateLogin(objModel.EmailId, objModel.Password).FirstOrDefault();
        }
        public RegisterNewUser_Result RegisterNewUser(LoginModel objModel)
        {
            return Context.RegisterNewUser(objModel.Name, objModel.EmailId, objModel.Password).FirstOrDefault();
        }

        public RegisterUser_Result RegisterUser(UserRegisterModel ObjModel)
        {
            return Context.RegisterUser(ObjModel.Mobile, ObjModel.Operation).FirstOrDefault();
        }

        public SaveBusiness_Result SaveBusiness(BusinessModel ObjModel)
        {
            return Context.SaveBusiness(ObjModel.Id, ObjModel.BusinessName, ObjModel.Location, ObjModel.BusinessLogo, ObjModel.ProfessionalCatId,
                ObjModel.ProfessionalKeyword, ObjModel.CityId, ObjModel.PinCode, ObjModel.UserId, ObjModel.Email, ObjModel.Description, ObjModel.Phone, ObjModel.CountryId, ObjModel.Operation).FirstOrDefault();

        }

        public SaveNetwork_Result SaveNetwork(NetworkModel objModel)
        {
            return Context.SaveNetwork(objModel.LogInUserId, objModel.UserId, objModel.UserProfileId, objModel.Operation).FirstOrDefault();


        }

        public SaveProduct_Result SaveProduct(ProductModel objModel)
        {
            return Context.SaveProduct(objModel.Id, objModel.ProductName, objModel.ProductCatId, objModel.ProductImage, objModel.SellingPrice, objModel.DiscountPrice, objModel.BusinessId, objModel.UserId, objModel.Description, objModel.Operation).FirstOrDefault();

        }

        public SaveProfile_Result SaveProfile(UserProfileModel ObjModel)
        {
            return Context.SaveProfile(ObjModel.UserId, ObjModel.FirstName, ObjModel.LastName, ObjModel.ProfileImage, ObjModel.Description, ObjModel.EmailId, ObjModel.ProfessionalCatId, ObjModel.Title, ObjModel.ProfessionalKeyword, ObjModel.CityId, ObjModel.Password, ObjModel.GenderId, ObjModel.DOB, ObjModel.Location, ObjModel.Phone, ObjModel.Pincode, ObjModel.CountryId).FirstOrDefault();
        }

        public SaveUserInterest_Result SaveUserInterest(UserInterestModel objModel)
        {
            var Result = Context.SaveUserInterest(objModel.XmlData, objModel.Operation);
            return Result.FirstOrDefault();
        }

        public SaveUserInterest_Result UpdateUserInterest(UserInterestModel objModel)
        {
            var Result = Context.SaveUserInterest(objModel.XmlData, objModel.Operation);

            return Result.FirstOrDefault();
        }

        IEnumerable<GetUsersProfileList_Result> IUserRepositary.GetUserProfileList(UsersProfileModel objModel)
        {
            return Context.GetUsersProfileList(objModel.UserId).ToList();
        }
    }

    public interface IUserRepositary : IGenericRepository<EWT_PSQNEWEntities>
    {
        RegisterUser_Result RegisterUser(UserRegisterModel ObjModel);
        LoginAuthenticate_Result LoginAuthenticte(UserRegisterModel ObjModel);
        SaveProfile_Result SaveProfile(UserProfileModel ObjModel);
        SaveBusiness_Result SaveBusiness(BusinessModel ObjModel);
        SaveUserInterest_Result SaveUserInterest(UserInterestModel objModel);
        SaveProduct_Result SaveProduct(ProductModel objModel);
        IEnumerable<GetUsersProfileList_Result> GetUserProfileList(UsersProfileModel objModel);
        IEnumerable<GetAllInterest_Result> GetAllInterest();
        IEnumerable<InterestCategories_Result> GetAllInterestCategory();
        SaveNetwork_Result SaveNetwork(NetworkModel objModel);
        IEnumerable<GetNetwork_Result> GetNetwork(NetworkModel networkModel);
        IEnumerable<GetUserBusiness_Result> GetUserbusiness(BusinessModel objModel);
        IEnumerable<GetUserProfile_Result> GetUserProfile(UserProfileModel objModel);
        IEnumerable<GetUserBusiness_Result> GetBusiness(BusinessModel objModel);

        IEnumerable<GetProduct_Result> GetProduct(ProductModel objModel);

        IEnumerable<GetUserInterest_Result> GetUserInterest(UserInterestModel objModel);

        SaveUserInterest_Result UpdateUserInterest(UserInterestModel objModel);
        IEnumerable<GetProfession_Result> GetProfession();
        IEnumerable<GetCountry_Result> GetCountry();
        IEnumerable<GetState_Result> GetState(DropDownModel objModel);
        IEnumerable<GetCity_Result> GetCity(DropDownModel objModel);
        IEnumerable<GetProductCategory_Result> GetProductCategory();
        IEnumerable<GetProductDetail_Result> GetProductDetail(ProductModel objModel);
        IEnumerable<GetBusinessDetail_Result> GetBusinessDetail(BusinessModel objModel);
        AuthenticateLogin_Result LoginUser(LoginModel objModel);
        RegisterNewUser_Result RegisterNewUser(LoginModel objModel);
        ForgetPassword_Result ForgetPassword(LoginModel objModel);
    }

}