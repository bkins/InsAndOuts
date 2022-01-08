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

        public void InsertPain(Pain pain)
        {
            Database.AddPain(pain);
        }

        public void UpdatePain(Pain pain)
        {
            Database.UpdatePain(pain);
        }
        
        /// <summary>
        /// Returns a list of *unique* Symptom Types
        /// </summary>
        /// <returns></returns>
        public List<SymptomType> GetAllSymptomTypes()
        {
            return Database.GetAllSymptomTypes()
                           .Distinct()
                           .ToList();
        }

        public int InsertSymptomType(SymptomType symptomType)
        {
            return Database.AddSymptomType(symptomType);
        }

        public int UpdateSymptomType(SymptomType symptomType)
        {
            return Database.UpdateSymptomType(symptomType);
        }

        public int DeleteSymptomType(ref SymptomType symptomType)
        {
            return Database.DeleteSymptomType(ref symptomType);
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
