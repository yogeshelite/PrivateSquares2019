
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

        public SaveBusiness_Result SaveBusiness(BusinessModel ObjModel)
        {
            return Context.SaveBusiness(ObjModel.Id, ObjModel.BusinessName, ObjModel.Location, ObjModel.BusinessLogo, ObjModel.ProfessionalCatId,
                ObjModel.ProfessionalKeyword, ObjModel.ProfessionalCatId, ObjModel.PinCode,ObjModel.UserId,ObjModel.Operation).FirstOrDefault();

        }

        public SaveProduct_Result SaveProduct(ProductModel objModel)
        {
            return Context.SaveProduct(objModel.Id, objModel.ProductName, objModel.ProductCatId, objModel.ProductImage,objModel.SellingPrice,objModel.DiscountPrice,objModel.BusinessId,objModel.UserId,objModel.Operation).FirstOrDefault();

        }

        public SaveProfile_Result SaveProfile(UserProfileModel ObjModel)
        {
            return Context.SaveProfile(ObjModel.UserId,ObjModel.FirstName,ObjModel.LastName,ObjModel.ProfileImage,ObjModel.Description,ObjModel.EmailId,ObjModel.ProfessionalCatId,ObjModel.Title,ObjModel.ProfessionalKeyword,ObjModel.CityId,ObjModel.Password,ObjModel.GenderId,ObjModel.DOB).FirstOrDefault();
        }

        public SaveUserInterest_Result SaveUserInterest(UserInterestModel objModel)
        {
            var Result = Context.SaveUserInterest(objModel.XmlData);
            return Result.FirstOrDefault();
        }

        IEnumerable<GetUsersProfileList_Result> IUserRepositary.GetUserProfileList()
        {
            return Context.GetUsersProfileList().ToList();
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
        IEnumerable<GetUsersProfileList_Result> GetUserProfileList();
    }
}