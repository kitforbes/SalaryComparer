namespace SalaryComparer
{
    public class Salary
    {
        public double Amount { get; }
        public PensionContribution EmployeeContribution { get; }
        public PensionContribution EmployerContribution { get; }
        public double TotalPensionContribution => EmployeeContribution.Amount + EmployerContribution.Amount;
        public double AdjustedNetIncome => this.Amount - EmployeeContribution.Amount;
        public double TotalIncomeTax => CalculateIncomeTax(this.AdjustedNetIncome);
        public double TotalNationalInsurance => CalculateNationalInsurance(this.AdjustedNetIncome);
        public double TotalStudentLoan => CalculateStudentLoan(this.AdjustedNetIncome);
        public double TotalDeductions => this.TotalIncomeTax + this.TotalNationalInsurance + this.TotalStudentLoan;
        public double TakeHome => this.AdjustedNetIncome - this.TotalDeductions;
        public double NetGain => this.TakeHome + this.TotalPensionContribution;

        public Salary(double amount, double pensionPct, double employerPensionPct)
        {
            this.Amount = amount;
            this.EmployeeContribution = new PensionContribution(pensionPct, amount);
            this.EmployerContribution = new PensionContribution(employerPensionPct, amount);
        }

        private double CalculateIncomeTax(double salary)
        {
            var output = 0.0;
            // Base
            output += CalculateBand(salary, 0.0, 0, 12_500);
            // Starter Rate
            output += CalculateBand(salary, 0.19, 12_500, 14_549);
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
