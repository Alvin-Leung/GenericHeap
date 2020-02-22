using GenericHeap;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class HeapMock<T> : Heap<T> where T : IComparable
    {
        public HeapMock() : base()
        {
        }

        public HeapMock(IComparer<T> comparer) : base(comparer)
        {
        }

        public T[] GetInternalArray()
        {
            return this.elements.ToArray();
        }
    }
}
