using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SalaryComparer.Core;

namespace SalaryComparer
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static ILogger _logger;

        static void Main(string[] args)
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            _logger = new ConsoleLogger();

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

            PrintSummary(salaries, _logger);
        }

        private static void PrintSummary(IList<Salary> salaries, ILogger logger)
        {
            var separator = "|";
            var heading = $"                               {separator}";
            foreach (var salary in salaries)
            {
                heading += $" {salary.Name.Truncate(11),-11} {separator}";
            }

            _logger.Information(heading);
            WriteDividingLine(salaries.Count, logger, separator);

            WriteCurrencyLine("Salary", GetDifference(salaries.Select(x => x.Amount).ToList()), logger, separator);
            WriteCurrencyLine("Pension Contribution", GetDifference(salaries.Select(x => x.EmployeeContribution.Amount).ToList()), logger, separator);
            WriteCurrencyLine("Employer Pension Contribution", GetDifference(salaries.Select(x => x.EmployerContribution.Amount).ToList()), logger, separator);
            WriteCurrencyLine("Income Tax", GetDifference(salaries.Select(x => x.TotalIncomeTax).ToList()), logger, separator);
            WriteCurrencyLine("National Insurance", GetDifference(salaries.Select(x => x.TotalNationalInsurance).ToList()), logger, separator);
            WriteCurrencyLine("Student Loan", GetDifference(salaries.Select(x => x.TotalStudentLoan).ToList()), logger, separator);

            WriteDividingLine(salaries.Count, logger, separator);

            WriteCurrencyLine("Total Deductions", GetDifference(salaries.Select(x => x.TotalDeductions).ToList()), logger, separator);
            WriteCurrencyLine("Total Pension Contribution", GetDifference(salaries.Select(x => x.TotalPensionContribution).ToList()), logger, separator);
            WriteCurrencyLine("Take Home", GetDifference(salaries.Select(x => x.TakeHome).ToList()), logger, separator);

            WriteDividingLine(salaries.Count, logger, separator);

            WriteCurrencyLine("Net Gain", GetDifference(salaries.Select(x => x.NetGain).ToList()), logger, separator);
        }

        private static IList<double> GetDifference(IList<double> values)
        {
            if (values.Count == 0)
            {
                return values;
            }

            var results = new List<double>
            {
                Math.Round(values[0]),
            };

            for (int i = 1; i < values.Count; i++)
            {
                results.Add(Math.Round(values[i] - values[0]));
            }

            return results;
        }

        private static void WriteCurrencyLine(string lineTitle, IList<double> items, ILogger logger, string separator = "|")
        {
            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            var output = $"{lineTitle.Truncate(30),-30} {separator}";
            for (int i = 0; i < items.Count; i++)
            {
                if (i == 0)
                {
                    FormattableString data = $" {items[i],11:C0} {separator}";
                    output += data.ToString(culture);
                }
                else
                {
                    var symbol = items[i] > 0 ? "+" : " ";
                    var value = $"{symbol}{items[i]:C0}".ToString(culture);
                    output += $" {value,11} {separator}";
                }
            }

            logger.Information(output);
        }

        private static void WriteDividingLine(int columns, ILogger logger, string separator = "|")
        {
            var output = $"-------------------------------{separator}";
            for (int i = 0; i < columns; i++)
            {
                output += $"-------------{separator}";
            }

            logger.Information(output);
        }
    }
}
