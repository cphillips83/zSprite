using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Asteroids.Core;
using Atma.Asteroids.Engine;
using Atma.Asteroids.States;
using Atma.Asteroids.Engine.Subsystems;
using Atma.Asteroids.Engine.Subsystems.OpenTK;

namespace Atma.Asteroids
{
    public class Program
    {
        private static readonly Logger logger = Logger.getLogger(typeof(Program));

        public static void Main(params string[] args)
        {
            new ConsoleLogger();

            var ge = new GameEngine(new ISubsystem[] { new StopwatchTime(), new OpenTKGraphicsSubsystem() });
            ge.run(new DummyState());

            //var time = new StopwatchTime();
            //time.updateTimeFromServer(5);

            //var r = new Random();
            //for (var i = 0; i < 1000; i++)
            //{
            //    //time.step();
            //    foreach (var tick in time.tick())
            //    {
            //        logger.info(time.ToString());
            //    }


            //    System.Threading.Thread.Sleep(33);
            //}

            //logger.info("test");
        }
    }
}
