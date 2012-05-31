using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FluentValidation;
using SilverlightValidation.Interfaces;
using SilverlightValidation.Models;
using SilverlightValidation.Commands;
using GalaSoft.MvvmLight.Messaging;
using SilverlightValidation.Messages;

namespace SilverlightValidation.ViewModels
{
    public class UserViewModel : ViewModelBase, IUserViewModel, IEditableObject
    {
        #region Fields

        private readonly AbstractValidator<IUserModel> _validator;
        private UserModel _data;
        private UserModel _backup;

        #endregion

        #region Constructor

        public UserViewModel(UserModel model, AbstractValidator<IUserModel> validator)
        {
            if (model == null) throw new ArgumentNullException("model");
            if (validator == null) throw new ArgumentNullException("validator");

            _validator = validator;
            _data = model;
            _backup = model.Clone() as UserModel;

            OkCommand = new RelayCommand(OkCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
        }

        #endregion

        #region Methods

        private void SetProperties(IUserModel source)
        {
            _data.Username = source.Username;
            _data.Password = source.Password;
            _data.Email = source.Email;
            _data.DateOfBirth = source.DateOfBirth;
            _data.Description = source.Description;
        }

        #endregion

        #region Properties

        private const string UsernameProperty = "Username";
        public string Username
        {
            get { return _data.Username; }
            set
            {
                if (_data.Username != value)
                {
                    _data.Username = value;
                    RaisePropertyChanged(UsernameProperty);
                    IsChanged = true;
                }

                ClearError(UsernameProperty);
                var validationResult = _validator.Validate(this, UsernameProperty);
                if (!validationResult.IsValid)
                    validationResult.Errors.ToList().ForEach(x => SetError(UsernameProperty, x.ErrorMessage));
            }
        }

        private const string PasswordProperty = "Password";
        public string Password
        {
            get { return _data.Password; }
            set
            {
                if (_data.Password != value)
                {
                    _data.Password = value;
                    RaisePropertyChanged(PasswordProperty);
                    IsChanged = true;
                }

                ClearError(PasswordProperty);
                var validationResult = _validator.Validate(this, PasswordProperty);
                if (!validationResult.IsValid)
                    validationResult.Errors.ToList().ForEach(x => SetError(PasswordProperty, x.ErrorMessage));
            }
        }

        private const string EmailProperty = "Email";
        public string Email
        {
            get { return _data.Email; }
            set
            {
                if (_data.Email != value)
                {
                    _data.Email = value;
                    RaisePropertyChanged(EmailProperty);
                    IsChanged = true;
                }

                ClearError(EmailProperty);
                var validationResult = _validator.Validate(this, EmailProperty);
                if (!validationResult.IsValid)
                    validationResult.Errors.ToList().ForEach(x => SetError(EmailProperty, x.ErrorMessage));
            }
        }

        private const string DateOfBirthProperty = "DateOfBirth";
        public DateTime? DateOfBirth
        {
            get { return _data.DateOfBirth; }
            set
            {
                if (_data.DateOfBirth != value)
                {
                    _data.DateOfBirth = value;
                    RaisePropertyChanged(DateOfBirthProperty);
                    IsChanged = true;
                }

                ClearError(DateOfBirthProperty);
                var validationResult = _validator.Validate(this, DateOfBirthProperty);
                if (!validationResult.IsValid)
                    validationResult.Errors.ToList().ForEach(x => SetError(DateOfBirthProperty, x.ErrorMessage));
            }
        }

        private const string DescriptionProperty = "Description";
        public string Description
        {
            get { return _data.Description; }
            set
            {
                if (_data.Description != value)
                {
                    _data.Description = value;
                    RaisePropertyChanged(DescriptionProperty);
                    IsChanged = true;
                }

                ClearError(DescriptionProperty);
                var validationResult = _validator.Validate(this, DescriptionProperty);
                if (!validationResult.IsValid)
                    validationResult.Errors.ToList().ForEach(x => SetError(DescriptionProperty, x.ErrorMessage));
            }
        }

        #endregion

        #region Commands

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private void OkCommandExecute(object obj)
        {
            RefreshToViewErrors();

            if (IsChanged && !HasErrors)
            {
                // save here
                Messenger.Default.Send(new UserViewResponseMessage() { UserViewModel = this });
            }
        }

        // in case user hasn't touched the form
        private void RefreshToViewErrors()
        {
            Username = _data.Username;
            Password = _data.Password;
            Email = _data.Email;
            DateOfBirth = _data.DateOfBirth;
        }

        private void CancelCommandExecute(object obj)
        {
            Messenger.Default.Send(new UserViewResponseMessage() { UserViewModel = null });
        }
        
        #endregion

        private void ResetFormData()
        {
            SetProperties(_backup);
            ClearAllErrors();
            IsChanged = false;
        }

        public bool IsChanged { get; private set; }

        #region IEditableObject for datagrid

        private bool inEdit;
        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            ResetFormData();
        }

        public void EndEdit()
        {
            if (!inEdit) return;
        }

        #endregion
    }
}
