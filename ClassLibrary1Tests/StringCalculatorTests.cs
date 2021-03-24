using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ClassLibrary1.Tests
{
    [TestClass()]
    public class StringCalculatorTests
    {
        [TestMethod()]
        public void EmptyStringAddTest()
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add("");
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        [DataRow("1", 1)]
        [DataRow("2", 2)]
        [DataRow("30", 30)]
        public void SingleNumberAddTest(string number, int expected)
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add(number);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow("1,1", 2)]
        [DataRow("2,4", 6)]
        [DataRow("30,10", 40)]
        [DataRow("30,10,2", 42)]
        public void NumbersCommaDelimiterAddTest(string numbers, int expected)
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add(numbers);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow("1\n1", 2)]
        [DataRow("2\n4", 6)]
        [DataRow("30\n10", 40)]
        [DataRow("30\n10\n2", 42)]
        public void NumbersNewlineDelimiterAddTest(string numbers, int expected)
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add(numbers);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow("1\n1", 2)]
        [DataRow("2,4", 6)]
        [DataRow("30\n10", 40)]
        [DataRow("30\n10,2", 42)]
        [DataRow("30\n10,2,15\n20", 77)]
        public void NumbersTwoDelimitersAddTest(string numbers, int expected)
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add(numbers);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow("1\n-1")]
        [DataRow("-2,-4")]
        [DataRow("30\n-10")]
        [DataRow("-30\n10,2")]
        [DataRow("30\n-10,2,-15\n20")]
        public void NegativeNumbersAddTest(string numbers)
        {
            StringCalculator sc = new StringCalculator();
            Assert.ThrowsException<ArgumentException>(() => sc.Add(numbers));
        }

        [TestMethod()]
        [DataRow("1\n1", 2)]
        [DataRow("2,4000", 2)]
        [DataRow("30\n10", 40)]
        [DataRow("30\n10,200000", 40)]
        [DataRow("30\n10,2000,1000\n20", 1060)]
        public void NumbersGreaterThan1000IgnoredAddTest(string numbers, int expected)
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add(numbers);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow("1\n1", 2)]
        [DataRow("//#2#4", 6)]
        [DataRow("30,10", 40)]
        [DataRow("//?30?10?2", 42)]
        [DataRow("///30/10/2/15/20", 77)]
        public void NumbersSingleCharDelimiterAddTest(string numbers, int expected)
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add(numbers);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow("1\n1", 2)]
        [DataRow("//[#]2#4", 6)]
        [DataRow("30,10", 40)]
        [DataRow("//[#$?]30#$?10#$?2", 42)]
        [DataRow("//[//]30//10//2//15//20", 77)]
        public void NumbersMultiCharDelimiterAddTest(string numbers, int expected)
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add(numbers);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow("1\n1", 2)]
        [DataRow("//[#]2#4", 6)]
        [DataRow("30,10", 40)]
        [DataRow("//[#$?][X]30#$?10X2", 42)]
        [DataRow("//[//][{}]30//10{}2//15{}20", 77)]
        public void NumbersManyMultiCharDelimitersAddTest(string numbers, int expected)
        {
            StringCalculator sc = new StringCalculator();
            int result = sc.Add(numbers);
            Assert.AreEqual(expected, result);
        }
    }
}