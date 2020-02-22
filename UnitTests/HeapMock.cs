using GenericHeap;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    /// <summary>
    /// A mock of the <see cref="Heap{T}"/> class for access to its internal list of elements
    /// </summary>
    /// <typeparam name="T">Type of element that will be stored in the heap</typeparam>
    public class HeapMock<T> : Heap<T> where T : IComparable
    {
        /// <summary>
        /// Constructs a <see cref="HeapMock{T}"/> with default comparer for type <see cref="T"/>
        /// </summary>
        public HeapMock() : base()
        {
        }

        /// <summary>
        /// Constructs a <see cref="HeapMock{T}"/> with custom comparer for type <see cref="T"/>
        /// </summary>
        /// <param name="comparer">The custom comparer to use when determining priority order</param>
        public HeapMock(IComparer<T> comparer) : base(comparer)
        {
        }

        /// <summary>
        /// Gets an array containing the internal elements of <see cref="Heap{T}"/>
        /// </summary>
        /// <returns>An array containing the internal elements of <see cref="Heap{T}"/></returns>
        public T[] GetInternalArray()
        {
            return this.elements.ToArray();
        }
    }
}
