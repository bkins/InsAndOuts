using System.Collections.Generic;
using System.Threading.Tasks;
using InsAndOuts.Models;

namespace InsAndOuts.Data
{
    public interface IDataStore
    {
        void CreateTables();
        
        void DropTables();
        
        void RefreshDatabase();

    #region Adds

        int  AddMeal         (Meal meal);
        int  AddPain         (Pain pain);
        void AddStoolWithType(Stool stool);

    #endregion

    #region Gets

        Meal                   GetMeal         (int mealId);
        IEnumerable<Meal>      GetAllMeals     ();
        Pain                   GetPain         (int painId);
        IEnumerable<Pain>      GetAllPain      ();
        Stool                  GetStool        (int stoolId);
        IEnumerable<Stool>     GetAllStools    ();
        StoolType              GetStoolType    (int stoolTypeId);
        IEnumerable<StoolType> GetAllStoolTypes();

    #endregion

    #region Updates

        int  UpdateMeal     (Meal      meal);
        int  UpdatePain     (Pain      pain);
        void UpdateStool    (Stool     stool);
        int  UpdateStoolType(StoolType stoolType);
        
    #endregion

    #region Deletes

        int DeleteMeal     (ref Meal      meal);
        int DeletePain     (ref Pain      pain);
        int DeleteStool    (ref Stool     stool);
        int DeleteStoolType(ref StoolType stoolType);

        #endregion
    }
}
