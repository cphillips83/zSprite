using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;

namespace Atma.Asteroids
{
    public class Program
    {

        public static void Main(params string[] args)
        {
            new ConsoleLogger();

            var logger = Logger.getLogger(typeof(Program));

            logger.info("test");
            Console.Read();
        }
    }
}
