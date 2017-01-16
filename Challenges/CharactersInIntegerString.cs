using System;
using NUnit.Framework;

namespace Challenges
{
    [TestFixture]
    public class CharactersInIntegerString
    {
        /// <summary>
        /// For a given string s, comprised of positive integers
        /// Find how many ways you can split it up so each substring is a number less than 26
        /// ie. "123" -> 1,2,3 or 12,3 or 1,23 therefore 3
        /// ie. "1234" -> 1,2,3,4 or 12,3,4 or 1,23,4 therefore 3
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private int Calculate(string number)
        {
            if (number.Length == 0)
                return 1;

            var count = Calculate(number.Substring(1));

            if (number.Length > 1 && Int32.Parse(number.Substring(0, 2)) <= 26)
                count += Calculate(number.Substring(2));

            return count;
        }

        [Test]
        [TestCase("121", 3)]
        [TestCase("1234", 3)]
        public void Calculate_MoreThanTwoDigits_ReturnsExpected(string input, int expected)
        {
            var result = Calculate(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("345")]
        [TestCase("888")]
        [TestCase("999999")]
        public void Calculate_MoreThanTwoDigitsAllGreaterThan2_ReturnsOne(string input)
        {
            var result = Calculate(input);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        [TestCase("0")]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        public void Calculate_SingleDigit_ReturnsOne(string input)
        {
            var result = Calculate(input);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        [TestCase("27")]
        [TestCase("99")]
        public void Calculte_TwoDigitsGreaterThan26_ReturnsOne(string input)
        {
            var result = Calculate(input);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        [TestCase("10")]
        [TestCase("26")]
        public void Calculte_TwoDigitsLessThan27_ReturnsTwo(string input)
        {
            var result = Calculate(input);

            Assert.That(result, Is.EqualTo(2));
        }
    }
}