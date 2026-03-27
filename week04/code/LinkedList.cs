using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        // TODO Problem 1
        // Plan: Mirror of InsertHead but working from the tail end.
        // Step 1: Create a new node.
        // Step 2: If the list is empty, set both head and tail to the new node.
        // Step 3: If the list is not empty, connect the new node after the current tail,
        //         update the current tail's Next to point to the new node,
        //         set the new node's Prev to the current tail,
        //         then update _tail to be the new node.

        Node newNode = new(value);

        if (_tail is null)
        {
            // List is empty — both head and tail point to the new node
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Prev = _tail;  // Connect new node back to the current tail
            _tail.Next = newNode;  // Connect current tail forward to new node
            _tail = newNode;       // Update tail to be the new node
        }
    }

    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }

    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // TODO Problem 2
        // Plan: Mirror of RemoveHead but working from the tail end.
        // Step 1: If the list is empty or has one item (_head == _tail),
        //         set both head and tail to null.
        // Step 2: If the list has more than one item, disconnect the second-to-last
        //         node from the tail, then update _tail to be the second-to-last node.

        if (_head == _tail)
        {
            // Handles both empty list and single-item list
            _head = null;
            _tail = null;
        }
        else if (_tail is not null)
        {
            _tail.Prev!.Next = null; // Disconnect second-to-last node from the tail
            _tail = _tail.Prev;      // Update tail to be the second-to-last node
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        // TODO Problem 3
        // Plan:
        // Step 1: Start at the head and search for the first node with the matching value.
        // Step 2: If found at the head, call RemoveHead().
        // Step 3: If found at the tail, call RemoveTail().
        // Step 4: If found in the middle, reconnect the previous node's Next
        //         to skip the current node, and the next node's Prev to skip back.
        // Step 5: Stop searching after the first match is removed.

        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _head)
                {
                    RemoveHead();
                }
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    // Middle node: bypass it by relinking neighbors
                    curr.Prev!.Next = curr.Next; // Previous node skips over curr
                    curr.Next!.Prev = curr.Prev; // Next node points back past curr
                }
                return; // Stop after removing the first match
            }

            curr = curr.Next;
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        // TODO Problem 4
        // Plan:
        // Step 1: Start at the head and walk through every node.
        // Step 2: If the current node's data matches oldValue, update it to newValue.
        // Step 3: Unlike Remove, do NOT stop — continue through the entire list
        //         to replace ALL occurrences of oldValue.

        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue; // Replace the value in place
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse()
    {
        // TODO Problem 5
        // Plan: Mirror of GetEnumerator but start at the tail and walk backwards.
        // Step 1: Start at the tail instead of the head.
        // Step 2: Yield each node's data.
        // Step 3: Move to the previous node (curr.Prev) instead of curr.Next.
        // Step 4: Stop when curr is null (we've passed the head).

        var curr = _tail; // Start at the end for backward iteration
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Prev;       // Go backward in the linked list
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}