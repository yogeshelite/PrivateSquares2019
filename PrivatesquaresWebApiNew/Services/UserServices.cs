using Newtonsoft.Json;
using PrivatesquaresWebApiNew.Models;
using PrivatesquaresWebApiNew.Persistance.Data;
using PrivatesquaresWebApiNew.Persistance.Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivatesquaresWebApiNew.Services
{
    public class UserServices : IUserServices
    {
        public IUserRepositary _instance { get; set; }
        public UserServices()
        {

        }
        public UserServices(IUserRepositary instance)
        {
            _instance = instance;
        }

        public ResponseModel RegisterUser(UserRegisterModel objModel)
        {
            var _result = _instance.RegisterUser(objModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel Login(UserRegisterModel objModel)
        {
            var _result = _instance.LoginAuthenticte(objModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };

        }

        public ResponseModel SaveProfile(UserProfileModel objModel)
        {
            var _result = _instance.SaveProfile(objModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel SaveBusiness(BusinessModel objModel)
        {
            var _result = _instance.SaveBusiness(objModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel SaveUserInterest(UserInterestModel objModel)
        {
            var _result = new SaveUserInterest_Result();
            try
            {
                _result = _instance.SaveUserInterest(objModel);

            }
            catch (Exception ex)
            {
                String Exception = ex.ToString();
                _result.Response = Exception;
            }
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel SaveProduct(ProductModel objModel)
        {

            var _result = _instance.SaveProduct(objModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel GetUsersProfile()
        {
            var _result = _instance.GetUserProfileList();
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetAllInterest()
        {
            var _result = _instance.GetAllInterest();
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetAllInterestCategory()
        {
            var _result = _instance.GetAllInterestCategory();
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel SaveNetwork(NetworkModel objModel)
        {
            var _result = _instance.SaveNetwork(objModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel GetNetwork(NetworkModel networkModel)
        {
            var _result = _instance.GetNetwork(networkModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetUserbusiness(BusinessModel objModel)
        {
            var _result = _instance.GetUserbusiness(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetUserProfile(UserProfileModel objModel)
        {
            var _result = _instance.GetUserProfile(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetBusiness(BusinessModel objModel)
        {
            var _result = _instance.GetUserbusiness(objModel);

            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetProduct(ProductModel objModel)
        {
            var _result = _instance.GetProduct(objModel);

            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetUserInterest(UserInterestModel objModel)
        {
            var _result = _instance.GetUserInterest(objModel);

            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel UpdateUserInterest(UserInterestModel objModel)
        {
            var _result = new SaveUserInterest_Result();

            try

            {

                _result = _instance.UpdateUserInterest(objModel);
            }

            catch (Exception ex)

            {

                String Exception = ex.ToString();

                _result.Response = Exception;

            }

            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel GetProfession()
        {
            var _result = _instance.GetProfession();
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetCountry()
        {
            var _result = _instance.GetCountry();

            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetState(DropDownModel objModel)
        {
            var _result = _instance.GetState(objModel);

            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetCity(DropDownModel objModel)
        {
            var _result = _instance.GetCity(objModel);

            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetProductCategory()
        {
            var _result = _instance.GetProductCategory();

            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }
        public ResponseModel GetProductDetail(ProductModel objModel)

        {
            var _result = _instance.GetProductDetail(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };

        }

        public ResponseModel GetBusinessDetail(BusinessModel objModel)
        {
            var _result = _instance.GetBusinessDetail(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel LoginUser(LoginModel objModel)
        {
            var _result = _instance.LoginUser(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }
        public ResponseModel RegisterNewUser(LoginModel objModel)
        {
            var _result = _instance.RegisterNewUser(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }
    }
    public interface IUserServices
    {
        ResponseModel RegisterUser(UserRegisterModel objModel);
        ResponseModel Login(UserRegisterModel objModel);
        ResponseModel SaveProfile(UserProfileModel objModel);
        ResponseModel SaveBusiness(BusinessModel objModel);
        ResponseModel SaveUserInterest(UserInterestModel objModel);
        ResponseModel SaveProduct(ProductModel objModel);
        ResponseModel GetUserbusiness(BusinessModel objModel);
        ResponseModel GetUsersProfile();
        ResponseModel GetAllInterest();
        ResponseModel GetAllInterestCategory();
        ResponseModel SaveNetwork(NetworkModel objModel);
        ResponseModel GetNetwork(NetworkModel objModel);
        ResponseModel GetUserProfile(UserProfileModel objModel);
        ResponseModel GetBusiness(BusinessModel objModel);

        ResponseModel GetProduct(ProductModel objModel);

        ResponseModel GetUserInterest(UserInterestModel objModel);

        ResponseModel UpdateUserInterest(UserInterestModel objModel);
        ResponseModel GetProfession();
        ResponseModel GetCountry();
        ResponseModel GetState(DropDownModel objModel);
        ResponseModel GetCity(DropDownModel objModel);
        ResponseModel GetProductCategory();
        ResponseModel GetProductDetail(ProductModel objModel);
        ResponseModel GetBusinessDetail(BusinessModel objModel);
        ResponseModel LoginUser(LoginModel objModel);
        ResponseModel RegisterNewUser(LoginModel objModel);
        #region Commented Code For Sample Code
        //ResponseModel RegisterUser(UserModel userModel);

        //ResponseModel GetAuthorize(LoginModel loginModel);
        //ResponseModel SaveProduct(UserProductModel user);
        //object GetUserDetailByUserId(UserProductModel user);
        //ResponseModel UserProducts(UserProductModel user);
        //ResponseModel ChangePassword(ChangePasswordModel objModel);
        //ResponseModel ForgetPassword(LoginModel objModel);
        //ResponseModel GetUserSubscriptionPlan(UserSubscriptionPlanModel objModel);
        //ResponseModel GetSubscriptionPlan(SubscriptionPlanModel objModel);
        //ResponseModel SaveUserSubscriptionPlan(SubscribeModel objModel);
        #endregion
    }
}