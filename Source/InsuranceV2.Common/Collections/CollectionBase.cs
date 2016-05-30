using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InsuranceV2.Common.Collections
{
    public abstract class CollectionBase<T> : Collection<T>
    {
        protected CollectionBase() : base(new List<T>())
        {
        }

        protected CollectionBase(IList<T> initialList) : base(initialList)
        {
        }

        protected CollectionBase(CollectionBase<T> initialList) : base(initialList)
        {
        }

        public void Sort(IComparer<T> comparer)
        {
            var list = Items as List<T>;
            list?.Sort(comparer);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection), "Parameter collection is null");
            }
            foreach (var item in collection)
            {
                Add(item);
            }
        }
    }
}