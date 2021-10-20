using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsAndOuts.Models;

namespace InsAndOuts.Data
{
    public class DataAccess
    {
        private IDataStore Database { get; set; }

        public DataAccess(IDataStore database)
        {
            Database = database;
        }

        public void CreateTables()
        {
            Database.CreateTables();
        }

        public void DropTables()
        {
            Database.DropTables();
        }

        public int InsertMeal(Meal meal)
        {
            return Database.AddMeal(meal);
        }

        public void UpdateMeal(Meal meal)
        {
            Database.UpdateMeal(meal);
        }

        public List<Meal> GetAllMeals()
        {
            return Database.GetAllMeals().ToList();
        }

        public List<Pain> GetAllPain()
        {
            return Database.GetAllPain()
                           .ToList();
        }

        public int InsertPain(Pain pain)
        {
            return Database.AddPain(pain);
        }

        public int UpdatePain(Pain pain)
        {
            return Database.UpdatePain(pain);
        }

        public void InsertStool(Stool stool)
        {
            Database.AddStoolWithType(stool);
        }

        public void UpdateStool(Stool stool)
        {
            Database.UpdateStool(stool);
        }

        public List<Stool> GetAllStools()
        {
            return Database.GetAllStools()
                           .ToList();
        }

        public int DeleteMeal(Meal meal)
        {
            return Database.DeleteMeal(ref meal);
        }

        public int DeleteStool(Stool stool)
        {
            return Database.DeleteStool(ref stool);
        }

        public int DeletePain(Pain pain)
        {
            return Database.DeletePain(ref pain);
        }
    }
}
