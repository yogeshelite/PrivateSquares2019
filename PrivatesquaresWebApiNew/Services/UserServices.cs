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
    }
    public interface IUserServices
    {
        ResponseModel RegisterUser(UserRegisterModel objModel);
        ResponseModel Login(UserRegisterModel objModel);
        ResponseModel SaveProfile(UserProfileModel objModel);
        ResponseModel SaveBusiness(BusinessModel objModel);
        ResponseModel SaveUserInterest(UserInterestModel objModel);
        ResponseModel SaveProduct(ProductModel objModel);
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