namespace XUnit
{

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
        public void Run()
        {
            this.SetUp();
            var m = this.GetType().GetMethod(this.Name);
            m.Invoke(this, null);
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
    }
}
