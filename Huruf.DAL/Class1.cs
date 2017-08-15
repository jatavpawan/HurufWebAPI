

namespace Huruf.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    #region ["Return Values"]
    public class ReturnValues
    {
        public string Success { get; set; }
        public string Failure { get; set; }
        public string Source { get; set; }
        public bool Status { get; set; }
        public string UserLikes { get; set; }

    }
    #endregion

    #region Login
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GCMId { get; set; }
    }
    #endregion


    public partial class AddBlogData
    {

        public int BlogId { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string textContent { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int PrivacyID { get; set; }
        public string UserLikes { get; set; }
        public List<string> Fileinfo { get; set; }
        public List<UserDataRegister> Userinfo { get; set; }
    }

    public partial class UserRegister
    {
        public byte[] FileName { get; set; }
        public string FilePathName { get; set; }
    }

    public partial class ChangeUserPassword
    {
        public int RegistrationID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }


    public class BlogData
    {
        public int BlogId { get; set; }
        public string ImageName { get; set; }
    }

    public class UserDataRegister
    {
        public string FilePathName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RegistrationID { get; set; }
        public byte[] FileName { get; set; }
        public string GCMId { get; set; }
        public System.DateTime CreateDate { get; set; }
    }


}
