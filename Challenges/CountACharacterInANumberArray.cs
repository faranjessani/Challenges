using NUnit.Framework;

namespace Challenges
{
    [TestFixture]
    public class CountACharacterInANumberArray
    {
        private int CountInstancesOfNumber(int[] input, int number)
        {
            var count = 0;
            foreach (var i in input)
            {
                var temp = i;
                while (temp > 0)
                {
                    if (temp % 10 == number) count++;
                    temp /= 10;
                }
            }

            return count;
        }

        [Test]
        [TestCase(2, 0, 1, 10, 101)]
        [TestCase(4, 1, 1, 10, 101)]
        [TestCase(5, 9, 99991, 10, 1019)]
        public void CountACharacterInANumberArrayTest(int expected, int numberToFind, params int[] values)
        {
            var result = CountInstancesOfNumber(values, numberToFind);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}