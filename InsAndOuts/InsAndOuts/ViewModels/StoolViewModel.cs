using System;
using System.Collections.Generic;
using System.Text;
using InsAndOuts.Models;

namespace InsAndOuts.ViewModels
{
    public class StoolViewModel : ViewModelBase
    {
        public Stool       Stool  { get; set; }
        public List<Stool> Stools { get; set; }

        public StoolViewModel()
        {
            Stool  = new Stool();
            Stools = DataAccessLayer.GetAllStools();
        }

        public void SaveStool()
        {
            if (Stool.Id == 0)
            {
                DataAccessLayer.InsertStool(Stool);
            }
            else
            {
                DataAccessLayer.UpdateStool(Stool);
            }
        }
    }
}
