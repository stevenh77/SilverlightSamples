using System;
using System.ComponentModel;
using System.Windows.Input;

namespace SilverlightValidation.Interfaces
{
    public interface IUserViewModel : INotifyDataErrorInfo, INotifyPropertyChanged, IUserModel
    {
        bool IsChanged { get; }
        ICommand OkCommand { get; set; }
        ICommand CancelCommand { get; set; }
    }
}