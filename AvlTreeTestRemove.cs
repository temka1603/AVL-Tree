using NUnit.Framework;
using AVLTree;

namespace AVLTreeTests
{
    [TestFixture]
    public class BinaryTreeTestRemove
    {
        public AvlTree<int> IntTree;

        public void Insert(params int[] integers)
        {
            for (int i = 0; i < integers.Length; ++i)
            {
                IntTree.Insert(integers[i]);
            }
        }

        public void MakeGraph(string fileName)
        {
            OutputTree<int> outputObj = new OutputTree<int>();
            outputObj.BuildGraph(IntTree.Root, fileName);
        }

        public void ShouldBeInTree(params int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
                Assert.True(IntTree.Contains(arr[i]));
        }

        [SetUp]
        public void Initialize()
        {
            IntTree = new AvlTree<int>();

            Insert(20, 30, 10, 8, 15, 3);
        }

        [Test(Description = "Test Remove Element With Parent And Without Childs")]
        public void TestRemoveNothing()
        {
            MakeGraph("removeBefore");

            IntTree.Delete(999);

            MakeGraph("removeAfter");

            ShouldBeInTree(20, 30, 8, 10, 3, 15);

            Assert.False(IntTree.Contains(999));
        }

        [Test(Description = "Test Remove Element With Parent And Without Childs")]
        public void TestRemoveNotSingleElementWithoutChild()
        {
            IntTree.Delete(15);

            MakeGraph("remove15");

            ShouldBeInTree(20, 30, 8, 10, 3);

            Assert.False(IntTree.Contains(15));
        }

        [Test(Description = "Test Remove Element With Parent And With Childs")]
        public void TestRemoveNotSingleElementWithChilds()
        {
            IntTree.Delete(10);

            ShouldBeInTree(20, 30, 8, 15, 3);

            MakeGraph("remove10");

            Assert.False(IntTree.Contains(10));
        }

        [Test(Description = "Test Remove Root With Childs")]
        public void TestRemoveRootWithChilds()
        {
            IntTree.Delete(20);

            MakeGraph("remove20");

            ShouldBeInTree(10, 30, 8, 15, 3);

            Assert.False(IntTree.Contains(20));
        }



        [Test(Description = "Test Remove Node With Just One Leaf")]
        public void TestRemoveLeafWithSoloChild()
        {
            IntTree.Insert(40);
            IntTree.Delete(40);

            ShouldBeInTree(20, 10, 30, 8, 15, 3);

            MakeGraph("remove40");

            Assert.False(IntTree.Contains(40));
        }

        [Test(Description = "Test Remove Left Leaf of The Tree")]
        public void TestRemoveLeftLeaf()
        {
            IntTree.Delete(3);

            ShouldBeInTree(20, 10, 30, 8, 15);

            MakeGraph("remove3");

            Assert.False(IntTree.Contains(3));
        }

        [Test(Description = "Test Remove Node With Parent and Single Leaff")]
        public void TestRemoveNodeSingleLeaf()
        {
            IntTree.Delete(8);

            ShouldBeInTree(20, 10, 30, 15, 3);

            Assert.False(IntTree.Contains(8));
        }

        [Test(Description = "Test Remove Not Existed in Tree element")]
        public void TestRemoveNotExisted()
        {
            IntTree.Delete(50);

            MakeGraph("remove50");

            ShouldBeInTree(20, 10, 30, 8, 15, 3);

            Assert.False(IntTree.Contains(50));
        }

    }


}
