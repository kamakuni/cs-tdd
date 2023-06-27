namespace XUnit
{
    public class Runner
    {
        public string Name { get; }
        public bool WasRun { get; set; }

        public Runner(string name)
        {
            this.Name = name;
            this.WasRun = false;
        }

        public void Run()
        {
            var m = typeof(Runner).GetMethod("Method");
            m.Invoke(this, null);
        }

        public void Method()
        {
            this.WasRun = true;
        }
    }
}
