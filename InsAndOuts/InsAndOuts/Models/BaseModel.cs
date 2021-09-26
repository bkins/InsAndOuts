using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using InsAndOuts.Data;

namespace InsAndOuts.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public string Name                { get; set; }
        public string DescriptionPainText { get; set; }
        public string DescriptionHtml     { get; set; }
        public string When                { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseModel()
        {
            Name                = string.Empty;
            DescriptionPainText = string.Empty;
            DescriptionHtml     = string.Empty;
            When                = $"{DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";;
        }

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
