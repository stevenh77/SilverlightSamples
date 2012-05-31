using System;
using SilverlightValidation.Interfaces;

namespace SilverlightValidation.Models
{
    public class UserModel : IUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }

        private UserModel() { }

        public static UserModel Create(string username = "",
                                        string password = "",
                                        string email = "",
                                        DateTime? dateOfBirth = null,
                                        string descripton = "")
        {
            return new UserModel()
                       {
                           Username = username,
                           Password = password,
                           Email = email,
                           DateOfBirth = dateOfBirth,
                           Description = descripton
                       };
        }

        public IUserModel Clone()
        {
            return (UserModel)this.MemberwiseClone();
        }
    }
}
