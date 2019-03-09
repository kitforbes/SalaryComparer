using System;

namespace SalaryComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var current = new Salary(double.Parse(args[0]), double.Parse(args[1]), double.Parse(args[2]));
            var proposed = new Salary(double.Parse(args[3]), double.Parse(args[4]), double.Parse(args[5]));

            WriteToConsole($@"
                              |   Current   |  Proposed   | Difference  |
------------------------------|-------------|-------------|-------------|");
            WriteCurrencyLine("Salary                       ", new[] { current.Amount, proposed.Amount, proposed.Amount - current.Amount });
            WriteCurrencyLine("Pension Contribution         ", new[] {
current.EmployeeContribution.Amount, proposed.EmployeeContribution.Amount, proposed.EmployeeContribution.Amount - current.EmployeeContribution.Amount });
            WriteCurrencyLine("Employer Pension Contribution", new[] { current.EmployerContribution.Amount, proposed.EmployerContribution.Amount, proposed.EmployerContribution.Amount - current.EmployerContribution.Amount });
            WriteCurrencyLine("Total Pension Contribution   ", new[] { current.TotalPensionContribution, proposed.TotalPensionContribution, proposed.TotalPensionContribution - current.TotalPensionContribution });
        }

        private static void WriteCurrencyLine(string lineTitle, double[] items, string separator = " | ")
        {
            var output = $"{lineTitle}{separator}";
            foreach (var item in items)
            {
                output += $"{item,11:C}{separator}";
            }

            WriteToConsole(output);
        }

        private static void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
