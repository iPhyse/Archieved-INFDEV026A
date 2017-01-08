using Microsoft.Xna.Framework;

namespace Frontend.ExcerciseTwo
{
    public static class Tree
    {
        public static Node<Vector2> Insert(Node<Vector2> root, Vector2 key)
        {
            if (root == null)
            {
                //When the tree is empty on initiatiation, create a root
                root = new Node<Vector2>(key, new Node<Vector2>(), new Node<Vector2>());
            }
            else if (root.Vector2.Equals(key)) { } //The node does already excist in the tree
            else
            {
                    if (key.Y < root.Vector2.Y)
                    {
                        root.Left = Insert(root.Left, key);
                    }
                    else
                    {
                        root.Right = Insert(root.Right, key);
                    }
            }
            return root;
        }
    }
}
