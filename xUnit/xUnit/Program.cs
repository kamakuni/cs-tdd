﻿using XUnit;

var suite = new TestSuite();
suite.Add(new TestCaseTest("TestTemplateMethod"));
suite.Add(new TestCaseTest("TestResult"));
suite.Add(new TestCaseTest("TestFailedResult"));
suite.Add(new TestCaseTest("TestFailedResultFormatting"));
suite.Add(new TestCaseTest("TestTestSuite"));
var result = new TestResult();
suite.Run(result);
Console.WriteLine(result.Summary());