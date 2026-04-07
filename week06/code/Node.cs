public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1: Only allow unique values.
        // If the value equals the current node's data, do nothing (ignore duplicate).
        if (value == Data)
            return;

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // Problem 2: Search for value using BST rules.
        // Base case: if the current node matches, return true.
        // If value is smaller, search left subtree.
        // If value is larger, search right subtree.
        // If we reach null (no child), the value is not in the tree.

        if (value == Data)
            return true;

        if (value < Data)
            return Left is not null && Left.Contains(value);
        else
            return Right is not null && Right.Contains(value);
    }

    public int GetHeight()
    {
        // Problem 4: Height = 1 + max(left height, right height).
        // If a subtree is null, its height is 0.
        // Recursively get heights of left and right subtrees,
        // then return 1 plus whichever is greater.

        int leftHeight = Left is null ? 0 : Left.GetHeight();
        int rightHeight = Right is null ? 0 : Right.GetHeight();

        return 1 + Math.Max(leftHeight, rightHeight);
    }
}