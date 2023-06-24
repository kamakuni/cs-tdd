namespace App
{

    public interface IExpression
    {
        public IExpression Times(int multiplier);
        public IExpression Plus(IExpression addend);
        public Money Reduce(Bank bank, string to);
    }

    public class Sum : IExpression
    {
        public IExpression Augend { get; }

        public IExpression Addend { get; }

        public Sum(IExpression augend, IExpression addend)
        {
            Augend = augend;
            Addend = addend;
        }

        public IExpression Times(int multiplier)
        {
            return new Sum(this.Augend.Times(multiplier), this.Addend.Times(multiplier));
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }
        public Money Reduce(Bank bank, string to)
        {
            var amount = Augend.Reduce(bank, to).Amount + Addend.Reduce(bank, to).Amount;
            return new Money(amount, to);
        }

    }

}