using System;

namespace SilverlightValidation.Interfaces
{
    public interface IUserModel
    {
        string Username { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        DateTime? DateOfBirth { get; set; }
        string Description { get; set; }
    }
}
