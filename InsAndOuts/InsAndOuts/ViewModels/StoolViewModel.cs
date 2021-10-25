using System;
using System.Collections.Generic;
using System.Linq;
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

        public StoolViewModel(string searchText)
        {
            var searchTerms = searchText.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

            if (searchTerms.Length < 2)
            {
                return;
            }
            
            Stool = DataAccessLayer.GetAllStools()
                                   .FirstOrDefault(field => field.When == searchTerms[0] 
                                                         && field.StoolType.Contains(searchTerms[1]));
        }

        public void Save()
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

        public void Delete()
        {
            if (Stool.Id != 0)
            {
                DataAccessLayer.DeleteStool(Stool);
            }
        }
    }
}
