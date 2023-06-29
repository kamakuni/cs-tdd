namespace XUnit
{

    public abstract class TestCase
    {
        public string Name { get; }
        public bool WasRun { get; set; }
        public bool WasSetUp { get; set; }

        public TestCase(string name)
        {
            this.Name = name;
        }

        public abstract void SetUp();
        protected void Assert(bool expected, bool actual)
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

        public void Run()
        {
            this.SetUp();
            var m = this.GetType().GetMethod(this.Name);
            m.Invoke(this, null);
        }

    }
    public class WasRunClass : TestCase
    {

        public WasRunClass(string name) : base(name)
        {
        }
        public override void SetUp()
        {
            this.WasRun = false;
            this.WasSetUp = true;
        }

        public void TestMethod()
        {
            this.WasRun = true;
        }
    }

    class TestCaseTest : TestCase
    {
        public TestCase Test { get; set; }
        public TestCaseTest(string name) : base(name)
        {

        }

        public override void SetUp()
        {
            // This is a Fixture
            this.Test = new WasRunClass("TestMethod");
        }

        public void TestRunning()
        {
            this.Test.Run();
            this.Assert(true, this.Test.WasRun);
        }

        public void TestSetUp()
        {
            this.Test.Run();
            this.Assert(true, this.Test.WasSetUp);
        }
    }
}
