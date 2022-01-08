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

        int  AddMeal         (Meal          meal);
        void AddPain         (Pain          pain);
        int  AddSymptomType  (SymptomType   symptomType);
        void AddStoolWithType(Stool         stool);

    #endregion

    #region Gets

        Meal                     GetMeal            (int mealId);
        IEnumerable<Meal>        GetAllMeals        ();
        Pain                     GetPain            (int painId);
        IEnumerable<Pain>        GetAllPain         ();
        IEnumerable<SymptomType> GetAllSymptomTypes ();
        SymptomType              GetSymptomType     (string   name);
        Stool                    GetStool           (int stoolId);
        IEnumerable<Stool>       GetAllStools       ();
        StoolType                GetStoolType       (int stoolTypeId);
        IEnumerable<StoolType>   GetAllStoolTypes   ();

    #endregion

    #region Updates

        int  UpdateMeal         (Meal           meal);
        void UpdatePain         (Pain           pain);
        int  UpdateSymptomType  (SymptomType    symptomType);
        void UpdateStool        (Stool          stool);
        int  UpdateStoolType    (StoolType      stoolType);
        
    #endregion

    #region Deletes

        int DeleteMeal       (ref Meal        meal);
        int DeletePain       (ref Pain        pain);
        int DeleteStool      (ref Stool       stool);
        int DeleteSymptomType(ref SymptomType symptomType);
        int DeleteStoolType  (ref StoolType   stoolType);

        #endregion
    }
}
