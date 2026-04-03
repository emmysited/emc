using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Plan:
        // Base case: if n <= 0, return 0.
        // Recursive case: the sum of squares up to n is n^2 plus the sum of squares up to n-1.
        // SumSquaresRecursive(n) = n*n + SumSquaresRecursive(n-1)

        if (n <= 0)
            return 0;

        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Plan:
        // Base case: if the current word has reached the desired size, add it to results.
        // Recursive case: loop through each letter in letters,
        //   add that letter to the current word,
        //   remove that letter from the remaining letters (to avoid reuse),
        //   recurse with the updated word and remaining letters,
        //   then continue to the next letter.

        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            // Pick letters[i] and remove it from the remaining pool
            string remaining = letters[..i] + letters[(i + 1)..];
            PermutationsChoose(results, remaining, size, word + letters[i]);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Initialize memoization dictionary on first call
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        // If we already computed this value, return it directly (memoization)
        if (remember.ContainsKey(s))
            return remember[s];

        // Solve using recursion and store in remember to avoid recomputing
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///	
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Plan:
        // Base case: if there are no more wildcards (*) in the pattern,
        //   the pattern is a complete binary string — add it to results.
        // Recursive case: find the first * in the pattern,
        //   replace it with '0' and recurse,
        //   replace it with '1' and recurse.

        int wildcardIndex = pattern.IndexOf('*');

        if (wildcardIndex == -1)
        {
            // No wildcards left — this is a complete binary string
            results.Add(pattern);
            return;
        }

        // Replace the first * with '0' and recurse
        WildcardBinary(pattern[..wildcardIndex] + "0" + pattern[(wildcardIndex + 1)..], results);

        // Replace the first * with '1' and recurse
        WildcardBinary(pattern[..wildcardIndex] + "1" + pattern[(wildcardIndex + 1)..], results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Plan:
        // Step 1: Add the current position to the path.
        // Step 2: If this position is the end, add the path to results and return.
        // Step 3: Try moving in all 4 directions (right, left, down, up).
        //         For each direction, check if it is a valid move using IsValidMove.
        //         If valid, recurse in that direction.
        // Step 4: After exploring all directions, remove the current position
        //         from the path (backtracking) so other paths aren't affected.

        // Add current position to the path
        currPath.Add((x, y));

        // Base case: if we reached the end, save the path
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
        }
        else
        {
            // Try all 4 directions: right, left, down, up
            if (maze.IsValidMove(currPath, x + 1, y))
                SolveMaze(results, maze, x + 1, y, currPath);

            if (maze.IsValidMove(currPath, x - 1, y))
                SolveMaze(results, maze, x - 1, y, currPath);

            if (maze.IsValidMove(currPath, x, y + 1))
                SolveMaze(results, maze, x, y + 1, currPath);

            if (maze.IsValidMove(currPath, x, y - 1))
                SolveMaze(results, maze, x, y - 1, currPath);
        }

        // Backtrack: remove current position so other paths aren't affected
        currPath.RemoveAt(currPath.Count - 1);
    }
}