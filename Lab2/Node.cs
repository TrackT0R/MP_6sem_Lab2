using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    public class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Val { get; set; }
        internal int Height { get; set; } = 1;

        internal Node<TKey, TValue> Parent { get; set; }
        internal Node<TKey, TValue> Left { get; set; }
        internal Node<TKey, TValue> Right { get; set; }


        public Node()
        {
        }

        public Node(TKey key, TValue val)
        {
            this.Val = val;
            this.Key = key;
            this.Parent = null;
        }

        public Node(TKey key, TValue val, Node<TKey, TValue> Parent)
        {
            this.Val = val;
            this.Key = key;
            this.Parent = Parent;
        }

        internal void RecalculateHeight()
        {
            var currentNode = this;

            while (currentNode != null) {
                if (currentNode.Left == null && currentNode.Right == null) {
                    currentNode.Height = 1;
                }
                else if (currentNode.Left == null) {
                    currentNode.Height = currentNode.Right.Height + 1;
                }
                else if (currentNode.Right == null) {
                    currentNode.Height = currentNode.Left.Height + 1;
                }
                else {
                    int maxHeight = Math.Max(currentNode.Right.Height, currentNode.Left.Height) + 1;
                    currentNode.Height = maxHeight;
                }

                currentNode = currentNode.Parent;
            }
        }
    }
}
