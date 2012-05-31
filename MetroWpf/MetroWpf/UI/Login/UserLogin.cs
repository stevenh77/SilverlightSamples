using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace MetroWpf.UI.Login
{
  public sealed class UserLogin : 
    ObservableObject, 
    IDataErrorInfo
  {
    #region · Fields ·

    private Dictionary<string, string> _errors 
      = new Dictionary<string, string>();

    #endregion

    #region · IDataErrorInfo Members ·

    public string Error
    {
      get { return null; }
    }

    public string this[string columnName]
    {
      get { return null; }
    }

    #endregion

    #region · Properties ·

    public string UserId { get; set; }
    public string Password { get; set; }

    #endregion

    #region · Constructors ·

    public UserLogin()
    {
    }

    #endregion
  }
}
