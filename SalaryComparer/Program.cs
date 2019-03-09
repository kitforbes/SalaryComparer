using System;

namespace SalaryComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var current = new Salary(double.Parse(args[0]), double.Parse(args[1]), double.Parse(args[2]));
            var proposed = new Salary(double.Parse(args[3]), double.Parse(args[4]), double.Parse(args[5]));

            var output = $@"
                              |   Current   |  Proposed   | Difference  |
------------------------------|-------------|-------------|-------------|
Salary                        | {current.Amount,11:C} | {proposed.Amount,11:C} | {proposed.Amount - current.Amount,11:C} |
Pension Contribution          | {current.EmployeeContribution.Amount,11:C} | {proposed.EmployeeContribution.Amount,11:C} | {proposed.EmployeeContribution.Amount - current.EmployeeContribution.Amount,11:C} |
Employer Pension Contribution | {current.EmployerContribution.Amount,11:C} | {proposed.EmployerContribution.Amount,11:C} | {proposed.EmployerContribution.Amount - current.EmployerContribution.Amount,11:C} |
Total Pension Contribution    | {current.TotalPensionContribution,11:C} | {proposed.TotalPensionContribution,11:C} | {proposed.TotalPensionContribution - current.TotalPensionContribution,11:C} |
";

            Console.WriteLine(output);
        }
    }
}
