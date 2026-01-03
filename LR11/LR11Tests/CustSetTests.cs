using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Linq;

namespace CustomSetLibrary.Tests
{
    [TestFixture]
    public class CustomSetTests
    {
        private CustomSet<int> set;

        [SetUp]
        public void Setup()
        {
            set = new CustomSet<int>();
        }

        [Test]
        public void Set_IsInitiallyEmpty()
        {
            ClassicAssert.IsTrue(set.IsEmpty);
            ClassicAssert.AreEqual(0, set.Count);
        }

        [Test]
        public void Add_Element_AddsSuccessfully()
        {
            var result = set.Add(1);
            ClassicAssert.IsTrue(result);
            ClassicAssert.IsFalse(set.IsEmpty);
            ClassicAssert.AreEqual(1, set.Count);
        }

        [Test]
        public void Add_Duplicate_ReturnsFalse()
        {
            var added = set.Add(1);
            var result = set.Add(1);
            Assert.That(added, Is.True);
            ClassicAssert.IsFalse(result);
            Assert.That(result, Is.False);
            ClassicAssert.AreEqual(1, set.Count);
        }

        [Test]
        public void Remove_ExistingElement_RemovesSuccessfully()
        {
            set.Add(1);
            var result = set.Remove(1);
            ClassicAssert.IsTrue(result);
            ClassicAssert.IsTrue(set.IsEmpty);
            ClassicAssert.AreEqual(0, set.Count);
        }

        [Test]
        public void Remove_NonExistingElement_ReturnsFalse()
        {
            var result = set.Remove(1);
            ClassicAssert.IsFalse(result);
            ClassicAssert.IsTrue(set.IsEmpty);
            ClassicAssert.AreEqual(0, set.Count);
        }

        [Test]
        public void Contains_Element_ReturnsCorrectly()
        {
            set.Add(1);
            ClassicAssert.IsTrue(set.Contains(1));
            ClassicAssert.IsFalse(set.Contains(2));
        }

        [Test]
        public void Clear_SetsToEmpty()
        {
            set.Add(1);
            set.Add(2);
            set.Clear();
            ClassicAssert.IsTrue(set.IsEmpty);
            ClassicAssert.AreEqual(0, set.Count);
        }

        [Test]
        public void GetEnumerator_EnumeratesCorrectly()
        {
            set.Add(1);
            set.Add(2);
            set.Add(1); // дубликат

            var list = set.ToList();
            Assert.That(list, Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test]
        public void Add_Duplicate_ShouldNotIncreaseCount()
        {
            // Убивает мутант: return false на return true
            bool added1 = set.Add(1);
            bool added2 = set.Add(1);
            ClassicAssert.IsTrue(added1);
            ClassicAssert.IsFalse(added2);
            ClassicAssert.AreEqual(1, set.Count); // явная проверка
        }

        [Test]
        public void Remove_ExistingElement_ShouldReturnTrue()
        {
            // Убивает мутант: _list.Remove на return false
            set.Add(1);
            bool removed = set.Remove(1);
            ClassicAssert.IsTrue(removed); // явная проверка результата
        }
    }
}