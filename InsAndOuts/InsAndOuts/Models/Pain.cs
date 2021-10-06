using System;
using System.Collections.Generic;
using System.Text;
using InsAndOuts.Utilities;
using SQLite;

namespace InsAndOuts.Models
{
    [Table("Pains")]
    public class Pain : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id    { get; set; }
        public int Level { get; set; }

        public Pain()
        {
            Level = 0;
        }

        public override string ToString()
        {
            return $"{When} - Level: {Level}";
        }
        
        public DateTime WhenToDateTime()
        {
            return DateTime.Parse(When);
        }

        public TimeSpan WhenToTimeSpan()
        {
            if (When.IsNullEmptyOrWhitespace())
            {
                return new TimeSpan();
            }

            var time = When.Split(' ')[1];
            return TimeSpan.Parse(time);
        }
    }
}
