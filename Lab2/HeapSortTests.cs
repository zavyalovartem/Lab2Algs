using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    [TestFixture]
    class HeapSortTests
    {
        [TestCase(5, 0, 2)]
        [TestCase(100, 0, 20)]
        [TestCase(2000, 0, 20)]
        public void HeapSortTest(int numOfWords, int minLength, int maxLength)
        {
            var generator = new Lab2Main();
            var arr = generator.generateStrings(numOfWords, minLength, maxLength).ToArray();
            var comparer = StringComparer.Ordinal;
            Heap<string> heap = new Heap<string>(arr, comparer);
            heap.HeapSort();
            Assert.IsTrue(comparer.Compare(heap._array[0], heap._array[1]) <= 0 && comparer.Compare(heap._array[0], heap._array[2]) <= 0);
        }

        [TestCase(5, 0, 5)]
        [TestCase(100, 0, 20)]
        [TestCase(2000, 0, 20)]
        public void BuildMaxHeapTest(int numOfWords, int minLength, int maxLength)
        {
            var generator = new Lab2Main();
            var arr = generator.generateStrings(numOfWords, minLength, maxLength).ToArray();
            var comparer = StringComparer.Ordinal;
            Heap<string> heap = new Heap<string>(arr, comparer);
            heap.build_max_heap();
            Assert.IsTrue(comparer.Compare(heap._array[0], heap._array[1]) >= 0 && comparer.Compare(heap._array[0], heap._array[2]) >= 0);
        }
    }
}
