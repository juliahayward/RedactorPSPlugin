using JuliaHayward.Redactor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuliaHayward.RedactorTests
{
    [TestClass]
    public class TokenDictionaryTests
    {
        [TestMethod]
        public void LoadingAndSaving_Works()
        {
            var dict = new TokenDictionary();
            dict.Add("foo", "bar");
            dict.Save();

            var dict2 = new TokenDictionary();
            dict2.Load();
            Assert.AreEqual(dict2.RedactionTokens["foo"], "bar");

        }
    }
}
