using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    interface ITree<TValue> where TValue : IComparable<TValue>
    {
        TreeNode<TValue> Root { get; set;}

        bool Insert(TValue value);

        TreeNode<TValue> Find(TValue value);

        bool Contains(TValue item);

        void Delete(TValue value);
    }
}