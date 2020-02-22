using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class HeapTests
    {
        [TestCase(new[] { 10 }, new[] { 10 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 5, 4, 2, 1, 3 })]
        [TestCase(new[] { 1000, 2, 567, 36, 999, 1001 }, new[] { 1001, 999, 1000, 2, 36, 567 })]
        [TestCase(new[] { 5, 20, 3, 40, 6, 51, 0, -10, 46 }, new[] { 51, 46, 40, 20, 6, 3, 0, -10, 5 })]
        public void TestInsertInternal(int[] elementsToInsert, int[] expectedInternalArray)
        {
            var heap = new HeapMock<int>();

            foreach (var element in elementsToInsert)
            {
                heap.Insert(element);
            }

            Assert.That(heap.GetInternalArray(), Is.EqualTo(expectedInternalArray));
        }

        [TestCase(new[] { 15 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1000, 2, 567, 36, 999, 1001 })]
        [TestCase(new[] { 5, 20, 3, 40, 6, 51, 0, -10, 46 })]
        [TestCase(new[] { -514, 20, 25, 3145, 10, 13, -15, 111, 23, 1001 })]
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
            var heap = new HeapMock<int>();

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
