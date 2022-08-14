using Algorithms.Others;

namespace Algorithms.Test
{

    [TestClass]
    public class OthersTests
    {
        private readonly int[] sortedArray = new int[] { 1, 3, 5, 8, 10, 15, 20 };


        public OthersTests()
        {

        }

        [TestMethod]
        public void BinarySearch_Test()
        {
            var result= BinarySearch<int>.Search(sortedArray, 15, null, null);
            Assert.AreEqual(5, result);
            var notFoundResult = BinarySearch<int>.Search(sortedArray, 2, null, null);
            Assert.AreEqual(-1, notFoundResult);
        }

    }
}