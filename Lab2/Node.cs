using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    public class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Val { get; set; }
        internal Node<TKey, TValue> left { get; set; }
        internal Node<TKey, TValue> right { get; set; }


        public Node()
        {
        }

        public Node(TKey key, TValue val)
        {
            this.Val = val;
            this.Key = key;
        }
    }
}
