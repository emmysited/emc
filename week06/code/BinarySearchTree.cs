using System.Collections;

public class BinarySearchTree : IEnumerable<int>
{
    private Node? _root;

    /// <summary>
    /// Insert a new node in the BST.
    /// </summary>
    public void Insert(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_root is null)
        {
            _root = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            _root.Insert(value);
        }
    }

    /// <summary>
    /// Check to see if the tree contains a certain value
    /// </summary>
    /// <param name="value">The value to look for</param>
    /// <returns>true if found, otherwise false</returns>
    public bool Contains(int value)
    {
        return _root != null && _root.Contains(value);
    }

    /// <summary>
    /// Yields all values in the tree
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the BST
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var numbers = new List<int>();
        TraverseForward(_root, numbers);
        foreach (var number in numbers)
        {
            yield return number;
        }
    }

    private void TraverseForward(Node? node, List<int> values)
    {
        if (node is not null)
        {
            TraverseForward(node.Left, values);
            values.Add(node.Data);
            TraverseForward(node.Right, values);
        }
    }

    /// <summary>
    /// Iterate backward through the BST.
    /// </summary>
    public IEnumerable Reverse()
    {
        var numbers = new List<int>();
        TraverseBackward(_root, numbers);
        foreach (var number in numbers)
        {
            yield return number;
        }
    }

    private void TraverseBackward(Node? node, List<int> values)
    {
        // Problem 3: Mirror of TraverseForward.
        // TraverseForward visits: Left -> Node -> Right (ascending order).
        // TraverseBackward visits: Right -> Node -> Left (descending order).

        if (node is not null)
        {
            TraverseBackward(node.Right, values);  // Visit right subtree first (larger values)
            values.Add(node.Data);                  // Visit current node
            TraverseBackward(node.Left, values);   // Visit left subtree last (smaller values)
        }
    }

    /// <summary>
    /// Get the height of the tree
    /// </summary>
    public int GetHeight()
    {
        if (_root is null)
            return 0;
        return _root.GetHeight();
    }

    public override string ToString()
    {
        return "<Bst>{" + string.Join(", ", this) + "}";
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}

public static class Trees
{
    /// <summary>
    /// Given a sorted list, create a balanced BST by always inserting
    /// the middle element of each sublist first.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree();
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// Problem 5: Recursively insert the middle value of the sublist defined
    /// by 'first' and 'last' indices into the BST, then recurse on the
    /// left half and right half.
    ///
    /// Plan:
    /// Step 1: Base case — if first > last, the sublist is empty, so return.
    /// Step 2: Find the middle index: (first + last) / 2.
    /// Step 3: Insert the value at the middle index into the BST.
    /// Step 4: Recurse on the left half: first to middle-1.
    /// Step 5: Recurse on the right half: middle+1 to last.
    /// </summary>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        // Base case: empty sublist
        if (first > last)
            return;

        // Find and insert the middle element
        int middle = (first + last) / 2;
        bst.Insert(sortedNumbers[middle]);

        // Recurse on left half
        InsertMiddle(sortedNumbers, first, middle - 1, bst);

        // Recurse on right half
        InsertMiddle(sortedNumbers, middle + 1, last, bst);
    }
}