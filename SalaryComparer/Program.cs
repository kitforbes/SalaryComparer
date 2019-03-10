using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace SalaryComparer
{
    class Program
    {
        private static IConfigurationRoot _configuration;

        static void Main(string[] args)
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var salariesConfiguration = _configuration.GetSection("Salaries").GetChildren();
            var salaries = new List<Salary>();
            foreach (var salary in salariesConfiguration)
            {
                var name = salary.GetSection("Name").Value.ToString();
                var amount = double.Parse(salary.GetSection("Amount").Value);
                var employeePensionContribution = double.Parse(salary.GetSection("PensionContributionPercentage:Employee").Value);
                var employerPensionContribution = double.Parse(salary.GetSection("PensionContributionPercentage:Employer").Value);
                var studentLoan = bool.Parse(salary.GetSection("StudentLoan").Value);

                salaries.Add(new Salary(name, amount, employeePensionContribution, employerPensionContribution, studentLoan));
            }

            PrintSummary(salaries);
        }

        private static void PrintSummary(IList<Salary> salaries)
        {
            var separator = "|";
            var showDiff = salaries.Count == 2;

            var heading = $"                               {separator}";
            foreach (var salary in salaries)
            {
                heading += $" {salary.Name,-11} {separator}";
            }

            WriteToConsole(heading);
            WriteDividingLine(salaries.Count, separator);
            WriteCurrencyLine("Salary", salaries.Select(x => x.Amount).ToArray(), separator);
            WriteCurrencyLine("Pension Contribution", salaries.Select(x => x.EmployeeContribution.Amount).ToArray(), separator);
            WriteCurrencyLine("Employer Pension Contribution", salaries.Select(x => x.EmployerContribution.Amount).ToArray(), separator);
            WriteCurrencyLine("Income Tax", salaries.Select(x => x.TotalIncomeTax).ToArray(), separator);
            WriteCurrencyLine("National Insurance", salaries.Select(x => x.TotalNationalInsurance).ToArray(), separator);
            WriteCurrencyLine("Student Loan", salaries.Select(x => x.TotalStudentLoan).ToArray(), separator);
            WriteDividingLine(salaries.Count, separator);
            WriteCurrencyLine("Total Deductions", salaries.Select(x => x.TotalDeductions).ToArray(), separator);
            WriteCurrencyLine("Total Pension Contribution", salaries.Select(x => x.TotalPensionContribution).ToArray(), separator);
            WriteCurrencyLine("Take Home", salaries.Select(x => x.TakeHome).ToArray(), separator);
            WriteDividingLine(salaries.Count, separator);
            WriteCurrencyLine("Net Gain", salaries.Select(x => x.NetGain).ToArray(), separator);
        }

        private static void WriteCurrencyLine(string lineTitle, double[] items, string separator = "|")
        {
            var output = $"{lineTitle,-30} {separator}";
            foreach (var item in items)
            {
                output += $" {item,11:C} {separator}";
            }

            WriteToConsole(output);
        }

        private static void WriteDividingLine(int columns, string separator = "|")
        {
            var output = $"-------------------------------{separator}";
            for (int i = 0; i < columns; i++)
            {
                output += $"-------------{separator}";
            }

            WriteToConsole(output);
        }

        private static void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
