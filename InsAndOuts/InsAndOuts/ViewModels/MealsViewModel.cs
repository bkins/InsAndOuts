using System;
using System.Collections.Generic;
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
