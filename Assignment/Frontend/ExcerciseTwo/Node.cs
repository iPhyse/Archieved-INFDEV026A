using Microsoft.Xna.Framework;

namespace Frontend.ExcerciseTwo
{
    public class Node<T>
    {
        public Vector2 Vector2 { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node()
        {
        }

        public Node(Vector2 vector, Node<T> left, Node<T> right)
        {
            Vector2 = vector;
            Left = left;
            Right = right;
        }
    }
}
