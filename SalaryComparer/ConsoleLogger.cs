using System;

namespace SalaryComparer
{
    public class ConsoleLogger : ILogger
    {
        public void Information(string message)
        {
            Console.WriteLine(message);
        }
    }
}
