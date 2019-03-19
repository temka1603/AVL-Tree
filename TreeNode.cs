using System;

namespace AVLTree
{
   public class TreeNode<T> where T : IComparable<T>
   {
        public T Value { get; set; }
        
        public TreeNode<T> Parent { get; set; }

        public TreeNode<T> Left { get; set; }

        public TreeNode<T> Right { get; set; }

        public int Height { get; set; }

       public TreeNode(T value, TreeNode<T> node)
       {
           Value = value;
           Parent = node;
           Left = null;
           Right = null;
           Height = 0;
       } 

        public TreeNode(T value)
        {
            this.Value = value;
        }

    }

}