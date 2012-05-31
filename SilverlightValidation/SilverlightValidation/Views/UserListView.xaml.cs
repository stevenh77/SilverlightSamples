using System.Windows.Browser;
using System.Windows.Input;
using SilverlightValidation.Data;
using SilverlightValidation.ViewModels;
using SilverlightValidation.Validators;

namespace SilverlightValidation.Views
{
    public partial class UserListView
    {
        private UserListViewModel vm;

        public UserListView()
        {
            InitializeComponent();
            HtmlPage.Document.SetProperty("title", "Silverlight Validation");

            vm = new UserListViewModel(new UserView(), Factory.CreateUserModels(), UserModelValidator.Create());
            this.DataContext = vm;
        }

        private void DatePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Tab)
                e.Handled = true;
        }
    }
}
