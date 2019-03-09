namespace SalaryComparer
{
    public class Salary
    {
        public double Amount { get; }
        public PensionContribution EmployeeContribution { get; }
        public PensionContribution EmployerContribution { get; }
        public double TotalPensionContribution => EmployeeContribution.Amount + EmployerContribution.Amount;

        public Salary(double amount, double pensionPct, double employerPensionPct)
        {
            this.Amount = amount;
            this.EmployeeContribution = new PensionContribution(pensionPct, amount);
            this.EmployerContribution = new PensionContribution(employerPensionPct, amount);
        }
    }
}
