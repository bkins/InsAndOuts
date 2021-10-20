using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public PainViewModel(string searchText)
        {
            var searchTerms = searchText.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

            if (searchTerms.Length < 2)
            {
                return;
            }

            var painLevel = searchTerms[1]
                           .Split(':')[1]
                           .Trim();

            Pain = DataAccessLayer.GetAllPain()
                                  .FirstOrDefault(field => field.When == searchTerms[0] 
                                                        && field.Level == int.Parse(painLevel));
        }

        public void Save()
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

        public void Delete()
        {
            if (Pain.Id != 0)
            {
                DataAccessLayer.DeletePain(Pain);
            }
        }
    }
}
