using System;
using System.Diagnostics;

namespace WebMarket.Common
{
    public static class ConsoleProcess
    {
        static Stopwatch _timerStopwatch = new Stopwatch();
        private static int _i = 0;
        public static void Start(Type type)
        {
            _timerStopwatch = new Stopwatch();
            _timerStopwatch.Start();
            _i = 0;
            Console.WriteLine(type.Name  + @" indexing started - " +  DateTime.Now);
        }

        public static void End(Type type)
        {
            Console.WriteLine(type.Name + @" indexing finished at - " + _timerStopwatch.ElapsedMilliseconds);
            _timerStopwatch.Stop();
        }

        public static void Increment()
        {
            _i++;
        }

        public static void Restart()
        {
            _timerStopwatch.Restart();
        }

        public static void ModOf5000()
        {
            if (_i%5000 != 0) return;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(@"iteration for {0} took - {1} ms", _i, _timerStopwatch.ElapsedMilliseconds);
        }

        public static void ModOf3000()
        {
            if (_i % 3000 != 0) return;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(@"iteration for {0} took - {1} ms", _i, _timerStopwatch.ElapsedMilliseconds);
        }

        public static void ModOf500()
        {
            if (_i % 500 != 0) return;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(@"iteration for {0} took - {1} ms", _i, _timerStopwatch.ElapsedMilliseconds);
        }

        public static long ElapsedMilliseconds()
        {
           return _timerStopwatch.ElapsedMilliseconds;
        }

    }
}
