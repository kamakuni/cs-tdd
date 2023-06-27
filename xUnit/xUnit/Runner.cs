namespace XUnit
{

    public class TestCase
    {
        public string Name { get; }
        public TestCase(string name)
        {
            this.Name = name;
        }

        public void Run()
        {
            //Console.WriteLine(this.GetType());
            var m = this.GetType().GetMethod(this.Name);
            m.Invoke(this, null);
        }

    }
    public class Runner : TestCase
    {
        public bool WasRun { get; set; }

        public Runner(string name) : base(name)
        {
            this.WasRun = false;
        }

        public void Method()
        {
            this.WasRun = true;
        }
    }
}
