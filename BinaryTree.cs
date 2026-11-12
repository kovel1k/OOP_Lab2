using System;
using System.Collections;
using System.Collections.Generic;

namespace OOP_Lab3_2
{
    // Вузол бінарного дерева
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }

    // Узагальнене бінарне дерево з обмеженням class (не структури) та IComparable<T>
    public class BinaryTree<T> : IEnumerable<T> where T : class, IComparable<T>
    {
        public Node<T> Root { get; private set; }

        public void Add(T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            if (Root == null)
            {
                Root = new Node<T>(data);
                return;
            }

            AddRecursive(Root, data);
        }

        private void AddRecursive(Node<T> current, T data)
        {
            // Використання IComparable<T> для розміщення елементів у дереві
            if (data.CompareTo(current.Data) < 0)
            {
                if (current.Left == null)
                    current.Left = new Node<T>(data);
                else
                    AddRecursive(current.Left, data);
            }
            else
            {
                if (current.Right == null)
                    current.Right = new Node<T>(data);
                else
                    AddRecursive(current.Right, data);
            }
        }

        // Власний ітератор для обходу в прямому порядку (Preorder)
        public IEnumerator<T> GetEnumerator()
        {
            return new PreorderEnumerator(Root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Реалізація IEnumerator<T>
        private class PreorderEnumerator : IEnumerator<T>
        {
            private readonly Node<T> _root;
            private Stack<Node<T>> _stack;
            private T _current;

            public PreorderEnumerator(Node<T> root)
            {
                _root = root;
                _stack = new Stack<Node<T>>();
                if (_root != null)
                {
                    _stack.Push(_root);
                }
            }

            public T Current => _current;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_stack.Count == 0)
                    return false;

                Node<T> node = _stack.Pop();
                _current = node.Data;

                // Для прямого порядку (Preorder: Root -> Left -> Right)
                // у стек спочатку кладемо правого нащадка, потім лівого
                if (node.Right != null)
                    _stack.Push(node.Right);
                if (node.Left != null)
                    _stack.Push(node.Left);

                return true;
            }

            public void Reset()
            {
                _stack.Clear();
                if (_root != null)
                {
                    _stack.Push(_root);
                }
                _current = default(T);
            }

            public void Dispose()
            {
                _stack = null;
            }
        }
    }
}
