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
            return Find(key, root);
        }

        private Node<TKey, TValue> Find(TKey Key, Node<TKey, TValue> elem)
        {
            var res = new Node<TKey, TValue>();

            if (elem.Key.Equals(Key))
                res = elem;
            else if (elem.Key.CompareTo(Key) > 0) {
                if (elem.left == null)
                    throw new ItemNotExistException();
                res = Find(Key, elem.left);
            }
            else {
                if (elem.left == null)
                    throw new ItemNotExistException();
                res = Find(Key, elem.right);
            }

            return res;
        }
        #endregion

        #region Insert
        public void Insert(TKey key, TValue val)
        {
            root = RecursiveInsert(root, new Node<TKey, TValue>(key, val));
        }

        private Node<TKey, TValue> RecursiveInsert(Node<TKey, TValue> current, Node<TKey, TValue> n)
        {
            if (current == null) {
                current = n;
            }
            else if (n.Key.CompareTo(current.Key) < 0) {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.Key.CompareTo(current.Key) > 0) {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            else {
                throw new ItemAlreadyExistException();
            }

            return current;
        }
        #endregion

        #region балансировка
        private Node<TKey, TValue> balance_tree(Node<TKey, TValue> current)
        {
            int b_factor = balance_factor(current);
                
            return b_factor >  1 ? (balance_factor(current.left)  > 0 ? RotateLL(current) : RotateLR(current)):
                    b_factor < -1 ? (balance_factor(current.right) > 0 ? RotateRL(current) : RotateRR(current)):
                    current;
        }

        public int getTreeHeight()
        {
            return getHeight(root);
        }

        private int getHeight(Node<TKey, TValue> current)
        {
            return current == null ? 0 : Math.Max(getHeight(current.left), getHeight(current.right)) + 1; ;
        }

        private int balance_factor(Node<TKey, TValue> current)
        {
            return getHeight(current.left) - getHeight(current.right);
        }
        #endregion

        #region удаление
        public void Delete(TKey target)
        {
            root = delete(root, target);
        }
        private Node<TKey, TValue> delete(Node<TKey, TValue> current, TKey target)
        {
            Node<TKey, TValue> parent;
            if (current == null)
                throw new ItemNotExistException();

            if (target.CompareTo(current.Key) < 0) {
                current.left = delete(current.left, target);
                if (balance_factor(current) == -2)
                    current = balance_factor(current.right) <= 0 ? RotateRR(current) : RotateRL(current);
            }
            else if (target.CompareTo(current.Key) > 0) {
                current.right = delete(current.right, target);
                if (balance_factor(current) == 2)
                    current = balance_factor(current.left) >= 0 ? RotateLL(current) : RotateLR(current);
            }
            else if (current.right != null) {
                //находим минимальное значение в правом поддереве
                parent = current.right;
                while (parent.left != null) {
                    parent = parent.left;
                }
                //заменяем значение минимальным, а его удаляем
                current.Val = parent.Val;
                current.right = delete(current.right, parent.Key);
                //при необходимости балансируем дерево
                if (balance_factor(current) == 2) {
                    current = balance_factor(current.left) >= 0 ? RotateLL(current):
                                                                    RotateLR(current);
                }
            }
            else
                current = current.left;

            return current;
        }
        #endregion

        #region Rotates
        private Node<TKey, TValue> RotateRR(Node<TKey, TValue> parent)
        {
            Node<TKey, TValue> pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node<TKey, TValue> RotateLL(Node<TKey, TValue> parent)
        {
            Node<TKey, TValue> pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node<TKey, TValue> RotateLR(Node<TKey, TValue> parent)
        {
            Node<TKey, TValue> pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node<TKey, TValue> RotateRL(Node<TKey, TValue> parent)
        {
            Node<TKey, TValue> pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
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
