using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Core
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger()
            : base()
        {

        }

        protected override void log(string type, string module, string message)
        {
            var dt = DateTime.UtcNow;
            Console.WriteLine("({0}) {1, 25} : {2} -> {3}", dt.ToString("MM/dd HH:MM:ss"), type, module, message);
        }
    }
}
