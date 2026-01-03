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
        public void CountCharacterOccurrences_MultipleMatches_ReturnsExactCount()
        {
            // Given
            var counter = new CharCounter();
            string text = "aabbccaa"; // 4 раза 'a'
            char target = 'a';
            int expected = 4;

            // When
            int actual = counter.CountCharacterOccurrences(text, target);

            // Then
            ClassicAssert.AreEqual(expected, actual);
        }

        [Test]
        public void CountCharacterOccurrences_ProcessesEntireString()
        {
            var counter = new CharCounter();
            // Строка с символом только в конце — если index не увеличивается, он не дойдёт до конца
            int result = counter.CountCharacterOccurrences("bbbbba", 'a');
            ClassicAssert.AreEqual(1, result);
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

        [Test]
        public void CountCharacterOccurrences_ChangeCountByOne_Fails()
        {
            // Этот тест убьёт мутанта: count++ на count--
            var result = counter.CountCharacterOccurrences("aaa", 'a');
            // Если мутант: count--, то result = -3  != 3 
            ClassicAssert.AreEqual(3, result);
        }

        [Test]
        public void CountCharacterOccurrences_EmptyString_ReturnsZero_Exact()
        {
            // Убивает мутант: return count на return count + 1
            ClassicAssert.AreEqual(0, counter.CountCharacterOccurrences("", 'a'));
        }

    }
}

