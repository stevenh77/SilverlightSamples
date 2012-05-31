using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MetroWpf.Messages;

namespace MetroWpf.UI.Login
{
    /// <summary>
    /// Login view view model class
    /// </summary>
    public sealed class LoginViewModel :
      ViewModelBase
    {
        #region · Data Properties ·

        /// <summary>
        /// Gets or sets whether the form is busy
        /// </summary>
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (this.isBusy != value)
                {
                    this.isBusy = value;
                    RaisePropertyChanged("IsBusy");
                    //relay requery?
                }
            }
        }

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        private string userId;
        public string UserId
        {
            get { return this.userId; }
            set
            {
                if (this.userId != value)
                {
                    this.userId = value;
                    RaisePropertyChanged("UserId");
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    RaisePropertyChanged("Password");
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region · Constructors ·

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class
        /// </summary>
        public LoginViewModel()
            : base()
        {
            LoginCommand = new RelayCommand(LoginCommandExecute, CanLoginCommandExecute);
            CloseCommand = new RelayCommand(CloseCommandExecute);
        }

        #endregion

        #region · Command Actions ·

        #region LoginCommand

        public RelayCommand LoginCommand { get; set; }

        private void LoginCommandExecute()
        {
            //successful login!

            Messenger.Default.Send(new UserAuthenticatedMessage() { UserId = userId });
        }

        private bool CanLoginCommandExecute()
        {
            if (string.IsNullOrEmpty(UserId) ||
              string.IsNullOrEmpty(Password))
                return false;

            return true;
        }

        #endregion

        #region CloseCommand
        public RelayCommand CloseCommand { get; set; }

        private void CloseCommandExecute()
        {
        }
        #endregion

        #endregion
    }
}