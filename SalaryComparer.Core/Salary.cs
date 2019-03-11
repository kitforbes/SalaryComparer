namespace SalaryComparer.Core
{
    public class Salary
    {
        public string Name { get; }
        public double Amount { get; }
        public bool StudentLoan { get; }
        public PensionContribution EmployeeContribution { get; }
        public PensionContribution EmployerContribution { get; }
        public double TotalPensionContribution => EmployeeContribution.Amount + EmployerContribution.Amount;
        public double AdjustedNetIncome => this.Amount - EmployeeContribution.Amount;
        public double TotalIncomeTax => CalculateIncomeTax(this.AdjustedNetIncome);
        public double TotalNationalInsurance => CalculateNationalInsurance(this.AdjustedNetIncome);
        public double TotalStudentLoan => this.StudentLoan ? CalculateStudentLoan(this.AdjustedNetIncome) : 0;
        public double TotalDeductions => this.TotalIncomeTax + this.TotalNationalInsurance + this.TotalStudentLoan;
        public double TakeHome => this.AdjustedNetIncome - this.TotalDeductions;
        public double NetGain => this.TakeHome + this.TotalPensionContribution;

        public Salary(string name, double amount, double pensionPct, double employerPensionPct, bool studentLoan)
        {
            this.Name = name;
            this.Amount = amount;
            this.StudentLoan = studentLoan;
            this.EmployeeContribution = new PensionContribution(pensionPct, amount);
            this.EmployerContribution = new PensionContribution(employerPensionPct, amount);
        }

        private double CalculatePersonalAllowance(double salary)
        {
            var personalAllowance = 12_500;
            var Limit = 100_000;
            if (salary <= Limit)
            {
                return personalAllowance;
            }
            else if (salary > Limit && salary <= (Limit + (personalAllowance * 2)))
            {
                var reducedAllowance = (int)(salary - Limit) / 2;
                return reducedAllowance;
            }
            else
            {
                return 0;
            }
        }

        private double CalculateIncomeTax(double salary)
        {
            var personalAllowance = CalculatePersonalAllowance(salary);
            var output = 0.0;
            // Base
            output += CalculateBand(salary, 0.0, 0, personalAllowance);
            // Starter Rate
            output += CalculateBand(salary, 0.19, personalAllowance, 14_549);
            // Basic Rate
            output += CalculateBand(salary, 0.20, 14_549, 24_944);
            // Intermediate Rate
            output += CalculateBand(salary, 0.21, 24_944, 43_430);
            // Higher Rate
            output += CalculateBand(salary, 0.41, 43_430, 150_000);
            // Additional Rate
            output += CalculateBand(salary, 0.46, 150_000, 999_999_999);

            return output;
        }

        private double CalculateNationalInsurance(double salary)
        {
            var output = 0.0;
            // Base
            output += CalculateBand(salary, 0.0, 0, 8_632);
            //Primary Threshold
            output += CalculateBand(salary, 0.12, 8_632, 50_000);
            // Upper Earnings Limit
            output += CalculateBand(salary, 0.02, 50_000, 999_999_999);

            return output;
        }

        private double CalculateStudentLoan(double salary)
        {
            var output = 0.0;
            // Base
            output += CalculateBand(salary, 0.0, 0, 18_330);
            // Basic Rate
            output += CalculateBand(salary, 0.09, 18_330, 999_999_999);

            return output;
        }

        public double CalculateBand(double salary, double rate, double lower, double upper)
        {
            if (salary >= lower && salary < upper)
            {
                var taxable = salary - lower;
                return taxable * rate;
            }
            else if (salary >= upper)
            {
                var taxable = upper - lower;
                return taxable * rate;
            }
            else
            {
                return 0;
            }
        }
    }
}
