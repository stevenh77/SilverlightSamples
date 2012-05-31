using System.Windows.Browser;
using System.Windows.Input;
using SilverlightValidation.Models;
using SilverlightValidation.Validators;
using SilverlightValidation.ViewModels;

namespace SilverlightValidation.Views
{
    public partial class UserView
    {
        private UserViewModel vm;

        public UserView()
        {
            InitializeComponent();
            HtmlPage.Document.SetProperty("title", "Silverlight Validation");

            vm = new UserViewModel(UserModel.Create(), UserModelValidator.Create());
            this.DataContext = vm;
        }

        private void DatePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Tab)
                e.Handled = true;
        }
    }
}