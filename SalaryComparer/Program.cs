using System;

namespace SalaryComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var current = new Salary(double.Parse(args[0]), double.Parse(args[1]), double.Parse(args[2]), true);
            var proposed = new Salary(double.Parse(args[3]), double.Parse(args[4]), double.Parse(args[5]), true);

            WriteToConsole($@"
                              |   Current   |  Proposed   | Difference  |
------------------------------|-------------|-------------|-------------|");
            WriteCurrencyLine("Salary                       ", new[] { current.Amount, proposed.Amount, proposed.Amount - current.Amount });
            WriteCurrencyLine("Pension Contribution         ", new[] {
current.EmployeeContribution.Amount, proposed.EmployeeContribution.Amount, proposed.EmployeeContribution.Amount - current.EmployeeContribution.Amount });
            WriteCurrencyLine("Employer Pension Contribution", new[] { current.EmployerContribution.Amount, proposed.EmployerContribution.Amount, proposed.EmployerContribution.Amount - current.EmployerContribution.Amount });
            WriteCurrencyLine("Income Tax                   ", new[] { current.TotalIncomeTax, proposed.TotalIncomeTax, proposed.TotalIncomeTax - current.TotalIncomeTax });
            WriteCurrencyLine("National Insurance           ", new[] { current.TotalNationalInsurance, proposed.TotalNationalInsurance, proposed.TotalNationalInsurance - current.TotalNationalInsurance });
            if (current.StudentLoan || proposed.StudentLoan)
            {
                WriteCurrencyLine("Student Loan                 ", new[] { current.TotalStudentLoan, proposed.TotalStudentLoan, proposed.TotalStudentLoan - current.TotalStudentLoan });
            }

            WriteToConsole("------------------------------|-------------|-------------|-------------|");
            WriteCurrencyLine("Total Deductions             ", new[] { current.TotalDeductions, proposed.TotalDeductions, proposed.TotalDeductions - current.TotalDeductions });
            WriteCurrencyLine("Total Pension Contribution   ", new[] { current.TotalPensionContribution, proposed.TotalPensionContribution, proposed.TotalPensionContribution - current.TotalPensionContribution });
            WriteCurrencyLine("Take Home                    ", new[] { current.TakeHome, proposed.TakeHome, proposed.TakeHome - current.TakeHome });
            WriteToConsole("------------------------------|-------------|-------------|-------------|");
            WriteCurrencyLine("Net Gain                     ", new[] { current.NetGain, proposed.NetGain, proposed.NetGain - current.NetGain });
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
