using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsAndOuts.Models;

namespace InsAndOuts.ViewModels
{
    public class MealsViewModel : ViewModelBase
    {
        public Meal       Meal { get; set; }
        public List<Meal> Meals { get; set; }
        public MealsViewModel()
        {
            Meal = new Meal();
            Meals = DataAccessLayer.GetAllMeals();
        }

        public MealsViewModel(string searchText)
        {
            var searchTerms = searchText.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

            if (searchTerms.Length < 2)
            {
                return;
            }

            Meal = DataAccessLayer.GetAllMeals()
                                  .FirstOrDefault(field => field.When == searchTerms[0] 
                                                        && field.Name == searchTerms[1]);
        }

        public void Save()
        {
            var mealId = 0;

            if (Meal.Id == 0)
            {
                mealId = DataAccessLayer.InsertMeal(Meal);
            }
            else
            {
                DataAccessLayer.UpdateMeal(Meal);
            }
        }
    }
}
