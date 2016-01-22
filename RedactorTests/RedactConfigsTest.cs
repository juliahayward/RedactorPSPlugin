using System;
using System.IO;
using JuliaHayward.Redactor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JuliaHayward.RedactorTests
{
    [TestClass]
    public class RedactConfigsTest
    {
        private const string TestContents = "This is a string that Julia created";
        private const string RedactedContents = "This is a string that REDACTED created";

        [TestInitialize]
        public void CreateTestFiles()
        {
            var testContents = "This is a string that Julia created";
            File.WriteAllText(@"C:\temp\file1.txt", testContents);
            File.WriteAllText(@"C:\temp\file2.config", testContents);
        }

        [TestMethod]
        public void RedactConfigs_ProcessTestFolder_RedactsTheConfigOnly()
        {
            var c = new RedactConfigsCommand();
            c.Name = new[] {@"C:\temp"};
            var output = c.Invoke().GetEnumerator();

            var file1 = File.ReadAllText(@"C:\temp\file1.txt");
            Assert.AreEqual(file1, TestContents);
            var file2 = File.ReadAllText(@"C:\temp\file2.config");
            Assert.AreEqual(file2, RedactedContents);
        }
    }
}
