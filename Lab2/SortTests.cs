using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    [TestFixture]
    class SortTests
    {
        [TestCase(20, 0, 20, 20)]
        [TestCase(100, 0, 20, 100)]
        [TestCase(10000, 0, 20, 10000)]
        public void StrGeneratorLengthTests(int numOfWords, int minLength, int maxLength, int resLength)
        {
            var generator = new Lab2Main();
            var res = generator.generateStrings(numOfWords, minLength, maxLength);
            Assert.AreEqual(res.Count, resLength);
        }

        [TestCase(20, 0, 20, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        public void StrGeneratorCharactersTest(int numOfWords, int minLength, int maxLength, string chars)
        {
            var generator = new Lab2Main();
            var res = generator.generateStrings(numOfWords, minLength, maxLength);
            var flag = true;
            foreach (var word in res)
            {
                flag = word.All(c => chars.Contains(c));
                if (!flag)
                {
                    break;
                }
            }
            Assert.AreEqual(flag, true);
        }
    }
}
