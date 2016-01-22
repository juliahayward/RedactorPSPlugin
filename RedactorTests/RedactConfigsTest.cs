using System;
using JuliaHayward.Redactor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JuliaHayward.RedactorTests
{
    [TestClass]
    public class RedactConfigsTest
    {
        [TestMethod]
        public void RedactConfigs_ProcessTestFolder()
        {
            var c = new RedactConfigsCommand();
            c.Name = new[] {@"C:\temp"};
            var output = c.Invoke().GetEnumerator();
        }
    }
}
