namespace SalaryComparer.Core
{
    public class PensionContribution
    {
        public double Percentage { get; }
        public double Amount { get; }

        public PensionContribution(double percentage, double salary)
        {
            this.Percentage = percentage;
            this.Amount = CalculateContribution(percentage, salary);
        }

        private double CalculateContribution(double percentage, double salary)
        {
            return salary * percentage;
        }
    }
}
