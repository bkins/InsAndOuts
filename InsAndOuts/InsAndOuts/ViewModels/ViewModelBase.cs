using InsAndOuts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InsAndOuts.Data;
using Xamarin.Forms;

namespace InsAndOuts.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public DataAccess DataAccess;
    
        public DataAccess DataAccessLayer
        {
            get => DataAccess = DataAccess ?? new DataAccess(App.Database);
            set => DataAccess = value;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
    
            handler?.Invoke(this
                          , new PropertyChangedEventArgs(propertyName));
        }
    
        protected void SetValue<T>(ref T                     backingField
                                 , T                         value
                                 , [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField
                                                 , value))
            {
                return;
            }
    
            backingField = value;
    
            OnPropertyChanged(propertyName);
        }
    }
}
