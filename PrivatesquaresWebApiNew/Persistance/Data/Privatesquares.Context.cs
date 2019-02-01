﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrivatesquaresWebApiNew.Persistance.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EWT_PSQNEWEntities : DbContext
    {
        public EWT_PSQNEWEntities()
            : base("name=EWT_PSQNEWEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<InterestCategories_Result> InterestCategories()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InterestCategories_Result>("InterestCategories");
        }
    
        public virtual ObjectResult<LoginAuthenticate_Result> LoginAuthenticate(string mobile, string oTP)
        {
            var mobileParameter = mobile != null ?
                new ObjectParameter("Mobile", mobile) :
                new ObjectParameter("Mobile", typeof(string));
    
            var oTPParameter = oTP != null ?
                new ObjectParameter("OTP", oTP) :
                new ObjectParameter("OTP", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LoginAuthenticate_Result>("LoginAuthenticate", mobileParameter, oTPParameter);
        }
    
        public virtual ObjectResult<RegisterUser_Result> RegisterUser(string userMobile)
        {
            var userMobileParameter = userMobile != null ?
                new ObjectParameter("UserMobile", userMobile) :
                new ObjectParameter("UserMobile", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RegisterUser_Result>("RegisterUser", userMobileParameter);
        }
    
        public virtual ObjectResult<SaveUserProfile_Result> SaveUserProfile(Nullable<long> userId, string firstName, string lastName, string profileImage, string description, string emailId, Nullable<long> professionalCatId, string title, Nullable<long> professionalKeywordId, Nullable<long> cityId, string password, Nullable<long> genderId, Nullable<System.DateTime> dOB)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(long));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var profileImageParameter = profileImage != null ?
                new ObjectParameter("ProfileImage", profileImage) :
                new ObjectParameter("ProfileImage", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var emailIdParameter = emailId != null ?
                new ObjectParameter("EmailId", emailId) :
                new ObjectParameter("EmailId", typeof(string));
    
            var professionalCatIdParameter = professionalCatId.HasValue ?
                new ObjectParameter("ProfessionalCatId", professionalCatId) :
                new ObjectParameter("ProfessionalCatId", typeof(long));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var professionalKeywordIdParameter = professionalKeywordId.HasValue ?
                new ObjectParameter("ProfessionalKeywordId", professionalKeywordId) :
                new ObjectParameter("ProfessionalKeywordId", typeof(long));
    
            var cityIdParameter = cityId.HasValue ?
                new ObjectParameter("CityId", cityId) :
                new ObjectParameter("CityId", typeof(long));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var genderIdParameter = genderId.HasValue ?
                new ObjectParameter("GenderId", genderId) :
                new ObjectParameter("GenderId", typeof(long));
    
            var dOBParameter = dOB.HasValue ?
                new ObjectParameter("DOB", dOB) :
                new ObjectParameter("DOB", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SaveUserProfile_Result>("SaveUserProfile", userIdParameter, firstNameParameter, lastNameParameter, profileImageParameter, descriptionParameter, emailIdParameter, professionalCatIdParameter, titleParameter, professionalKeywordIdParameter, cityIdParameter, passwordParameter, genderIdParameter, dOBParameter);
        }
    
        public virtual ObjectResult<UserInterestedCategory_Result> UserInterestedCategory(Nullable<long> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserInterestedCategory_Result>("UserInterestedCategory", userIdParameter);
        }
    
        public virtual ObjectResult<SaveProfile_Result> SaveProfile(Nullable<long> userId, string firstName, string lastName, string profileImage, string description, string emailId, Nullable<long> professionalCatId, string title, Nullable<long> professionalKeywordId, Nullable<long> cityId, string password, Nullable<long> genderId, Nullable<System.DateTime> dOB)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(long));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var profileImageParameter = profileImage != null ?
                new ObjectParameter("ProfileImage", profileImage) :
                new ObjectParameter("ProfileImage", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var emailIdParameter = emailId != null ?
                new ObjectParameter("EmailId", emailId) :
                new ObjectParameter("EmailId", typeof(string));
    
            var professionalCatIdParameter = professionalCatId.HasValue ?
                new ObjectParameter("ProfessionalCatId", professionalCatId) :
                new ObjectParameter("ProfessionalCatId", typeof(long));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var professionalKeywordIdParameter = professionalKeywordId.HasValue ?
                new ObjectParameter("ProfessionalKeywordId", professionalKeywordId) :
                new ObjectParameter("ProfessionalKeywordId", typeof(long));
    
            var cityIdParameter = cityId.HasValue ?
                new ObjectParameter("CityId", cityId) :
                new ObjectParameter("CityId", typeof(long));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var genderIdParameter = genderId.HasValue ?
                new ObjectParameter("GenderId", genderId) :
                new ObjectParameter("GenderId", typeof(long));
    
            var dOBParameter = dOB.HasValue ?
                new ObjectParameter("DOB", dOB) :
                new ObjectParameter("DOB", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SaveProfile_Result>("SaveProfile", userIdParameter, firstNameParameter, lastNameParameter, profileImageParameter, descriptionParameter, emailIdParameter, professionalCatIdParameter, titleParameter, professionalKeywordIdParameter, cityIdParameter, passwordParameter, genderIdParameter, dOBParameter);
        }
    
        public virtual int SaveUserInterest()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SaveUserInterest");
        }
    }
}
