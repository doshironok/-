using NUnit.Framework;
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
            Assert.IsTrue(set.IsEmpty);
            Assert.AreEqual(0, set.Count);
        }

        [Test]
        public void Add_Element_AddsSuccessfully()
        {
            var result = set.Add(1);
            Assert.IsTrue(result);
            Assert.IsFalse(set.IsEmpty);
            Assert.AreEqual(1, set.Count);
        }

        [Test]
        public void Add_Duplicate_ReturnsFalse()
        {
            set.Add(1);
            var result = set.Add(1);
            Assert.IsFalse(result);
            Assert.AreEqual(1, set.Count);
        }

        [Test]
        public void Remove_ExistingElement_RemovesSuccessfully()
        {
            set.Add(1);
            var result = set.Remove(1);
            Assert.IsTrue(result);
            Assert.IsTrue(set.IsEmpty);
            Assert.AreEqual(0, set.Count);
        }

        [Test]
        public void Remove_NonExistingElement_ReturnsFalse()
        {
            var result = set.Remove(1);
            Assert.IsFalse(result);
            Assert.IsTrue(set.IsEmpty);
            Assert.AreEqual(0, set.Count);
        }

        [Test]
        public void Contains_Element_ReturnsCorrectly()
        {
            set.Add(1);
            Assert.IsTrue(set.Contains(1));
            Assert.IsFalse(set.Contains(2));
        }

        [Test]
        public void Clear_SetsToEmpty()
        {
            set.Add(1);
            set.Add(2);
            set.Clear();
            Assert.IsTrue(set.IsEmpty);
            Assert.AreEqual(0, set.Count);
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
    }
}