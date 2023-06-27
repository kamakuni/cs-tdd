// See https://aka.ms/new-console-template for more information
var test = new XUnit.Runner("Method");
Console.WriteLine(test.WasRun);
test.Run();
Console.WriteLine(test.WasRun);
//var test = Runner("TestMethod");
//Console.WriteLine(test.WasRun);
//test.TestMethod();
//Console.WriteLine(test.WasRun);

