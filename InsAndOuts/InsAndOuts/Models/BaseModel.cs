using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Avails.D_Flat;
using InsAndOuts.Data;
using InsAndOuts.Utilities;

namespace InsAndOuts.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public string   Name                 { get; set; }
        public string   DescriptionPlainText { get; set; }
        public string   DescriptionHtml      { get; set; }
        public string   When                 { get; set; }
        public DateTime WhenDateTime         => DateTime.Parse(When);
        public string   WhenShortTime        => WhenDateTime.ToShortTimeString();
        public double   WhenOaDate           => WhenDateTime.ToOADate();

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseModel()
        {
            Name                 = string.Empty;
            DescriptionPlainText = string.Empty;
            DescriptionHtml      = string.Empty;
            When                 = DateTime.Now.ToShortDateTimeString(); //$"{DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";;
        }
        
        public DateTime WhenToDateTime()
        {
            return DateTime.Parse(When);
        }

        public TimeSpan WhenToTimeSpan()
        {
            if (When.IsNullEmptyOrWhitespace())
            {
                return new TimeSpan();
            }

            var time = When.Split(' ')[1];
            return TimeSpan.Parse(time);
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
