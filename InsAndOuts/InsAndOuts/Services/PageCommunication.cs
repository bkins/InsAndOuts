using System;
using System.Collections.Generic;
using System.Text;

namespace InsAndOuts.Services
{
    public sealed class PageCommunication
    {
        public static PageCommunication Instance { get; } = new PageCommunication();

        public int    IntegerValue { get; set; }
        public string StringValue  { get; set; }

        static PageCommunication()
        {
        }

        private PageCommunication()
        {
        }

    }
}
