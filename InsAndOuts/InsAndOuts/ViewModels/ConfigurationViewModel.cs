using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using InsAndOuts.Models;

namespace InsAndOuts.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {

        public ConfigurationViewModel()
        {
            
        }

        public void ClearData()
        {
            DataAccessLayer.DropTables();
            DataAccessLayer.CreateTables();
        }
    }
}
