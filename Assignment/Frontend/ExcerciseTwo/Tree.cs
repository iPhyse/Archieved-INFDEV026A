using Microsoft.Xna.Framework;

namespace Frontend.ExcerciseTwo
{
    public static class Tree
    {
        public static Node<Vector2> Insert(Vector2 b, Node<Vector2> n, bool x)
        {
            if (n == null)
            {
                //When the tree is empty on initiatiation, create a root
                n = new Node<Vector2>(b, new Node<Vector2>(), new Node<Vector2>());
            }
            else if (n.Vector2.Equals(b)) { } //The node does already excist in the tree
            else
            {
                if (x)
                {
                    if (b.X < n.Vector2.X)
                    {
                        n.Left = Insert(b, n.Left, false);
                    }
                    else
                    {
                        n.Right = Insert(b, n.Right, false);
                    }
                }
                else
                {
                    if (b.Y < n.Vector2.Y)
                    {
                        n.Left = Insert(b, n.Left, true);
                    }
                    else
                    {
                        n.Right = Insert(b, n.Right, true);
                    }
                }
            }
            return n;
        }
    }
}
