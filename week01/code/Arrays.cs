public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN:
        // Step 1: Create a new double array with size equal to 'length'.
        // Step 2: Loop from index 0 up to (but not including) length.
        // Step 3: At each index i, calculate the multiple: number * (i + 1).
        //         (i + 1 because the first multiple is number * 1, the second is number * 2, etc.)
        // Step 4: Store the calculated multiple in the array at position i.
        // Step 5: After the loop, return the completed array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN:
        // A "rotate right by amount" means the last 'amount' elements move to the front.
        // Example: {1,2,3,4,5,6,7,8,9} rotated right by 3 → {7,8,9,1,2,3,4,5,6}
        //   - The last 3 elements {7,8,9} become the new beginning.
        //   - The first 6 elements {1,2,3,4,5,6} follow after.
        //
        // Step 1: Find the split index — where the "tail" (end slice) begins.
        //         splitIndex = data.Count - amount
        //         Example: 9 - 3 = 6, so the tail starts at index 6.
        //
        // Step 2: Extract the tail slice using GetRange(splitIndex, amount).
        //         This gives us {7, 8, 9}.
        //
        // Step 3: Extract the head slice using GetRange(0, splitIndex).
        //         This gives us {1, 2, 3, 4, 5, 6}.
        //
        // Step 4: Clear the original list.
        //
        // Step 5: Add the tail slice first (it becomes the new front).
        //
        // Step 6: Add the head slice after (it follows the tail).
        //         Result: {7, 8, 9, 1, 2, 3, 4, 5, 6}

        int splitIndex = data.Count - amount;

        List<int> tail = data.GetRange(splitIndex, amount);
        List<int> head = data.GetRange(0, splitIndex);

        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);
    }
}