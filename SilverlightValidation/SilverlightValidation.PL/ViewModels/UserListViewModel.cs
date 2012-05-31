using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using SilverlightValidation.Commands;
using SilverlightValidation.Models;
using SilverlightValidation.Validators;
using GalaSoft.MvvmLight.Messaging;
using SilverlightValidation.Messages;

namespace SilverlightValidation.ViewModels
{
    public class UserListViewModel
    {
        private ChildWindow _window;

        public UserListViewModel(ChildWindow window, IList<UserModel> models, UserModelValidator validator)
        {
            _window = window;

            Data = new ObservableCollection<UserViewModel>();

            foreach (var model in models)
                Data.Add(new UserViewModel(model, validator));

            AddCommand = new RelayCommand(AddCommandExecute);
            DeleteCommand = new RelayCommand(DeleteCommandExecute);

            Messenger.Default.Register<UserViewResponseMessage>(this, UserViewResponseMessageReceived);
        }

        private void UserViewResponseMessageReceived(UserViewResponseMessage userViewResponseMessage)
        {
            if (userViewResponseMessage.UserViewModel != null)
                Data.Add(userViewResponseMessage.UserViewModel);
            _window.Close();
        }

        #region Properties

        public ObservableCollection<UserViewModel> Data { get; set; }

        public UserViewModel SelectedItem { get; set; }

        #endregion

        #region Commands

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void AddCommandExecute(object obj)
        {
            _window.Show();
        }

        private void DeleteCommandExecute(object obj)
        {
            if (SelectedItem!=null)
                Data.Remove(SelectedItem);
        }

        #endregion
    }
}