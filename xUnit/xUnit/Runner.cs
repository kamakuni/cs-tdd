namespace XUnit
{

    public class TestResult
    {
        private int RunCount { set; get; } = 0;
        private int ErrorCount { set; get; } = 0;
        public void TestStarted()
        {
            RunCount += 1;
        }
        public void TestFailed()
        {
            ErrorCount += 1;
        }

        public string Summary()
        {
            return $"{RunCount} run, {ErrorCount} failed";
        }
    }

    public class TestCase
    {
        public string Log { get; set; } = "";
        public string Name { get; }

        protected TestResult Result { get; set; } = null!;

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
        public void Run(TestResult result)
        {
            try
            {
                this.SetUp();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.TestFailed();
                return;
            }
            result.TestStarted();
            try
            {
                var m = this.GetType().GetMethod(this.Name);
                m.Invoke(this, null);
            }
            catch (Exception e)
            {
                result.TestFailed();
            }
            this.TearDown();
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

        public void TestBrokenMethod()
        {
            throw new Exception();
        }
        public override void TearDown()
        {
            this.Log += "TearDown ";
        }
    }

    class TestSuite
    {

        private List<TestCase> Tests { get; set; } = new List<TestCase>();
        public void Add(TestCase test)
        {
            Tests.Add(test);
        }

        public void Run(TestResult result)
        {
            Tests.ForEach(t => t.Run(result));
        }
    }

    class TestCaseTest : TestCase
    {
        public TestCaseTest(string name) : base(name)
        {

        }
        public override void SetUp()
        {
            this.Result = new TestResult();
        }

        public void TestTemplateMethod()
        {
            // This is a Fixture
            var test = new WasRun("TestMethod");
            test.Run(this.Result);
            this.AssertEqual("SetUp TestMethod TearDown ", test.Log);
        }

        public void TestTearDownWhenFailed()
        {
            var test = new WasRun("testBrokenMethod");
            test.Run(this.Result);
            this.AssertEqual("SetUp TearDown ", test.Log);
        }

        public void TestResult()
        {
            var test = new WasRun("TestMethod");
            test.Run(this.Result);
            this.AssertEqual("1 run, 0 failed", this.Result.Summary());
        }

        public void TestFailedResult()
        {
            var test = new WasRun("TestMethod");
            test.Run(this.Result);
            this.AssertEqual("1 run, 1 failed", this.Result.Summary());
        }

        public void TestFailedResultFormatting()
        {
            this.Result.TestStarted();
            this.Result.TestFailed();
            this.AssertEqual("1 run, 1 failed", this.Result.Summary());
        }

        public void TestTestSuite()
        {
            var suite = new TestSuite();
            suite.Add(new WasRun("testMethod"));
            suite.Add(new WasRun("testBrokenMethod"));
            suite.Run(this.Result);
            this.AssertEqual("2 run, 1 failed", this.Result.Summary());
        }

    }

}
