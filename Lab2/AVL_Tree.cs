using System;
using Lab2;

namespace Lab2
{
    public class AVL<TKey, TValue> where TKey   : IComparable<TKey>/* where TValue : IComparable<TValue>*/
    {
        private Node<TKey, TValue> root;

        #region Find
        public Node<TKey, TValue>  FindItem(TKey key)
        {
            var current = root;
            while (current != null) {
                int result = current.Key.CompareTo(key);

                if (result > 0) {
                    if (current.Left == null)
                        break;
                    current = current.Left ?? current;
                }
                else if (result < 0) {
                    if (current.Right == null)
                        break;
                    current = current.Right ?? current;
                }
                else {
                    break;
                }
            }

            return current;
        }
        #endregion

        #region Insert
        public void Insert(TKey key, TValue val)
        {
            if (root == null)
                root = new Node<TKey, TValue>(key, val);
            else {
                var parentNode = FindItem(key);
                var resultComparison = parentNode.Key.CompareTo(key);

                if (resultComparison == 0) {
                    throw new ItemAlreadyExistException();
                }
                else if (resultComparison < 0) {
                    parentNode.Right = new Node<TKey, TValue>(key, val, parentNode);
                }
                else {
                    parentNode.Left = new Node<TKey, TValue>(key, val, parentNode);
                }

                parentNode.RecalculateHeight();
                balance_tree(parentNode);
            }
        }

        private Node<TKey, TValue> RecursiveInsert(Node<TKey, TValue> current, Node<TKey, TValue> n)
        {
            if (current == null) {
                current = n;
            }
            else if (n.Key.CompareTo(current.Key) < 0) {
                current.Left  = RecursiveInsert(current.Left , n);
                balance_tree(current);
            }
            else if (n.Key.CompareTo(current.Key) > 0) {
                current.Right = RecursiveInsert(current.Right, n);
                balance_tree(current);
            }
            else {
                throw new ItemAlreadyExistException();
            }

            return current;
        }
        #endregion

        #region балансировка
        private void balance_tree(Node<TKey, TValue> node)
        {
            while (node != null) {
                int balance = balance_factor(node);

                if (balance > 1) {
                    int leftbalance = balance_factor(node.Left);

                    if (leftbalance <= 0) {
                        RotateLeft(node.Left);
                    }

                    RotateRight(node);

                }
                else if (balance < -1) {
                    int rightbalance = balance_factor(node.Right);

                    if (rightbalance >= 0) {
                        RotateRight(node.Right);
                    }

                    RotateLeft(node);
                }

                node = node.Parent;
            }
        }

        public int getTreeHeight()
        {
            return getHeight(root);
        }

        private int getHeight(Node<TKey, TValue> current)
        {
            return current == null ? 0 : current.Height;
        }

        private int balance_factor(Node<TKey, TValue> current)
        {
            return getHeight(current.Left ) - getHeight(current.Right);
        }
        #endregion

        #region удаление
        public void Delete(TKey key)
        {
            var removableNode = FindItem(key);

            if (removableNode == null || removableNode.Key.CompareTo(key) != 0) {
                throw new ItemNotExistException();
            }

            var successorNode = removableNode;
            Node<TKey, TValue> successorNodeParent = null;

            if (successorNode.Left != null) {
                successorNode = successorNode.Left;

                while (successorNode.Right != null) {
                    successorNode = successorNode.Right;
                }

                successorNodeParent = successorNode.Parent;
            }
            else if (successorNode.Right != null) {
                successorNode = successorNode.Right;

                while (successorNode.Left != null) {
                    successorNode = successorNode.Left;
                }

                successorNodeParent = successorNode.Parent;
            }
            else {
                successorNode = null;
            }

            ReplaceNodes(removableNode, successorNode);

            if (successorNodeParent != null)
                successorNodeParent.RecalculateHeight();
        }
        #endregion

        internal void ReplaceNodes(Node<TKey, TValue> replaceableNode, Node<TKey, TValue> successorNode)
        {
            if (successorNode == null) {
                if (replaceableNode == root) {
                    root = null;
                }
                else if (replaceableNode.Parent.Left != null && replaceableNode.Parent.Left == replaceableNode) {
                    replaceableNode.Parent.Left = null;
                }
                else {
                    replaceableNode.Parent.Right = null;
                }

                return;
            }

            replaceableNode.Key = successorNode.Key;
            replaceableNode.Val = successorNode.Val;

            if (successorNode.Parent != null) {
                if (successorNode.Left != null) {
                    successorNode.Left.Parent = successorNode.Parent;
                    if (successorNode.Parent.Left == successorNode) {
                        successorNode.Parent.Left = successorNode.Left;
                    }
                    else {
                        successorNode.Parent.Right = successorNode.Left;
                    }
                }
                else if (successorNode.Right != null) {
                    successorNode.Right.Parent = successorNode.Parent;
                    if (successorNode.Parent.Left == successorNode) {
                        successorNode.Parent.Left = successorNode.Right;
                    }
                    else {
                        successorNode.Parent.Right = successorNode.Right;
                    }
                }
                else {
                    if (successorNode.Parent.Left == successorNode) {
                        successorNode.Parent.Left = null;
                    }
                    else {
                        successorNode.Parent.Right = null;
                    }
                }
            }
        }

        #region Rotates
        internal void RotateLeft(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> newNode = node.Right;
            node.Right = newNode.Left;
            if (newNode.Left != null) {
                newNode.Left.Parent = node;
            }

            newNode.Parent = node.Parent;

            if (node.Parent == null) {
                root = newNode;
            }
            else if (node == node.Parent.Left) {
                node.Parent.Left = newNode;
            }
            else {
                node.Parent.Right = newNode;
            }

            newNode.Left = node;
            node.Parent = newNode;

            node.RecalculateHeight();
            newNode.RecalculateHeight();
        }

        internal void RotateRight(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> newNode = node.Left;
            node.Left = newNode.Right;
            if (newNode.Right != null) {
                newNode.Right.Parent = node;
            }

            newNode.Parent = node.Parent;

            if (node.Parent == null) {
                root = newNode;
            }
            else if (node == node.Parent.Right) {
                node.Parent.Right = newNode;
            }
            else {
                node.Parent.Left = newNode;
            }

            newNode.Right = node;
            node.Parent = newNode;

            node.RecalculateHeight();
            newNode.RecalculateHeight();
        }
        #endregion
    }

    [Serializable]
    public class ItemAlreadyExistException : Exception
    {
        public ItemAlreadyExistException() {
        }
    }

    [Serializable]
    public class ItemNotExistException : Exception
    {
        public ItemNotExistException() {
        }
    }
}
