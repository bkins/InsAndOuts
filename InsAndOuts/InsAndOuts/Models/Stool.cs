using System;
using System.Collections.Generic;
using System.Text;
using InsAndOuts.Utilities;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace InsAndOuts.Models
{
    [Table("Stools")]
    public class Stool : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id                 { get; set; }
        public byte[] Image           { get; set; }
        public string ImageFileName   { get; set; }
        public string StoolType       { get; set; }
        public int    StoolTypeNumber { get; set; }

        public Stool()
        {
            StoolType     = string.Empty;
            Image         = Array.Empty<byte>();
            ImageFileName = string.Empty;
        }

        public Stool(int typeNumber)
        {
            StoolTypeNumber = typeNumber;
            StoolType       = ConvertNumberToStoolType(typeNumber);
            Image           = Array.Empty<byte>();
            ImageFileName   = string.Empty;
        }

        public static string ConvertNumberToStoolType(int typeNumber)
        {
            /*
             *Type 1: Separate hard lumps, like nuts (difficult to pass and can be black)</x:String>
             *Type 2: Sausage-shaped, but lumpy</x:String>
             *Type 3: Like a sausage but with cracks on its surface (can be black)</x:String>
             *Type 4: Like a sausage or snake, smooth and soft (average stool)</x:String>
             *Type 5: Soft blobs with clear cut edges</x:String>
             *Type 6: Fluffy pieces with ragged edges, a mushy stool (diarrhea)</x:String>
             *Type 7: Watery, no solid pieces, entirely liquid (diarrhea)</x:String>
             */
            switch (typeNumber)
            {
                case 1:

                    return "Type 1: Separate hard lumps, like nuts (difficult to pass and can be black)";
                    
                case 2:

                    return "Type 2: Sausage-shaped, but lumpy";

                case 3:

                    return "Type 3: Like a sausage but with cracks on its surface (can be black)";

                case 4:

                    return "Type 4: Like a sausage or snake, smooth and soft (average stool)";

                case 5:

                    return "Type 5: Soft blobs with clear cut edges";

                case 6:

                    return "Type 6: Fluffy pieces with ragged edges, a mushy stool (diarrhea)";

                case 7:

                    return "Type 7: Watery, no solid pieces, entirely liquid (diarrhea";
            }

            return string.Empty;
        }

        public static int ConvertStoolTypeToNumber(string typeDescription)
        {
            return int.Parse(typeDescription.Split(':')[0]
                                            .Split(' ')[1]);
        }

        //[ForeignKey(typeof(StoolType))]
        //public int StoolTypeId { get; set; }

        //[OneToOne]
        //public StoolType StoolType { get; set; }

        public override string ToString()
        {
            if (StoolType.Contains(":"))
            {
                return $"{When} - {StoolType.Split(':')[0]}";    
            }

            return When;
        }
        
    }
}
