using System;
using System.Collections.Generic;
using System.Text;
using InsAndOuts.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace InsAndOuts.Data
{
    public class Database : IDataStore
    {
        private readonly SQLiteConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            CreateTables();
        }
        
        public string DbPath()
        {
            return _database.DatabasePath;
        }

        public void CreateTables()
        {
            _database.CreateTable<Meal>();
            _database.CreateTable<Pain>();
            _database.CreateTable<Stool>();
            //_database.CreateTable<StoolType>();
        }

        public void DropTables()
        {
            _database.DropTable<Meal>();
            _database.DropTable<Pain>();
            _database.DropTable<Stool>();
            //_database.DropTable<StoolType>();
        }

        public void RefreshDatabase()
        {
            DropTables();
            CreateTables();
        }

    #region Adds
        
        public int AddMeal(Meal meal)
        {
            return _database.Insert(meal);
        }

        public int AddPain(Pain pain)
        {
            return _database.Insert(pain);
        }

        public void AddStoolWithType(Stool stool)
        {
            _database.InsertWithChildren(stool);
        }

    #endregion

    #region Gets

        public Meal GetMeal(int mealId)
        {
            return _database.Get<Meal>(mealId);
        }

        public IEnumerable<Meal> GetAllMeals()
        {
            return _database.GetAllWithChildren<Meal>();
        }

        public Pain GetPain(int painId)
        {
            return _database.Get<Pain>(painId);
        }

        public IEnumerable<Pain> GetAllPain()
        {
            return _database.GetAllWithChildren<Pain>();
        }

        public Stool GetStool(int stoolId)
        {
            return _database.Get<Stool>(stoolId);
        }

        public IEnumerable<Stool> GetAllStools()
        {
            return _database.GetAllWithChildren<Stool>();

        }

        public StoolType GetStoolType(int stoolTypeId)
        {
            return _database.Get<StoolType>(stoolTypeId);
        }

        public IEnumerable<StoolType> GetAllStoolTypes()
        {
            return _database.GetAllWithChildren<StoolType>();
        }

    #endregion

    #region Updates

        public int UpdateMeal(Meal meal)
        {
            return _database.Update(meal);
        }

        public int UpdatePain(Pain pain)
        {
            return _database.Update(pain);
        }

        public void UpdateStool(Stool stool)
        {
            _database.UpdateWithChildren(stool);
        }

        public int UpdateStoolType(StoolType stoolType)
        {
            return _database.Update(stoolType);
        }
        
    #endregion

    #region Deletes

        public int DeleteMeal(ref Meal meal)
        {
            var mealId = _database.Delete(meal);
            meal = null;

            return mealId;
        }

        public int DeletePain(ref Pain pain)
        {
            var painId = _database.Delete(pain);
            pain = null;

            return painId;
        }

        public int DeleteStool(ref Stool stool)
        {
            var stoolId = _database.Delete(stool);
            stool = null;

            return stoolId;
        }

        public int DeleteStoolType(ref StoolType stoolType)
        {
            var stoolTypeId = _database.Delete(stoolType);
            stoolType = null;

            return stoolTypeId;
        }

    #endregion
    }
}
