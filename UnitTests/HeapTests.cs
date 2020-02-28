using GenericHeap;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    /// <summary>
    /// Tests for the <see cref="Heap{T}"/> class
    /// </summary>
    public class HeapTests
    {
        /// <summary>
        /// Checks that the state of the internal array in <see cref="Heap{T}"/> is correct after insertion
        /// of elements
        /// </summary>
        /// <param name="elementsToInsert">The elements to insert into the heap</param>
        /// <param name="expectedElements">The expected internal array after insertion of all elements</param>
        [TestCase(new[] { 10 }, new[] { 10 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 5, 4, 2, 1, 3 })]
        [TestCase(new[] { 1000, 2, 567, 36, 999, 1001 }, new[] { 1001, 999, 1000, 2, 36, 567 })]
        [TestCase(new[] { 5, 20, 3, 40, 6, 51, 0, -10, 46 }, new[] { 51, 46, 40, 20, 6, 3, 0, -10, 5 })]
        public void TestInsertInternal(int[] elementsToInsert, int[] expectedElements)
        {
            var heap = new HeapMock<int>();

            foreach (var element in elementsToInsert)
            {
                heap.Insert(element);
            }

            Assert.That(heap.GetInternalArray(), Is.EqualTo(expectedElements));
        }

        /// <summary>
        /// Checks that the state of the internal array in <see cref="Heap{T}"/> is correct after removal
        /// of elements
        /// </summary>
        /// <param name="elementsToInsert">The elements to insert into the heap</param>
        /// <param name="elementsToRemove">The elements that will be removed from the heap</param>
        /// <param name="expectedElements">The expected internal array after insertion of all elements</param>
        [TestCase(new[] { 10 }, new[] { 10 }, new int[0])]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 4, 2 }, new[] { 5, 3, 1 })]
        [TestCase(new[] { 1000, 2, 567, 36, 999, 1001 }, new[] { 999, 567, 2 }, new[] { 1001, 36, 1000 })]
        [TestCase(new[] { 5, 20, 3, 40, 6, 51, 0, -10, 46 }, new[] { 46, 40 }, new[] { 51, 20, 3, 5, 6, -10, 0 })]
        [TestCase(new[] { 5, 5, 5, 5, 5 }, new[] { 5, 5 }, new[] { 5, 5, 5 })]
        [TestCase(new[] { 5, 100, 3, 10, 7, 100, 20, 5, 100 }, new[] { 100, 100 }, new[] { 100, 10, 20, 5, 7, 3, 5 })]
        public void TestRemoveInternal(int[] elementsToInsert, int[] elementsToRemove, int[] expectedElements)
        {
            var heap = new HeapMock<int>();

            foreach (var element in elementsToInsert)
            {
                heap.Insert(element);
            }

            foreach (var element in elementsToRemove)
            {
                heap.Remove(element);
            }

            Assert.That(heap.GetInternalArray(), Is.EqualTo(expectedElements));
        }

        /// <summary>
        /// Checks that after inserting elements, polled values are returned in order of descending priority
        /// </summary>
        /// <param name="elementsToInsert">The elements to insert into the heap</param>
        [TestCase(new[] { 15 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1000, 2, 567, 36, 999, 1001 })]
        [TestCase(new[] { 5, 20, 3, 40, 6, 51, 0, -10, 46 })]
        [TestCase(new[] { -514, 20, 25, 3145, 10, 13, -15, 111, 23, 1001 })]
        [TestCase(new[] { -514, 30, 13432, 2, 30, 13, 13, 30, 50, 1001 })]
        [TestCase(new[] { 5950, -3669, -8120, -1500, -9856, -3287, -8245, 7474, -6259, -4816, -6742,
            5629, 9844, -7469, -5012, -1934, 701, -5852, 1292, 3251, -5159, 9304, -9625, 6531, -9771,
            2944, 7809, 213, 1593, -3080, -9253, -8800, 2735, 8509, -2457, 8660, -456, -8129, -4911,
            -2079, 5487, 9348, 5074, 1418, 7932, -2344, 1220, 7102, 2275, -5368, -6204, -5200, 9634,
            -6939, -2260, 8964, -7898, 1319, 6180, -326, 8553, 8748, -9875, 6688, 8693, -88, 9079,
            -4814, 3727, 8890, -2075, -8025, -6551, -7413, 6359, -7755, -4621, -3855, -433, 3932, -265,
            9008, 4183, -2765, 1057, -7578, -5517, -9142, 7227, 4923, -6464, 8602, -3920, 7167, 7539,
            877, 8593, 4706, 5517, 9143 })]
        public void TestPoll(int[] elementsToInsert)
        {
            var heap = new Heap<int>();

            foreach (var element in elementsToInsert)
            {
                heap.Insert(element);
            }

            var expectedElements = elementsToInsert.OrderByDescending(e => e);

            foreach (var expectedElement in expectedElements)
            {
                Assert.That(heap.Poll(), Is.EqualTo(expectedElement));
            }
        }
    }
}
