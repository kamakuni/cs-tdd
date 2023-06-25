namespace Currency
{

    public class Bank
    {

        public IDictionary<Pair, int> Rates { get; }
        private string from;
        private string to;

        public Bank()
        {
            this.Rates = new Dictionary<Pair, int>();
        }

        public int Rate(string from, string to)
        {
            if (from.Equals(to)) return 1;
            return this.Rates[new Pair(from, to)];
        }

        public Money Reduce(IExpression source, string to)
        {
            return source.Reduce(this, to);
        }

        public void AddRate(string from, string to, int rate)
        {
            this.Rates.Add(new Pair(from, to), rate);
        }
    }

    public class Pair
    {
        public string From { get; }
        public string To { get; }
        public Pair(string from, string to)
        {
            this.From = from;
            this.To = to;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            return this.Equals(obj as Pair);
        }

        public bool Equals(Pair other)
        {
            return this.From.Equals(other.From) && this.To.Equals(other.To);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

}