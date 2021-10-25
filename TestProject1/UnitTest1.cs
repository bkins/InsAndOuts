using InsAndOuts.Data;
using System;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        private static Database _database;
        private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                                                   , "IsAndOs_Tests.db3");
        
        public static Database Database => _database = _database ?? new Database(Path);

        [Fact]
        public void Test1()
        {

        }
    }
}
