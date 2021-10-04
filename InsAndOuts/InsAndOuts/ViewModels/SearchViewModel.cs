using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsAndOuts.Models;

namespace InsAndOuts.ViewModels
{
    public class SearchViewModel: ViewModelBase
    {
        public List<string> SearchableMeals  => BuildMealsList();
        public List<string> SearchableStools => BuildStoolsList();
        public List<string> SearchablePains  => BuildPainsList();
        
        public SearchViewModel()
        {
            
        }

        public  List<string> BuildMealsList()
        {
            var meals                 = DataAccessLayer.GetAllMeals();

            return meals.Select(meal => meal.ToString())
                        .ToList();
        }

        public List<string> BuildStoolsList()
        {
            var stools = DataAccessLayer.GetAllMeals();

            return stools.Select(stool => stool.ToString())
                         .ToList();
        }
        
        public List<string> BuildPainsList()
        {
            var pains = DataAccessLayer.GetAllMeals();

            return pains.Select(pain => pain.ToString())
                        .ToList();
        }
    }
}
