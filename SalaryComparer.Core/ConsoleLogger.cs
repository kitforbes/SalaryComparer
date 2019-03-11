using System;

namespace SalaryComparer.Core
{
    public class ConsoleLogger : ILogger
    {
        public void Information(string message)
        {
            Console.WriteLine(message);
        }
    }
}
