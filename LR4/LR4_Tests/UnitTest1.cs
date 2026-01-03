using NUnit.Framework;
using TextProcessingApp;
using System;
using NUnit.Framework.Legacy;

namespace TextProcessingTests
{
    [TestFixture]
    public class CharCounterTests
    {
        private CharCounter counter;

        [SetUp]
        public void Setup()
        {
            counter = new CharCounter();
        }

        [Test]
        public void CountCharacterOccurrences_NullString_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                counter.CountCharacterOccurrences(null, 'a'));
        }
        
        [Test]
        public void CountCharacterOccurrences_EmptyString_ReturnsZero()
        {
            int result = counter.CountCharacterOccurrences("", 'a');
            ClassicAssert.AreEqual(0, result);
        }


        [Test]
        public void CountCharacterOccurrences_SingleCharacterMatch_ReturnsOne()
        {
            int result = counter.CountCharacterOccurrences("a", 'a');
            ClassicAssert.AreEqual(1, result);
        }

        [Test]
        public void CountCharacterOccurrences_SingleCharacterNoMatch_ReturnsZero()
        {
            int result = counter.CountCharacterOccurrences("b", 'a');
            ClassicAssert.AreEqual(0, result);
        }

        [Test]
        public void CountCharacterOccurrences_MultipleCharactersWithMatches_ReturnsCorrectCount()
        {
            int result = counter.CountCharacterOccurrences("abacax", 'a');
            ClassicAssert.AreEqual(3, result);
        }

        [Test]
        public void CountCharacterOccurrences_MultipleCharactersNoMatches_ReturnsZero()
        {
            int result = counter.CountCharacterOccurrences("bcdef", 'a');
            ClassicAssert.AreEqual(0, result);
        }

    }
}

