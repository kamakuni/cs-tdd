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
        public bool WasSetUp { get; set; }

        public Runner(string name) : base(name)
        {
            this.WasRun = false;
        }

        public void SetUp()
        {
            this.WasSetUp = true;
        }

        public void Method()
        {
            this.WasRun = true;
        }
    }

    class TestCaseTest : TestCase
    {
        public TestCaseTest(string name) : base(name)
        {

        }

        private void Assert(bool expected, bool actual)
        {
            if (expected != actual)
            {
                Console.WriteLine("Failed");
                throw new Exception();
            }
            else
            {
                Console.WriteLine("Passed");
            }
        }

        public void TestRunning()
        {
            var test = new XUnit.Runner("Method");
            this.Assert(false, test.WasRun);
            test.Run();
            this.Assert(true, test.WasRun);
        }

        public void TestSetUp()
        {
            var test = new XUnit.Runner("Method");
            test.Run();
            this.Assert(true, test.WasSetUp);
        }
    }
}
