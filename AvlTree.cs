using System;

namespace AVLTree
{
    public class AvlTree<T> : ITree<T> where T : IComparable<T>
    {
        public TreeNode<T> Root { get; set; }

        public AvlTree()
        {
            Root = null;
        }

        public bool Insert(T value)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>(value, null);
            }
            else
            {
                TreeNode<T> root = Root;
                TreeNode<T> parent;

                while (true)
                {
                    if (root.Value.CompareTo(value) == 0)
                        return false;

                    parent = root;

                    bool goLeft = root.Value.CompareTo(value) > 0;
                    root = goLeft ? root.Left : root.Right;

                    if (root == null)
                    {
                        if (goLeft)
                        {
                            parent.Left = new TreeNode<T>(value, parent);
                        }
                        else
                        {
                            parent.Right = new TreeNode<T>(value, parent);
                        }

                        Rebalance(parent);
                        break;
                    }
                }
            }

            return true;
        }

        void SetBalance(TreeNode<T> node)
        {
            node.Height = Height(node.Right) - Height(node.Left);
        }

        int Height(TreeNode<T> node)
        {
            if (node == null)
                return -1;

            return 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        void Rebalance(TreeNode<T> node)
        {
            SetBalance(node);

            if (node.Height == -2)
            {
                if (Height(node.Left.Left) >= Height(node.Left.Right))
                    node = RotateRight(node);
                else
                    node = RotateLeftThenRight(node);
            }
            else if (node.Height == 2)
            {
                if (Height(node.Right.Right) >= Height(node.Right.Left))
                    node = RotateLeft(node);
                else
                    node = RotateRightThenLeft(node);
            }

            if (node.Parent != null)
            {
                Rebalance(node.Parent);
            }
            else
            {
                Root = node;
            }
        }

        TreeNode<T> RotateLeft(TreeNode<T> node)
        {
            TreeNode<T> temp = node.Right;
            temp.Parent = node.Parent;
            node.Right = temp.Left;

            if (node.Right != null)
                node.Right.Parent = node;

            temp.Left = node;
            node.Parent = temp;

            if (temp.Parent != null)
            {
                if (temp.Parent.Right == node)
                {
                    temp.Parent.Right = temp;
                }
                else
                {
                    temp.Parent.Left = temp;
                }
            }

            SetBalance(node);
            SetBalance(temp);
            return temp;
        }

        TreeNode<T> RotateRight(TreeNode<T> node)
        {
            TreeNode<T> b = node.Left;
            b.Parent = node.Parent;
            node.Left = b.Right;

            if (node.Left != null)
                node.Left.Parent = node;

            b.Right = node;
            node.Parent = b;

            if (b.Parent != null)
            {
                if (b.Parent.Right == node)
                {
                    b.Parent.Right = b;
                }
                else
                {
                    b.Parent.Left = b;
                }
            }

            SetBalance(node);
            SetBalance(b);
            return b;
        }

        TreeNode<T> RotateLeftThenRight(TreeNode<T> n)
        {
            n.Left = RotateLeft(n.Left);
            return RotateRight(n);
        }

        TreeNode<T> RotateRightThenLeft(TreeNode<T> n)
        {
            n.Right = RotateRight(n.Right);
            return RotateLeft(n);
        }

        public void Delete(T delKey)
        {
            if (Root == null)
                return;

            TreeNode<T> node = Root;
            TreeNode<T> parent = Root;
            TreeNode<T> delNode = null;
            TreeNode<T> child = Root;

            while (child != null)
            {
                parent = node;
                node = child;
                child = delKey.CompareTo(node.Value) >= 0 ? node.Right : node.Left;
                if (delKey.CompareTo(node.Value) == 0)
                    delNode = node;
            }

            if (delNode != null)
            {
                delNode.Value = node.Value;

                child = node.Left != null ? node.Left : node.Right;

                if (Root.Value.CompareTo(delKey) == 0)
                {
                    Root = child;
                }
                else
                {
                    if (parent.Left == node)
                    {
                        parent.Left = child;
                    }
                    else
                    {
                        parent.Right = child;
                    }


                    Rebalance(parent);
                }
            }
        }

        public TreeNode<T> Find(TreeNode<T> node, T item)
        {
            if (node == null || item.CompareTo(node.Value) == 0)
            {
                return node;
            }

            if (item.CompareTo(node.Value) < 0)
            {
                return Find(node.Left, item);
            }

            return Find(node.Right, item);
        }

        public TreeNode<T> Find(T item)
        {
            return Find(Root, item);
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }
    }
}
