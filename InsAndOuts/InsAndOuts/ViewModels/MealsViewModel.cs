using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsAndOuts.Models;
using InsAndOuts.Utilities;

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
            Logger.WriteLine($"About to save {nameof(Meal)}:"
                           , Category.Information);
            LogMeal(Meal);

            if (Meal.Id == 0)
            {
                Logger.WriteLine($"Inserting new {nameof(Meal)}", Category.Information);
                
                DataAccessLayer.InsertMeal(Meal);
                
                Logger.WriteLine($"{nameof(Meal)} inserted as:", Category.Information);
                LogMeal(Meal);
            }
            else
            {
                Logger.WriteLine($"Updating existing {nameof(Meal)}. Before update:", Category.Information);
                LogMeal(Meal);
                
                DataAccessLayer.UpdateMeal(Meal);

                Logger.WriteLine("After update:", Category.Information);
                LogMeal(Meal);
            }
        }

        private void LogMeal(Meal meal)
        {
            Logger.WriteLine($"   {nameof(meal.Id)}:                  '{meal.Id}'"
                           , Category.Information);
            Logger.WriteLine($"   {nameof(meal.Name)}:                '{meal.Name}'"
                           , Category.Information);
            Logger.WriteLine($"   {nameof(meal.DescriptionHtml)}:     '{meal.DescriptionHtml}'"
                           , Category.Information);
            Logger.WriteLine($"   {nameof(meal.DescriptionPlainText)}: '{meal.DescriptionPlainText}'"
                           , Category.Information);
            Logger.WriteLine($"   {nameof(meal.When)}:                '{meal.When}'"
                           , Category.Information);

        }
        public void Delete()
        {
            if (Meal?.Id != 0)
            {
                var deletedMealId = DataAccessLayer.DeleteMeal(Meal);
            }
        }
    }
}
