using System;
using System.Collections.Generic;
using SilverlightValidation.Models;

namespace SilverlightValidation.Data
{
    public class Factory
    {
        public static IList<UserModel> CreateUserModels()
        {
            return new List<UserModel>(5)
            {
                UserModel.Create("StevenH", "Password1", "steven@hotmail.com", new DateTime(1977, 09, 01), ""),
                UserModel.Create("RichardJ", "12N456a", "dicky@gmail.com", new DateTime(1983, 03, 13), "Loves .Net!"),
                UserModel.Create("BobbyP", "pa33Word", "bob@yahoo.co.uk", new DateTime(1992, 08, 30), ""),
                UserModel.Create("DavidM", "poIu789", "daveyboy@marsh.com", new DateTime(1965, 06, 21), "Java fan boy"),
                UserModel.Create("JessieJ", "jlkJh567", "jj@apple.co.uk", new DateTime(1990, 10, 15), "")
            };
        }
    }
}
