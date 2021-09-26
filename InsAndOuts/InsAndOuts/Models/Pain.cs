using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InsAndOuts.Models
{
    [Table("Pains")]
    public class Pain : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id    { get; set; }
        public int Level { get; set; }


    }
}
