using System;
using System.Collections.Generic;
using System.Text;
using InsAndOuts.Models;

namespace InsAndOuts.ViewModels
{
    public class PainViewModel : ViewModelBase
    {
        public Pain       Pain  { get; set; }
        public List<Pain> Pains { get; set; }

        public PainViewModel()
        {
            Pain  = new Pain();
            Pains = DataAccessLayer.GetAllPain();
        }

        public void SavePain()
        {
            if (Pain.Id == 0)
            {
                DataAccessLayer.InsertPain(Pain);
            }
            else
            {
                DataAccessLayer.UpdatePain(Pain);
            }
        }
    }
}
