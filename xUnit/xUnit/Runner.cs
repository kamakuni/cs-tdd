namespace XUnit
{

    public class TestResult
    {
        private int RunCount { set; get; } = 0;
        public void TestStarted()
        {
            RunCount += 1;
        }
        public string Summary()
        {
            return $"{RunCount} run, 0 failed";
        }
    }

    public class TestCase
    {
        public string Log { get; set; }
        public string Name { get; }

        public TestCase(string name)
        {
            this.Name = name;
        }
        public virtual void SetUp() { }
        public virtual void TearDown() { }
        protected void AssertEqual(bool expected, bool actual)
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
        protected void AssertEqual(string expected, string actual)
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
        public TestResult Run()
        {
            this.SetUp();
            var result = new TestResult();
            result.TestStarted();
            var m = this.GetType().GetMethod(this.Name);
            m.Invoke(this, null);
            this.TearDown();
            return result;
        }

    }
    public class WasRun : TestCase
    {

        public WasRun(string name) : base(name)
        {
        }
        public override void SetUp()
        {
            this.Log = "SetUp ";
        }

        public void TestMethod()
        {
            this.Log += "TestMethod ";
        }

        public override void TearDown()
        {
            this.Log += "TearDown ";
        }
    }

    class TestCaseTest : TestCase
    {
        public TestCaseTest(string name) : base(name)
        {

        }
        public void TestTemplateMethod()
        {
            // This is a Fixture
            var test = new WasRun("TestMethod");
            test.Run();
            this.AssertEqual("SetUp TestMethod TearDown ", test.Log);
        }
        public void TestResult()
        {
            var test = new WasRun("TestMethod");
            var result = test.Run();
            this.AssertEqual("1 run, 0 failed", result.Summary());
        }
    }
}
