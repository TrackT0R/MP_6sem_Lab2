using System;
using Lab2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVLUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        //высота дерева с семью элементами 3
        [TestMethod]
        public void HeightWithSevenElems()
        {
            var tree = new AVL<int, int>();
            Assert.AreEqual(tree.getTreeHeight(), 0);

            tree.Insert(1, 1);
            Assert.AreEqual(tree.getTreeHeight(), 1);
            
            tree.Insert(2, 1);
            Assert.AreEqual(tree.getTreeHeight(), 2);
            tree.Insert(3, 1);
            Assert.AreEqual(tree.getTreeHeight(), 2);
            
            tree.Insert(4, 1);
            Assert.AreEqual(tree.getTreeHeight(), 3);
            tree.Insert(5, 1);
            Assert.AreEqual(tree.getTreeHeight(), 3);
            tree.Insert(6, 1);
            Assert.AreEqual(tree.getTreeHeight(), 3);
            tree.Insert(7, 1);
            Assert.AreEqual(tree.getTreeHeight(), 3);

            tree.Insert(8, 1);
            Assert.AreEqual(tree.getTreeHeight(), 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemAlreadyExistException))]
        public void InsertExistingItem()
        {
            var tree = new AVL<int, int>();
            tree.Insert(1, 1);
            tree.Insert(1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotExistException))]
        public void FindNotExistingItem()
        {
            var tree = new AVL<int, int>();
            tree.Insert(1, 1);
            tree.Insert(2, 1);
            tree.FindItem(3);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotExistException))]
        public void DeleteNotExistingItem()
        {
            var tree = new AVL<int, int>();
            tree.Insert(1, 1);
            tree.Insert(2, 1);
            tree.Delete(3);
        }

        //высота после удаления
        [TestMethod]
        public void HeightAfterDeleting()
        {
            var tree = new AVL<int, int>();
            for (int i = 1; i < 8; i++)
                tree.Insert(i, 1);

            for (int i = 7; i > 1; i--)
                tree.Delete(i);

            Assert.AreEqual(tree.getTreeHeight(), 1);
        }
    }
}
