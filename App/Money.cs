namespace App
{

    public class Money : IExpression
    {

        public static Money Doller(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public int Amount { get; }
        public string Currency { get; }

        public Money(int amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public IExpression Times(int multiplier)
        {
            return new Money(this.Amount * multiplier, Currency);
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public Money Reduce(Bank bank, string to)
        {
            var rate = bank.Rate(this.Currency, to);
            return new Money(this.Amount / rate, to);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            return Equals(obj as Money);
        }
        public bool Equals(Money other)
        {
            return Amount == other.Amount
            && Currency == other.Currency;
        }
    }

}