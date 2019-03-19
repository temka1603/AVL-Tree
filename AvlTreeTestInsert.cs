using NUnit.Framework;
using AVLTree;

namespace AVLTreeTests
{
    public class AvlTreeTestInsert
    {
        public AvlTree<int> IntTree;

        [SetUp]
        public void Initialize()
        {
            IntTree = new AvlTree<int>();
            IntTree.Insert(6);
        }

        public void Insert(params int[] integers)
        {
            for (int i = 0; i < integers.Length; ++i)
            {
                IntTree.Insert(integers[i]);
            }
        }

        public void AssertTrueContains(params int[] integers)
        {
            for (int i = 0; i < integers.Length; ++i)
            {
                Assert.True(IntTree.Contains(integers[i]));
            }
        }

        public void MakeGraph(string fileName)
        {
            OutputTree<int> outputObj = new OutputTree<int>();
            outputObj.BuildGraph(IntTree.Root, fileName);
        }

        [Test()]
        public void TestContains()
        {

            bool result = IntTree.Contains(6);

            MakeGraph("insert1");

            Assert.True(result);
        }

        [Test(Description = "Test Insert and his Contains")]
        public void TestInsertedElementShouldBeInTree()
        {
            IntTree.Insert(4);

            bool result = IntTree.Contains(4);

            MakeGraph("insert2");

            Assert.True(result);
        }

        [Test(Description = "Test Insert")]
        public void TestCountShouldBeTwo()
        {
            Insert(2, 8);

            MakeGraph("insert3");

            AssertTrueContains(2, 8);
        }

        [Test(Description = "Check Balance of Tree")]
        public void TreeShouldBeBalanced()
        {
            Insert(2, 8, 0, 1, -1);

            MakeGraph("insert4");

            AssertTrueContains(2, 8, 0, 1, -1);
        }

        [Test(Description = "Check Balance of Tree")]
        public void TreeShouldBeBalancedUseRotateRight()
        {
            Insert(2, 8, 0, 11, 12, 13, 14);

            MakeGraph("insert5");

            AssertTrueContains(2, 8, 0, 11, 12, 13, 14);
        }
    }
}
