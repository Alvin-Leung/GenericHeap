using System;
using System.Collections.Generic;

namespace GenericHeap
{
    /// <summary>
    /// A generic heap implementation that allows injection of custom comparison strategies for 
    /// determining element priority
    /// </summary>
    /// <typeparam name="T">Type of element that will be stored in the heap</typeparam>
    public class Heap<T> where T : IComparable
    {
        /// <summary>
        /// The heap's internal elements
        /// </summary>
        protected readonly List<T> elements;
        private readonly IComparer<T> comparer;

        /// <summary>
        /// Returns true if there are no elements in the heap, otherwise returns false
        /// </summary>
        public bool IsEmpty => elements.Count == 0;

        /// <summary>
        /// Returns the number of elements in the heap
        /// </summary>
        public int Count => elements.Count;

        /// <summary>
        /// Constructs a <see cref="Heap{T}"/> with default comparer for type <see cref="T"/>
        /// </summary>
        public Heap() : this(Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Constructs a <see cref="Heap{T}"/> with custom comparer for type <see cref="T"/>
        /// </summary>
        /// <param name="comparer">The custom comparer to use when determining priority order</param>
        public Heap(IComparer<T> comparer)
        {
            this.comparer = comparer;
            this.elements = new List<T>();
        }

        /// <summary>
        /// Removes the highest priority element from the heap and returns it to the caller of <see cref="Poll"/>
        /// </summary>
        /// <returns>The highest priority element</returns>
        /// <exception cref="InvalidOperationException">Thrown when there are no elements in the heap</exception>
        public T Poll()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("There are no elements in the heap to poll");
            }

            var rootIndex = 0;
            var lastElementIndex = this.elements.Count - 1;
            var root = this.elements[rootIndex];

            this.elements[rootIndex] = this.elements[lastElementIndex];
            this.elements.RemoveAt(lastElementIndex);

            this.BubbleDown(rootIndex);

            return root;
        }

        /// <summary>
        /// Inserts an element into the heap
        /// </summary>
        /// <param name="newElement">The element to insert</param>
        public void Insert(T newElement)
        {
            this.elements.Add(newElement);
            this.BubbleUp(this.elements.Count - 1);
        }

        /// <summary>
        /// Gets the highest priority element from the heap without removing it
        /// </summary>
        /// <returns>The highest priority element</returns>
        public T Peek()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("There are no elements in the heap to peek");
            }

            return this.elements[0];
        }

        private void BubbleDown(int parentIndex)
        {
            var leftChildIndex = this.GetIndexOfLeftChild(parentIndex);
            var rightChildIndex = this.GetIndexOfRightChild(parentIndex);
            var maxIndex = this.elements.Count - 1;

            if (leftChildIndex > maxIndex)
            {
                return;
            }

            if (rightChildIndex > maxIndex)
            {
                if (this.IsChildHigherPriority(parentIndex, leftChildIndex))
                {
                    this.SwapElements(parentIndex, leftChildIndex);
                }

                return;
            }

            this.SwapWithHigherPriorityChild(parentIndex);
        }

        private void SwapWithHigherPriorityChild(int parentIndex)
        {
            var leftChildIndex = this.GetIndexOfLeftChild(parentIndex);
            var rightChildIndex = this.GetIndexOfRightChild(parentIndex);
            var isLeftChildHigherPriority = this.IsChildHigherPriority(parentIndex, leftChildIndex);
            var isRightChildHigherPriority = this.IsChildHigherPriority(parentIndex, rightChildIndex);

            if (isLeftChildHigherPriority && isRightChildHigherPriority)
            {
                var higherPriorityChildIndex = this.GetHigherPriorityElementIndex(leftChildIndex, rightChildIndex);
                this.SwapElements(parentIndex, higherPriorityChildIndex);
                this.BubbleDown(higherPriorityChildIndex);
            }
            else if (isLeftChildHigherPriority)
            {
                this.SwapElements(parentIndex, leftChildIndex);
                this.BubbleDown(leftChildIndex);
            }
            else if (isRightChildHigherPriority)
            {
                this.SwapElements(parentIndex, rightChildIndex);
                this.BubbleDown(rightChildIndex);
            }
        }

        private void BubbleUp(int childIndex)
        {
            if (childIndex == 0)
            {
                return;
            }

            var parentIndex = this.GetParentIndex(childIndex);

            if (IsChildHigherPriority(parentIndex, childIndex))
            {
                this.SwapElements(parentIndex, childIndex);
                this.BubbleUp(parentIndex);
            }
        }

        private bool IsChildHigherPriority(int parentIndex, int childIndex)
        {
            return this.comparer.Compare(this.elements[childIndex], this.elements[parentIndex]) > 0;
        }

        private int GetHigherPriorityElementIndex(int leftElementIndex, int rightElementIndex)
        {
            if (this.comparer.Compare(this.elements[leftElementIndex], this.elements[rightElementIndex]) > 0)
            {
                return leftElementIndex;
            }
            else
            {
                return rightElementIndex;
            }
        }

        private void SwapElements(int firstIndex, int secondIndex)
        {
            var temp = this.elements[firstIndex];
            this.elements[firstIndex] = this.elements[secondIndex];
            this.elements[secondIndex] = temp;
        }

        private int GetParentIndex(int childIndex)
        {
            if (childIndex % 2 == 0)
            {
                return (childIndex - 2) / 2;
            }
            else
            {
                return (childIndex - 1) / 2;
            }
        }

        private int GetIndexOfLeftChild(int currentIndex)
        {
            return 2 * currentIndex + 1;
        }

        private int GetIndexOfRightChild(int currentIndex)
        {
            return 2 * currentIndex + 2;
        }
    }
}
