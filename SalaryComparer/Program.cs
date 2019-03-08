using System;

namespace SalaryComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentSalary = double.Parse(args[0]);
            var currentPensionContributionRate = double.Parse(args[1]);
            var currentPensionContribution = currentSalary * currentPensionContributionRate;
            var currentEmployerPensionContributionRate = double.Parse(args[2]);
            var currentEmployerPensionContribution = currentSalary * currentEmployerPensionContributionRate;
            var currentTotalPensionContribution = currentPensionContribution + currentEmployerPensionContribution;

            var newSalary = double.Parse(args[3]);
            var newPensionContributionRate = double.Parse(args[4]);
            var newPensionContribution = newSalary * newPensionContributionRate;
            var newEmployerPensionContributionRate = double.Parse(args[5]);
            var newEmployerPensionContribution = newSalary * newEmployerPensionContributionRate;
            var newTotalPensionContribution = newPensionContribution + newEmployerPensionContribution;

            var output = $@"
                              |   Current   |  Proposed   | Difference  |
------------------------------|-------------|-------------|-------------|
Salary                        | {currentSalary,11:C} | {newSalary,11:C} | {newSalary - currentSalary,11:C} |
Pension Contribution          | {currentPensionContribution,11:C} | {newPensionContribution,11:C} | {newPensionContribution - currentPensionContribution,11:C} |
Employer Pension Contribution | {currentEmployerPensionContribution,11:C} | {newEmployerPensionContribution,11:C} | {newEmployerPensionContribution - currentEmployerPensionContribution,11:C} |
Total Pension Contribution    | {currentTotalPensionContribution,11:C} | {newTotalPensionContribution,11:C} | {newTotalPensionContribution - currentTotalPensionContribution,11:C} |
";

            Console.WriteLine(output);
        }
    }
}
