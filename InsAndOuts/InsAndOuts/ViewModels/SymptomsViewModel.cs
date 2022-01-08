using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.Contacts;
using InsAndOuts.Models;

namespace InsAndOuts.ViewModels
{
    public class SymptomsViewModel : ViewModelBase
    {
        public Pain              Pain                   { get; set; }
        public List<Pain>        Pains                  { get; set; }
        public List<SymptomType> SymptomTypes           => GetSymptomTypes();
        public SymptomType       SelectedSymptomType    { get; set; }

        public SymptomsViewModel()
        {
            Pain                = new Pain();
            Pains               = DataAccessLayer.GetAllPain();
            SelectedSymptomType = new SymptomType();
        }
        
        public SymptomsViewModel(string searchText)
        {
            //Ex: '1/6/2022 04:41 PM - Bloated - Level: 4'
            var searchTerms = searchText.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

            if (searchTerms.Length < 3)
            {
                return;
            }

            var symptomType = searchTerms[1];

            var painLevel = searchTerms[2]
                           .Split(':')[1]
                           .Trim();

            Pain = DataAccessLayer.GetAllPain()
                                  .FirstOrDefault(
                                                    field => field.When == searchTerms[0] 
                                                          && field.Level == int.Parse(painLevel)
                                                          && field.Type.Name == symptomType
                                                  );
        }

        private List<SymptomType> GetSymptomTypes()
        {
            var symptomTypesList = DataAccessLayer.GetAllSymptomTypes();
            symptomTypesList.Add(new SymptomType{Name = "<Add>"});

            return symptomTypesList.OrderBy(fields => fields.Name)
                                   .ToList();
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

        public void AddNewSymptomType(string name)
        {
            DataAccessLayer.InsertSymptomType(new SymptomType
                                              {
                                                  Name = name
                                              });
        }
    }
}
