using System;
using System.Collections.Generic;
using FluentAssertions;
using InsuranceV2.Common.Collections;
using InsuranceV2.Common.Models;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit
{
    [TestFixture]
    public class CollectionBaseTests : UnitTestBase
    {
        [Test]
        public void AddRangeThrowsWhenCollectionIsNull()
        {
            Action act = () =>
            {
                var collection = new StubCollection();
                collection.AddRange(null);
            };
            act.ShouldThrow<ArgumentNullException>().Where(x => x.Message.Contains("Parameter collection is null"));
        }

        [Test]
        public void NewCollectionUsingExistingCollectionAddsValues()
        {
            var collection1 = new StubCollection(new List<int> {1, 2, 3});
            var collection2 = new StubCollection(collection1);
            collection2.Count.Should().Be(3);
        }

        [Test]
        public void NewCollectionUsingNewListsAddsValues()
        {
            var collection = new StubCollection(new List<int> {1, 2, 3});
            collection.Count.Should().Be(3);
        }

        [Test]
        public void SortInsureesWithSpecifiedComparerSortsCorrectly()
        {
            var insurees = new Insurees
            {
                new Insuree {FirstName = "TestFirstName1", LastName = "aTestLastName1"},
                new Insuree {FirstName = "TestFirstName2", LastName = "cTestLastName2"},
                new Insuree {FirstName = "TestFirstName3", LastName = "bTestLastName3"}
            };

            insurees.Sort(new InsureeComparer());

            insurees[0].FullName.Should().Be("aTestLastName1, TestFirstName1");
            insurees[1].FullName.Should().Be("bTestLastName3, TestFirstName3");
            insurees[2].FullName.Should().Be("cTestLastName2, TestFirstName2");
        }

        [Test]
        public void SortIntsSorts()
        {
            var ints = new StubCollection {3, 2, 1};
            ints.Sort(null);
            ints[0].Should().Be(1);
            ints[1].Should().Be(2);
            ints[2].Should().Be(3);
        }

        [Test]
        public void UsingAddRangeAddsValues()
        {
            var collection1 = new StubCollection(new List<int> {1, 2, 3});
            var collection2 = new StubCollection();
            collection2.AddRange(collection1);
            collection2.Count.Should().Be(3);
        }
    }

    internal class StubCollection : CollectionBase<int>
    {
        public StubCollection()
        {
        }

        public StubCollection(IList<int> initialList)
            : base(initialList)
        {
        }

        public StubCollection(CollectionBase<int> initialList)
            : base(initialList)
        {
        }
    }

    public class InsureeComparer : IComparer<Insuree>
    {
        public int Compare(Insuree x, Insuree y)
        {
            return string.Compare(x.FullName, y.FullName, StringComparison.Ordinal);
        }
    }
}