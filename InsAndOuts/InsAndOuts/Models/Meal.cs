using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InsAndOuts.Models
{
    [Table("Meals")]
    public class Meal : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
