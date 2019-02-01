
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



        //public RagisterUser_Result RegisterUser(UserModel userModel)
        //{
        //    userModel.Id = Guid.NewGuid(); 
        //    return Context.RagisterUser(userModel.UserName, userModel.Password, userModel.Email, userModel.RegisterationDate).FirstOrDefault();

        //}

        //public LoginAuthenticate_Result LoginAuthenticte(LoginModel loginModel)
        //{
        //    var RESULT = Context.LoginAuthenticate(loginModel.UserName, loginModel.Password, loginModel.RecordTime);
        //    return  RESULT.FirstOrDefault();
        //}

        //public SaveProduct_Result SaveProduct(UserProductModel user)
        //{
        //    return Context.SaveProduct(user.ASIN, user.UserId, user.isFeatured, user.categoryId,user.Operation).FirstOrDefault();
        //}

        //public IEnumerable<GetUserProducts_Result> GetUserProducts(UserProductModel user)
        //{
        //    return Context.GetUserProducts(user.UserId);
        //}
        public LoginAuthenticate_Result LoginAuthenticte(UserRegisterModel ObjModel)
        {
            var RESULT = Context.LoginAuthenticate(ObjModel.Mobile, ObjModel.Otp);
            return RESULT.FirstOrDefault();
        }
        public RegisterUser_Result RegisterUser(UserRegisterModel ObjModel)
        {
            return Context.RegisterUser(ObjModel.Mobile).FirstOrDefault();
        }
    }

    public interface IUserRepositary : IGenericRepository<EWT_PSQNEWEntities>
    {
        RegisterUser_Result RegisterUser(UserRegisterModel ObjModel);
        LoginAuthenticate_Result LoginAuthenticte(UserRegisterModel ObjModel);
    }
}